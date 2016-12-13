﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;

namespace PdfiumViewerHB
{
    /// <summary>
    /// Provides functionality to render a PDF document.
    /// </summary>
    public class PdfDocument : IDisposable
    {
        private bool _disposed;
        private PdfFile _file;

        //each processor has it's own static pdfdoc...?
        //all "native" methods should be changed to _library or processor
        // **************************************************************
        private Processor.PdfiumProcessor _myProcessor=null;
        // **************************************************************

        /// <summary>
        /// Initializes a new instance of the PdfDocument class with the provided path.
        /// </summary>
        /// <param name="path"></param>
        public static PdfDocument Load(string path)
        {
            return new PdfDocument(path);
        }

        /// <summary>
        /// Initializes a new instance of the PdfDocument class with the provided stream.
        /// </summary>
        /// <param name="stream"></param>
        private static PdfDocument Load(Stream stream)
        {
            return new PdfDocument(stream);
        }

        /// <summary>
        /// Number of pages in the PDF document.
        /// </summary>
        public int PageCount
        {
            get { return PageSizes.Count; }
        }

        /// <summary>
        /// Bookmarks stored in this PdfFile
        /// </summary>
        public PdfBookmarkCollection Bookmarks
        {
            get { return _file.Bookmarks; }
        }

        /// <summary>
        /// Size of each page in the PDF document.
        /// </summary>
        public IList<SizeF> PageSizes { get; private set; }

        private PdfDocument(Stream stream)
            : this(PdfFile.Create(stream))
        {
        }

        private PdfDocument(string path)
            : this(File.OpenRead(path))
        {
        }

        private PdfDocument(PdfFile file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            _file = file;

            var pageSizes = file.GetPDFDocInfo();
            if (pageSizes == null)
                throw new Win32Exception();

            PageSizes = new ReadOnlyCollection<SizeF>(pageSizes);
        }

        /// <summary>
        /// Renders a page of the PDF document to the provided graphics instance.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="graphics">Graphics instance to render the page on.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="bounds">Bounds to render the page in.</param>
        /// <param name="forPrinting">Render the page for printing.</param>
        public void Render(int page, Graphics graphics, float dpiX, float dpiY, Rectangle bounds, bool forPrinting)
        {
            Render(page, graphics, dpiX, dpiY, bounds, forPrinting ? PdfRenderFlags.ForPrinting : PdfRenderFlags.None);
        }

        /// <summary>
        /// Renders a page of the PDF document to the provided graphics instance.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="graphics">Graphics instance to render the page on.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="bounds">Bounds to render the page in.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        public void Render(int page, Graphics graphics, float dpiX, float dpiY, Rectangle bounds, PdfRenderFlags flags)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            float graphicsDpiX = graphics.DpiX;
            float graphicsDpiY = graphics.DpiY;

            var dc = graphics.GetHdc();

