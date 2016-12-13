using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static PdfiumViewerHB.NativePdfiumMethods;

namespace PdfiumViewerHB
{
    internal abstract class PdfFile : IDisposable
    {
        private static readonly Encoding FPDFEncoding = new UnicodeEncoding(false, false, false);

        private IntPtr _document;
        private IntPtr _form;
        private bool _disposed;
        private NativePdfiumMethods.FPDF_FORMFILLINFO _formCallbacks;
        private GCHandle _formCallbacksHandle;

        public static PdfFile Create(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (stream is MemoryStream)
                return new PdfMemoryStreamFile((MemoryStream)stream);
            if (stream is FileStream)
                return new PdfFileStreamFile((FileStream)stream);
            return new PdfBufferFile(StreamExtensions.ToByteArray(stream));
        }

        protected PdfFile()
        {
            PdfLibrary.EnsureLoaded();
        }

        public PdfBookmarkCollection Bookmarks { get; private set; }

        public bool RenderPDFPageToDC(int pageNumber, IntPtr dc, int dpiX, int dpiY, int boundsOriginX, int boundsOriginY, int boundsWidth, int boundsHeight, NativePdfiumMethods.FPDF flags)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            using (var pageData = new PageData(_document, _form, pageNumber))
            {
                NativePdfiumMethods.FPDF_RenderPage(dc, pageData.Page, boundsOriginX, boundsOriginY, boundsWidth, boundsHeight, 0, flags);
            }

            return true;
        }

        public byte[] GetPDFPage(int pageNumber)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            IntPtr newDocument = NativePdfiumMethods.FPDF_CreateNewDocument();

            var bl = NativePdfiumMethods.FPDF_ImportPages(newDocument, _document, pageNumber.ToString(), 0);

            int pageCount = NativePdfiumMethods.FPDF_GetPageCount(newDocument);