            try
            {
                if ((int)graphicsDpiX != (int)dpiX || (int)graphicsDpiY != (int)dpiY)
                {
                    var transform = new NativeMethods.XFORM
                    {
                        eM11 = graphicsDpiX / dpiX,
                        eM22 = graphicsDpiY / dpiY
                    };

                    NativeMethods.SetGraphicsMode(dc, NativeMethods.GM_ADVANCED);
                    NativeMethods.ModifyWorldTransform(dc, ref transform, NativeMethods.MWT_LEFTMULTIPLY);
                }

                var point = new NativeMethods.POINT();
                NativeMethods.SetViewportOrgEx(dc, bounds.X, bounds.Y, out point);

                bool success = _file.RenderPDFPageToDC(
                    page,
                    dc,
                    (int)dpiX, (int)dpiY,
                    0, 0, bounds.Width, bounds.Height,
                    FlagsToFPDFFlags(flags)
                );

                NativeMethods.SetViewportOrgEx(dc, point.X, point.Y, out point);

                if (!success)
                    throw new Win32Exception();
            }
            finally
            {
                graphics.ReleaseHdc(dc);
            }
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="forPrinting">Render the page for printing.</param>
        /// <returns>The rendered image.</returns>
        public Image Render(int page, float dpiX, float dpiY, bool forPrinting)
        {
            var size = PageSizes[page];

            return Render(page, (int)size.Width, (int)size.Height, dpiX, dpiY, forPrinting);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        /// <returns>The rendered image.</returns>
        public Image Render(int page, float dpiX, float dpiY, PdfRenderFlags flags)
        {
            var size = PageSizes[page];

            return Render(page, (int)size.Width, (int)size.Height, dpiX, dpiY, flags);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="width">Width of the rendered image.</param>
        /// <param name="height">Height of the rendered image.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="forPrinting">Render the page for printing.</param>
        /// <returns>The rendered image.</returns>
        public Image Render(int page, int width, int height, float dpiX, float dpiY, bool forPrinting)
        {
            return Render(page, width, height, dpiX, dpiY, forPrinting ? PdfRenderFlags.ForPrinting : PdfRenderFlags.None);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="width">Width of the rendered image.</param>
        /// <param name="height">Height of the rendered image.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        /// <returns>The rendered image.</returns>
        public Image Render(int page, int width, int height, float dpiX, float dpiY, PdfRenderFlags flags)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bitmap.SetResolution(dpiX, dpiY);

            var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            try
            {
                var handle = NativePdfiumMethods.FPDFBitmap_CreateEx(width, height, 4, data.Scan0, width * 4);

                try
                {
                    uint background = (flags & PdfRenderFlags.Transparent) == 0 ? 0xFFFFFFFF : 0x00FFFFFF;

                    NativePdfiumMethods.FPDFBitmap_FillRect(handle, 0, 0, width, height, background);

                    bool success = _file.RenderPDFPageToBitmap(
                        page,
                        handle,
                        (int)dpiX, (int)dpiY,
                        0, 0, width, height,
                        this.FlagsToFPDFFlags(flags)
                    );

                    if (!success)
                        throw new Win32Exception();
                }
                finally
                {
                    NativePdfiumMethods.FPDFBitmap_Destroy(handle);
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            return bitmap;
        }

        private NativePdfiumMethods.FPDF FlagsToFPDFFlags(PdfRenderFlags flags)
        {
            return (NativePdfiumMethods.FPDF)(flags & ~PdfRenderFlags.Transparent);
        }

        /// <summary>
        /// Save the PDF document to the specified location.
        /// </summary>
        /// <param name="path">Path to save the PDF document to.</param>
        public void Save(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            using (var stream = File.Create(path))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Save the PDF document to the specified location.
        /// </summary>
        /// <param name="stream">Stream to save the PDF document to.</param>
        public void Save(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            _file.Save(stream);
        }

        public PdfMatches Search(string text, bool matchCase, bool wholeWord)
        {
            return Search(text, matchCase, wholeWord, 0, PageCount - 1);
        }

        public PdfMatches Search(string text, bool matchCase, bool wholeWord, int page)
        {
            return Search(text, matchCase, wholeWord, page, page);
        }

        public PdfMatches Search(string text, bool matchCase, bool wholeWord, int startPage, int endPage)
        {
            return _file.Search(text, matchCase, wholeWord, startPage, endPage);
        }

        ///// <summary>
        ///// Creates a <see cref="PrintDocument"/> for the PDF document.
        ///// </summary>
        ///// <returns></returns>
        //public PrintDocument CreatePrintDocument()
        //{
        //    return CreatePrintDocument(PdfPrintMode.CutMargin);
        //}

        ///// <summary>
        ///// Creates a <see cref="PrintDocument"/> for the PDF document.
        ///// </summary>
        ///// <param name="printMode">Specifies the mode for printing. The default
        ///// value for this parameter is CutMargin.</param>
        ///// <returns></returns>
        //public PrintDocument CreatePrintDocument(PdfPrintMode printMode)
        //{
        //    return new PdfPrintDocument(this, printMode);
        //}

        public PdfPageLinks GetPageLinks(int pageNumber, Size pageSize)
        {
            return _file.GetPageLinks(pageNumber, pageSize);
        }

        public byte[] GetPage(int PageNumber)
        {
           return  _file.GetPDFPage(PageNumber);
        }

        public void SetPageMediaBox(int index, float left, float right, float top, float bottom)
        {
            _file.SetPageMediaBox(index, left, right, top, bottom);
        }

        public PageRectangle GetPageMediaBox(int index)
        {
            return _file.GetPageMediaBox(index);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_file != null)
                {
                    _file.Dispose();
                    _file = null;
                }

                _disposed = true;
            }
        }
    }

    public class PageRectangle
    {
        public float Left
        {
            get { return left; }
            set { left = value; }
        }
        public float Right
        {
            get { return right; }
            set { right = value; }
        }
        public float Top
        {
            get { return top; }
            set { top = value; }
        }
        public float Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }
        internal float left;
        internal float right;
        internal float top;
        internal float bottom;
    }
}