            using (MemoryStream ms = new MemoryStream())
            {

                FPDF_FILEWRITE saveData = new FPDF_FILEWRITE();
                saveData.WriteBlock = (WriteBlockCallback)((pThis, buffer, buflen) =>
                {
                    ms.Write(buffer, 0, buffer.Length);
                    return true;
                });
                try
                {
                    NativePdfiumMethods.FPDF_SaveAsCopy(newDocument, saveData, 1);

                }
                finally
                {
                    GC.KeepAlive((object)saveData);
                }

                NativePdfiumMethods.FPDF_CloseDocument(newDocument);

                return ms.ToArray();
            }

        }

        public void SetPageMediaBox(int index, float left, float right, float top, float bottom)
        {
            IntPtr pageHandle = FPDF_LoadPage(_document, index);
            FPDFPage_SetMediaBox(pageHandle, left, bottom, right, top);
            FPDF_ClosePage(pageHandle);
        }

        public PageRectangle GetPageMediaBox(int index)
        {
            var rect = new PageRectangle();
            IntPtr pageHandle = FPDF_LoadPage(_document, index);
            FPDFPage_GetMediaBox(pageHandle, out rect.left, out rect.bottom, out rect.right, out rect.top);
            FPDF_ClosePage(pageHandle);
            return rect;
        }


        public bool RenderPDFPageToBitmap(int pageNumber, IntPtr bitmapHandle, int dpiX, int dpiY, int boundsOriginX, int boundsOriginY, int boundsWidth, int boundsHeight, NativePdfiumMethods.FPDF flags)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            using (var pageData = new PageData(_document, _form, pageNumber))
            {
                NativePdfiumMethods.FPDF_RenderPageBitmap(bitmapHandle, pageData.Page, boundsOriginX, boundsOriginY, boundsWidth, boundsHeight, 0, flags);
            }

            return true;
        }

        public PdfPageLinks GetPageLinks(int pageNumber, Size pageSize)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            var links = new List<PdfPageLink>();

            using (var pageData = new PageData(_document, _form, pageNumber))
            {
                int link = 0;
                IntPtr annotation;

                while (NativePdfiumMethods.FPDFLink_Enumerate(pageData.Page, ref link, out annotation))
                {
                    var destination = NativePdfiumMethods.FPDFLink_GetDest(_document, annotation);
                    int? target = null;
                    string uri = null;

                    if (destination != IntPtr.Zero)
                        target = (int)NativePdfiumMethods.FPDFDest_GetPageIndex(_document, destination);

                    var action = NativePdfiumMethods.FPDFLink_GetAction(annotation);
                    if (action != IntPtr.Zero)
                    {
                        const uint length = 1024;
                        var sb = new StringBuilder(1024);
                        NativePdfiumMethods.FPDFAction_GetURIPath(_document, action, sb, length);

                        uri = sb.ToString();
                    }

                    var rect = new NativePdfiumMethods.FS_RECTF();

                    if (NativePdfiumMethods.FPDFLink_GetAnnotRect(annotation, rect) && (target.HasValue || uri != null))
                    {
                        int deviceX1;
                        int deviceY1;
                        int deviceX2;
                        int deviceY2;

                        NativePdfiumMethods.FPDF_PageToDevice(
                            pageData.Page,
                            0,
                            0,
                            pageSize.Width,
                            pageSize.Height,
                            0,
                            rect.left,
                            rect.top,
                            out deviceX1,
                            out deviceY1
                        );

                        NativePdfiumMethods.FPDF_PageToDevice(
                            pageData.Page,
                            0,
                            0,
                            pageSize.Width,
                            pageSize.Height,
                            0,
                            rect.right,
                            rect.bottom,
                            out deviceX2,
                            out deviceY2
                        );

                        links.Add(new PdfPageLink(
                            new Rectangle(deviceX1, deviceY1, deviceX2 - deviceX1, deviceY2 - deviceY1),
                            target,
                            uri
                        ));
                    }
                }
            }

            return new PdfPageLinks(links);
        }

        public List<SizeF> GetPDFDocInfo()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            int pageCount = NativePdfiumMethods.FPDF_GetPageCount(_document);
            var result = new List<SizeF>(pageCount);

            for (int i = 0; i < pageCount; i++)
            {
                double height;
                double width;
                NativePdfiumMethods.FPDF_GetPageSizeByIndex(_document, i, out width, out height);

                result.Add(new SizeF((float)width, (float)height));
            }

            return result;
        }

        public abstract void Save(Stream stream);

        protected void LoadDocument(IntPtr document)
        {
            _document = document;

            NativePdfiumMethods.FPDF_GetDocPermissions(_document);

            _formCallbacks = new NativePdfiumMethods.FPDF_FORMFILLINFO();
            _formCallbacksHandle = GCHandle.Alloc(_formCallbacks);
            _formCallbacks.version = 1;

            _form = NativePdfiumMethods.FPDFDOC_InitFormFillEnvironment(_document, ref _formCallbacks);
            NativePdfiumMethods.FPDF_SetFormFieldHighlightColor(_form, 0, 0xFFE4DD);
            NativePdfiumMethods.FPDF_SetFormFieldHighlightAlpha(_form, 100);

            NativePdfiumMethods.FORM_DoDocumentJSAction(_form);
            NativePdfiumMethods.FORM_DoDocumentOpenAction(_form);

            Bookmarks = new PdfBookmarkCollection();

            LoadBookmarks(Bookmarks, NativePdfiumMethods.FPDF_BookmarkGetFirstChild(document, IntPtr.Zero));
        }

        private void LoadBookmarks(PdfBookmarkCollection bookmarks, IntPtr bookmark)
        {
            if (bookmark == IntPtr.Zero)
                return;

            bookmarks.Add(LoadBookmark(bookmark));
            while ((bookmark = NativePdfiumMethods.FPDF_BookmarkGetNextSibling(_document, bookmark)) != IntPtr.Zero)
                bookmarks.Add(LoadBookmark(bookmark));
        }

        private PdfBookmark LoadBookmark(IntPtr bookmark)
        {
            var result = new PdfBookmark
            {
                Title = GetBookmarkTitle(bookmark),
                PageIndex = (int)GetBookmarkPageIndex(bookmark)
            };

            //Action = NativePdfiumMethods.FPDF_BookmarkGetAction(_bookmark);
            //if (Action != IntPtr.Zero)
            //    ActionType = NativePdfiumMethods.FPDF_ActionGetType(Action);

            var child = NativePdfiumMethods.FPDF_BookmarkGetFirstChild(_document, bookmark);
            if (child != IntPtr.Zero)
                LoadBookmarks(result.Children, child);

            return result;
        }

        private string GetBookmarkTitle(IntPtr bookmark)
        {
            uint length = NativePdfiumMethods.FPDF_BookmarkGetTitle(bookmark, null, 0);
            byte[] buffer = new byte[length];
            NativePdfiumMethods.FPDF_BookmarkGetTitle(bookmark, buffer, length);

            string result = Encoding.Unicode.GetString(buffer);
            return result.Substring(0, result.Length - 1);
        }

        private uint GetBookmarkPageIndex(IntPtr bookmark)
        {
            IntPtr dest = NativePdfiumMethods.FPDF_BookmarkGetDest(_document, bookmark);
            if (dest != IntPtr.Zero)
                return NativePdfiumMethods.FPDFDest_GetPageIndex(_document, dest);

            return 0;
        }

        public PdfMatches Search(string text, bool matchCase, bool wholeWord, int startPage, int endPage)
        {
            var matches = new List<PdfMatch>();

            for (int page = startPage; page <= endPage; page++)
            {
                using (var pageData = new PageData(_document, _form, page))
                {
                    NativePdfiumMethods.FPDF_SEARCH_FLAGS flags = 0;
                    if (matchCase)
                        flags |= NativePdfiumMethods.FPDF_SEARCH_FLAGS.FPDF_MATCHCASE;
                    if (wholeWord)
                        flags |= NativePdfiumMethods.FPDF_SEARCH_FLAGS.FPDF_MATCHWHOLEWORD;

                    var handle = NativePdfiumMethods.FPDFText_FindStart(pageData.TextPage, FPDFEncoding.GetBytes(text), flags, 0);

                    try
                    {
                        while (NativePdfiumMethods.FPDFText_FindNext(handle))
                        {
                            int index = NativePdfiumMethods.FPDFText_GetSchResultIndex(handle);

                            int matchLength = NativePdfiumMethods.FPDFText_GetSchCount(handle);

                            var result = new byte[(matchLength + 1) * 2];
                            NativePdfiumMethods.FPDFText_GetText(pageData.TextPage, index, matchLength, result);
                            string match = FPDFEncoding.GetString(result, 0, matchLength * 2);

                            double left, right, bottom, top;
                            NativePdfiumMethods.FPDFText_GetCharBox(pageData.TextPage, index, out left, out right, out bottom, out top);

                            matches.Add(new PdfMatch(
                                new PointF((float)left, (float)top),
                                match,
                                page
                            ));
                        }
                    }
                    finally
                    {
                        NativePdfiumMethods.FPDFText_FindClose(handle);
                    }
                }
            }

            return new PdfMatches(startPage, endPage, matches);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_form != IntPtr.Zero)
                {
                    NativePdfiumMethods.FORM_DoDocumentAAction(_form, NativePdfiumMethods.FPDFDOC_AACTION.WC);
                    NativePdfiumMethods.FPDFDOC_ExitFormFillEnviroument(_form);
                    _form = IntPtr.Zero;
                }

                if (_document != IntPtr.Zero)
                {
                    NativePdfiumMethods.FPDF_CloseDocument(_document);
                    _document = IntPtr.Zero;
                }

                if (_formCallbacksHandle.IsAllocated)
                    _formCallbacksHandle.Free();

                _disposed = true;
            }
        }

       
        private class PageData : IDisposable
        {
            private readonly IntPtr _form;
            private bool _disposed;

            public IntPtr Page { get; private set; }

            public IntPtr TextPage { get; private set; }

            public double Width { get; private set; }

            public double Height { get; private set; }

            public PageData(IntPtr document, IntPtr form, int pageNumber)
            {
                _form = form;

                Page = NativePdfiumMethods.FPDF_LoadPage(document, pageNumber);
                TextPage = NativePdfiumMethods.FPDFText_LoadPage(Page);
                NativePdfiumMethods.FORM_OnAfterLoadPage(Page, form);
                NativePdfiumMethods.FORM_DoPageAAction(Page, form, NativePdfiumMethods.FPDFPAGE_AACTION.OPEN);

                Width = NativePdfiumMethods.FPDF_GetPageWidth(Page);
                Height = NativePdfiumMethods.FPDF_GetPageHeight(Page);
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    NativePdfiumMethods.FORM_DoPageAAction(Page, _form, NativePdfiumMethods.FPDFPAGE_AACTION.CLOSE);
                    NativePdfiumMethods.FORM_OnBeforeClosePage(Page, _form);
                    NativePdfiumMethods.FPDFText_ClosePage(TextPage);
                    NativePdfiumMethods.FPDF_ClosePage(Page);

                    _disposed = true;
                }
            }
        }
    }
}
