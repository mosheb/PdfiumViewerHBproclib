//
// GhostscriptProcessor.cs
// This file is part of Ghostscript.NET library
//
// Author: Josip Habjan (habjan@gmail.com, http://www.linkedin.com/in/habjan) 
// Copyright (c) 2013-2015 by Josip Habjan. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.WinAny.Interop;
using System.IO;
using static PdfiumViewerHB.NativeMethods;

namespace PdfiumViewerHB.Processor
{
    public partial class PdfiumProcessor : IDisposable
    {

        #region Private variables

        private bool _disposed = false;
        private PdfiumLibrary _gs;
//        private GhostscriptStdIO _stdIO_Callback;
 //       private GhostscriptProcessorInternalStdIOHandler _internalStdIO_Callback;
  //      private gsapi_pool_callback _poolCallBack;
  //      private StringBuilder _outputMessages = new StringBuilder();
  //      private StringBuilder _errorMessages = new StringBuilder();
   //     private int _totalPages;
        private bool _isRunning = false;
    //    private bool _stopProcessing = false;

        #endregion

        #region Constructor

        public PdfiumProcessor()
        {
            _gs = new PdfiumLibrary();
        }

        #endregion

        #region Destructor

        ~PdfiumProcessor()
        {
            Dispose(false);
        }

        #endregion

  
        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Dispose - disposing

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _gs.Dispose();
                }

                _disposed = true;
            }
        }

        #endregion

     
        private readonly object _Lock = new object();

        public void FPDF_AddRef()
        {
            lock (_Lock)
            {
                this.FPDF_AddRef();
            }
        }

        public void FPDF_Release()
        {
            lock (_Lock)
            {
                this.FPDF_Release();
            }
        }

        public IntPtr FPDF_LoadMemDocument(SafeHandle data_buf, int size, string password)
        {
            lock (_Lock)
            {
                return this.FPDF_LoadMemDocument(data_buf, size, password);
            }
        }

        public IntPtr FPDF_LoadMemDocument(byte[] data_buf, int size, string password)
        {
            lock (_Lock)
            {
                return this.FPDF_LoadMemDocument(data_buf, size, password);
            }
        }

        public void FPDF_CloseDocument(IntPtr document)
        {
            lock (_Lock)
            {
                this.FPDF_CloseDocument(document);
            }
        }

        public int FPDF_GetPageCount(IntPtr document)
        {
            lock (_Lock)
            {
                return this.FPDF_GetPageCount(document);
            }
        }

        public uint FPDF_GetDocPermissions(IntPtr document)
        {
            lock (_Lock)
            {
                return this.FPDF_GetDocPermissions(document);
            }
        }

        public IntPtr FPDFDOC_InitFormFillEnvironment(IntPtr document, ref FPDF_FORMFILLINFO formInfo)
        {
            lock (_Lock)
            {
                return this.FPDFDOC_InitFormFillEnvironment(document, ref formInfo);
            }
        }

        public void FPDF_SetFormFieldHighlightColor(IntPtr hHandle, int fieldType, uint color)
        {
            lock (_Lock)
            {
                this.FPDF_SetFormFieldHighlightColor(hHandle, fieldType, color);
            }
        }

        public void FPDF_SetFormFieldHighlightAlpha(IntPtr hHandle, byte alpha)
        {
            lock (_Lock)
            {
                this.FPDF_SetFormFieldHighlightAlpha(hHandle, alpha);
            }
        }

        public void FORM_DoDocumentJSAction(IntPtr hHandle)
        {
            lock (_Lock)
            {
                this.FORM_DoDocumentJSAction(hHandle);
            }
        }

        public void FORM_DoDocumentOpenAction(IntPtr hHandle)
        {
            lock (_Lock)
            {
                this.FORM_DoDocumentOpenAction(hHandle);
            }
        }

        public void FPDFDOC_ExitFormFillEnviroument(IntPtr hHandle)
        {
            lock (_Lock)
            {
                this.FPDFDOC_ExitFormFillEnviroument(hHandle);
            }
        }

        public void FORM_DoDocumentAAction(IntPtr hHandle, FPDFDOC_AACTION aaType)
        {
            lock (_Lock)
            {
                this.FORM_DoDocumentAAction(hHandle, aaType);
            }
        }

        public IntPtr FPDF_LoadPage(IntPtr document, int page_index)
        {
            lock (_Lock)
            {
                return this.FPDF_LoadPage(document, page_index);
            }
        }

        public IntPtr FPDFText_LoadPage(IntPtr page)
        {
            lock (_Lock)
            {
                return this.FPDFText_LoadPage(page);
            }
        }

        public void FORM_OnAfterLoadPage(IntPtr page, IntPtr _form)
        {
            lock (_Lock)
            {
                this.FORM_OnAfterLoadPage(page, _form);
            }
        }

        public void FORM_DoPageAAction(IntPtr page, IntPtr _form, FPDFPAGE_AACTION fPDFPAGE_AACTION)
        {
            lock (_Lock)
            {
                this.FORM_DoPageAAction(page, _form, fPDFPAGE_AACTION);
            }
        }

        public double FPDF_GetPageWidth(IntPtr page)
        {
            lock (_Lock)
            {
                return this.FPDF_GetPageWidth(page);
            }
        }

        public double FPDF_GetPageHeight(IntPtr page)
        {
            lock (_Lock)
            {
                return this.FPDF_GetPageHeight(page);
            }
        }

        public void FORM_OnBeforeClosePage(IntPtr page, IntPtr _form)
        {
            lock (_Lock)
            {
                this.FORM_OnBeforeClosePage(page, _form);
            }
        }

        public void FPDFText_ClosePage(IntPtr text_page)
        {
            lock (_Lock)
            {
                this.FPDFText_ClosePage(text_page);
            }
        }

        public void FPDF_ClosePage(IntPtr page)
        {
            lock (_Lock)
            {
                this.FPDF_ClosePage(page);
            }
        }

        public void FPDF_RenderPage(IntPtr dc, IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, FPDF flags)
        {
            lock (_Lock)
            {
                this.FPDF_RenderPage(dc, page, start_x, start_y, size_x, size_y, rotate, flags);
            }
        }

        public void FPDF_RenderPageBitmap(IntPtr bitmapHandle, IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, FPDF flags)
        {
            lock (_Lock)
            {
                this.FPDF_RenderPageBitmap(bitmapHandle, page, start_x, start_y, size_x, size_y, rotate, flags);
            }
        }

        public int FPDF_GetPageSizeByIndex(IntPtr document, int page_index, out double width, out double height)
        {
            lock (_Lock)
            {
                return this.FPDF_GetPageSizeByIndex(document, page_index, out width, out height);
            }
        }

        public IntPtr FPDFBitmap_CreateEx(int width, int height, int format, IntPtr first_scan, int stride)
        {
            lock (_Lock)
            {
                return this.FPDFBitmap_CreateEx(width, height, format, first_scan, stride);
            }
        }

        public void FPDFBitmap_FillRect(IntPtr bitmapHandle, int left, int top, int width, int height, uint color)
        {
            lock (_Lock)
            {
                this.FPDFBitmap_FillRect(bitmapHandle, left, top, width, height, color);
            }
        }

        public IntPtr FPDFBitmap_Destroy(IntPtr bitmapHandle)
        {
            lock (_Lock)
            {
                return this.FPDFBitmap_Destroy(bitmapHandle);
            }
        }

        public IntPtr FPDFText_FindStart(IntPtr page, byte[] findWhat, FPDF_SEARCH_FLAGS flags, int start_index)
        {
            lock (_Lock)
            {
                return this.FPDFText_FindStart(page, findWhat, flags, start_index);
            }
        }

        public int FPDFText_GetSchResultIndex(IntPtr handle)
        {
            lock (_Lock)
            {
                return this.FPDFText_GetSchResultIndex(handle);
            }
        }

        public int FPDFText_GetSchCount(IntPtr handle)
        {
            lock (_Lock)
            {
                return this.FPDFText_GetSchCount(handle);
            }
        }

        public int FPDFText_GetText(IntPtr page, int start_index, int count, byte[] result)
        {
            lock (_Lock)
            {
                return this.FPDFText_GetText(page, start_index, count, result);
            }
        }

        public void FPDFText_GetCharBox(IntPtr page, int index, out double left, out double right, out double bottom, out double top)
        {
            lock (_Lock)
            {
                this.FPDFText_GetCharBox(page, index, out left, out right, out bottom, out top);
            }
        }

        public bool FPDFText_FindNext(IntPtr handle)
        {
            lock (_Lock)
            {
                return this.FPDFText_FindNext(handle);
            }
        }

        public void FPDFText_FindClose(IntPtr handle)
        {
            lock (_Lock)
            {
                this.FPDFText_FindClose(handle);
            }
        }

        public bool FPDFLink_Enumerate(IntPtr page, ref int startPos, out IntPtr linkAnnot)
        {
            lock (_Lock)
            {
                return this.FPDFLink_Enumerate(page, ref startPos, out linkAnnot);
            }
        }

        public IntPtr FPDFLink_GetDest(IntPtr document, IntPtr link)
        {
            lock (_Lock)
            {
                return this.FPDFLink_GetDest(document, link);
            }
        }

        public uint FPDFDest_GetPageIndex(IntPtr document, IntPtr dest)
        {
            lock (_Lock)
            {
                return this.FPDFDest_GetPageIndex(document, dest);
            }
        }

        public bool FPDFLink_GetAnnotRect(IntPtr linkAnnot, FS_RECTF rect)
        {
            lock (_Lock)
            {
                return this.FPDFLink_GetAnnotRect(linkAnnot, rect);
            }
        }

        public void FPDF_PageToDevice(IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, double page_x, double page_y, out int device_x, out int device_y)
        {
            lock (_Lock)
            {
                this.FPDF_PageToDevice(page, start_x, start_y, size_x, size_y, rotate, page_x, page_y, out device_x, out device_y);
            }
        }

        public IntPtr FPDFLink_GetAction(IntPtr link)
        {
            lock (_Lock)
            {
                return this.FPDFLink_GetAction(link);
            }
        }

        public uint FPDFAction_GetURIPath(IntPtr document, IntPtr action, StringBuilder buffer, uint buflen)
        {
            lock (_Lock)
            {
                return this.FPDFAction_GetURIPath(document, action, buffer, buflen);
            }
        }

        public IntPtr FPDF_BookmarkGetFirstChild(IntPtr document, IntPtr bookmark)
        {
            lock (_Lock)
                return this.FPDF_BookmarkGetFirstChild(document, bookmark);
        }

        public IntPtr FPDF_BookmarkGetNextSibling(IntPtr document, IntPtr bookmark)
        {
            lock (_Lock)
                return this.FPDF_BookmarkGetNextSibling(document, bookmark);
        }

        public uint FPDF_BookmarkGetTitle(IntPtr bookmark, byte[] buffer, uint buflen)
        {
            lock (_Lock)
                return this.FPDF_BookmarkGetTitle(bookmark, buffer, buflen);
        }

        public IntPtr FPDF_BookmarkGetAction(IntPtr bookmark)
        {
            lock (_Lock)
                return this.FPDF_BookmarkGetAction(bookmark);
        }

        public IntPtr FPDF_BookmarkGetDest(IntPtr document, IntPtr bookmark)
        {
            lock (_Lock)
                return this.FPDF_BookmarkGetDest(document, bookmark);
        }

        public uint FPDF_ActionGetType(IntPtr action)
        {
            lock (_Lock)
                return this.FPDF_ActionGetType(action);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_GetBlockDelegate(IntPtr param, uint position, IntPtr buffer, uint size);

        private static readonly FPDF_GetBlockDelegate _getBlockDelegate = FPDF_GetBlock;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDF_SaveBlockDelegate(IntPtr fileWrite, IntPtr data, uint size);

        private static readonly FPDF_SaveBlockDelegate _saveBlockDelegate = FPDF_SaveBlock;

        private static int FPDF_GetBlock(IntPtr param, uint position, IntPtr buffer, uint size)
        {
            var stream = StreamManager.Get((int)param);
            if (stream == null)
                return 0;
            byte[] managedBuffer = new byte[size];

            stream.Position = position;
            int read = stream.Read(managedBuffer, 0, (int)size);
            if (read != size)
                return 0;

            Marshal.Copy(managedBuffer, 0, buffer, (int)size);
            return 1;
        }

        private static int FPDF_SaveBlock(IntPtr fileWrite, IntPtr data, uint size)
        {
            //var write = new FPDF_FILEWRITE();
            //Marshal.PtrToStructure(fileWrite, write);

            //var stream = StreamManager.Get((int)write.stream);
            //if (stream == null)
            //    return 0;

            //byte[] buffer = new byte[size];
            //Marshal.Copy(data, buffer, 0, (int)size);

            //try
            //{
            //    stream.Write(buffer, 0, (int)size);
            //    return (int)size;
            //}
            //catch
            //{
            //    return 0;
            //}
            return 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class FPDF_FILEACCESS
        {
            /// <summary>File length, in bytes.</summary>
            [MarshalAs(UnmanagedType.I4)]
            public uint FileLen;
            /// <summary>
            /// User callback function. See <see cref="T:Patagames.Pdf.GetBlockCallback" /> delegate for detail
            /// </summary>
            /// <remarks>Required: Yes.</remarks>
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public GetBlockCallback GetBlock;
            /// <summary>
            /// A custom pointer for all implementation specific data.
            /// This pointer will be used as the first parameter to getBlockCallback callback.
            /// </summary>
            [MarshalAs(UnmanagedType.SafeArray)]
            public byte[] Param;

            /// <summary>
            /// Initialize a new instance of the FPDF_FILEACCESS class using file length and user data
            /// </summary>
            /// <param name="FileLen">File length, in bytes.</param>
            /// <param name="param">A custom pointer for all implementation specific data.</param>
            public FPDF_FILEACCESS(uint FileLen, byte[] param = null)
            {
                this.Param = param;
                this.FileLen = FileLen;
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public delegate bool GetBlockCallback([MarshalAs(UnmanagedType.SafeArray)] byte[] param, [MarshalAs(UnmanagedType.I4)] uint position, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3), In, Out] byte[] buf, [MarshalAs(UnmanagedType.U4)] uint size);


        /// <summary>
        /// Opens a document using a .NET Stream. Allows opening huge
        /// PDFs without loading them into memory first.
        /// </summary>
        /// <param name="input">The input Stream. Don't dispose prior to closing the pdf.</param>
        /// <param name="password">Password, if the PDF is protected. Can be null.</param>
        /// <param name="streamPtr">Retrieves an IntPtr to the COM object for the Stream. The caller must release this with Marshal.Release prior to Disposing the Stream.</param>
        /// <returns>An IntPtr to the FPDF_DOCUMENT object.</returns>
        //public IntPtr FPDF_LoadCustomDocument(Stream input, string password, int id)
        //{
        //    var getBlock = Marshal.GetFunctionPointerForDelegate(_getBlockDelegate);

        //    var access = new FPDF_FILEACCESS
        //    {
        //        m_FileLen = (uint)input.Length,
        //        m_GetBlock = getBlock,
        //        m_Param = (IntPtr)id
        //    };

        //    lock (_Lock)
        //    {
        //        return this.FPDF_LoadCustomDocument(access, password);
        //    }
        //}

        //public FPDF_ERR FPDF_GetLastError()
        //{
        //    lock (_Lock)
        //    {
        //        return (FPDF_ERR)this.FPDF_GetLastError();
        //    }
        //}

        public void FPDFPage_SetRotation(IntPtr page, PdfRotation rotation)
        {
            lock (_Lock)
            {
                this.FPDFPage_SetRotation(page, rotation);
            }
        }

        public bool FPDFPage_GenerateContent(IntPtr page)
        {
            lock (_Lock)
            {
                return this.FPDFPage_GenerateContent(page);
            }
        }

        public void FPDFPage_Delete(IntPtr doc, int page)
        {
            lock (_Lock)
            {
                this.FPDFPage_Delete(doc, page);
            }
        }

        //public bool FPDF_SaveAsCopy(IntPtr doc, Stream output, FPDF_SAVE_FLAGS flags)
        //{
        //    int id = StreamManager.Register(output);

        //    try
        //    {
        //        var write = new FPDF_FILEWRITE
        //        {
        //            stream = (IntPtr)id,
        //            version = 1,
        //            WriteBlock = Marshal.GetFunctionPointerForDelegate(_saveBlockDelegate)
        //        };

        //        lock (_Lock)
        //        {
        //            return this.FPDF_SaveAsCopy(doc, write, flags);
        //        }
        //    }
        //    finally
        //    {
        //        StreamManager.Unregister(id);
        //    }
        //}

        //public bool FPDF_SaveWithVersion(IntPtr doc, Stream output, FPDF_SAVE_FLAGS flags, int fileVersion)
        //{
        //    int id = StreamManager.Register(output);

        //    try
        //    {
        //        var write = new FPDF_FILEWRITE
        //        {
        //            stream = (IntPtr)id,
        //            version = 1,
        //            WriteBlock = Marshal.GetFunctionPointerForDelegate(_saveBlockDelegate)
        //        };

        //        lock (_Lock)
        //        {
        //            return this.FPDF_SaveWithVersion(doc, write, flags, fileVersion);
        //        }
        //    }
        //    finally
        //    {
        //        StreamManager.Unregister(id);
        //    }
        //}

        //#region Process - device

        //public void Process(GhostscriptDevice device)
        //{
        //    this.Process(device, null);
        //}

        //#endregion

        //#region Process - args

        //public void Process(string[] args)
        //{
        //    this.Process(args, null);
        //}

        //#endregion

        //#region Process - device, stdIO_callback

        //public void Process(GhostscriptDevice device, GhostscriptStdIO stdIO_callback)
        //{
        //    this.StartProcessing(device, stdIO_callback);
        //}

        //#endregion

        //#region Process - args, stdIO_callback

        //public void Process(string[] args, GhostscriptStdIO stdIO_callback)
        //{
        //    this.StartProcessing(args, stdIO_callback);
        //}

        //#endregion

        //#region StartProcessing - device, stdIO_callback

        //public void StartProcessing(GhostscriptDevice device, GhostscriptStdIO stdIO_callback)
        //{
        //    if (device == null)
        //    {
        //        throw new ArgumentNullException("device");
        //    }

        //    this.StartProcessing(device.GetSwitches(), stdIO_callback);
        //}

        //#endregion

        //#region StartProcessing - args, stdIO_callback

        ///// <summary>
        ///// Run Ghostscript.
        ///// </summary>
        ///// <param name="args">Command arguments</param>
        ///// <param name="stdIO_callback">StdIO callback, can be set to null if you dont want to handle it.</param>
        //public void StartProcessing(string[] args, GhostscriptStdIO stdIO_callback)
        //{
        //    if (args == null)
        //    {
        //        throw new ArgumentNullException("args");
        //    }

        //    if (args.Length < 3)
        //    {
        //        throw new ArgumentOutOfRangeException("args");
        //    }

        //    for (int i = 0; i < args.Length; i++)
        //    {
        //        args[i] = System.Text.Encoding.Default.GetString(System.Text.Encoding.UTF8.GetBytes(args[i]));
        //    }

        //    _isRunning = true;

        //    IntPtr instance = IntPtr.Zero;

        //    int rc_ins = _gs.gsapi_new_instance(out instance, IntPtr.Zero);

        //    if (ierrors.IsError(rc_ins))
        //    {
        //        throw new GhostscriptAPICallException("gsapi_new_instance", rc_ins);
        //    }

        //    try
        //    {
        //        _stdIO_Callback = stdIO_callback;

        //        _internalStdIO_Callback = new GhostscriptProcessorInternalStdIOHandler(
        //                                        new StdInputEventHandler(OnStdIoInput), 
        //                                        new StdOutputEventHandler(OnStdIoOutput), 
        //                                        new StdErrorEventHandler(OnStdIoError));

        //        int rc_stdio = _gs.gsapi_set_stdio(instance,
        //                                _internalStdIO_Callback._std_in,
        //                                _internalStdIO_Callback._std_out,
        //                                _internalStdIO_Callback._std_err);

        //        _poolCallBack = new gsapi_pool_callback(Pool);

        //        int rc_pool = _gs.gsapi_set_poll(instance, _poolCallBack);

        //        if (ierrors.IsError(rc_pool))
        //        {
        //            throw new GhostscriptAPICallException("gsapi_set_poll", rc_pool);

        //        }

        //        if (ierrors.IsError(rc_stdio))
        //        {
        //            throw new GhostscriptAPICallException("gsapi_set_stdio", rc_stdio);
        //        }

        //        this.OnStarted(new GhostscriptProcessorEventArgs());

        //        _stopProcessing = false;

        //        if (_gs.is_gsapi_set_arg_encoding_supported)
        //        {
        //            int rc_enc = _gs.gsapi_set_arg_encoding(instance, GS_ARG_ENCODING.UTF8);
        //        }

        //        int rc_init = _gs.gsapi_init_with_args(instance, args.Length, args);

        //        if (ierrors.IsErrorIgnoreQuit(rc_init))
        //        {
        //            if (!ierrors.IsInterrupt(rc_init))
        //            {
        //                throw new GhostscriptAPICallException("gsapi_init_with_args", rc_init);
        //            }
        //        }

        //        int rc_exit = _gs.gsapi_exit(instance);

        //        if (ierrors.IsErrorIgnoreQuit(rc_exit))
        //        {
        //            throw new GhostscriptAPICallException("gsapi_exit", rc_exit);
        //        }
        //    }
        //    finally
        //    {
        //        _gs.gsapi_delete_instance(instance);

        //        GC.Collect();

        //        _isRunning = false;

        //        this.OnCompleted(new GhostscriptProcessorEventArgs());
        //    }
        //}

        //#endregion

        //#region StopProcessing

        //public void StopProcessing()
        //{
        //    _stopProcessing = true;
        //}

        //#endregion

        //#region Pool

        //private int Pool(IntPtr handle)
        //{
        //    if (_stopProcessing)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //#endregion

        //#region OnStdIoInput

        //private void OnStdIoInput(out string input, int count)
        //{
        //    if (_stdIO_Callback != null)
        //    {
        //        _stdIO_Callback.StdIn(out input, count);
        //    }
        //    else
        //    {
        //        input = string.Empty;
        //    }
        //}

        //#endregion

        //#region OnStdIoOutput

        //private void OnStdIoOutput(string output)
        //{
        //    lock (_outputMessages)
        //    {
        //        _outputMessages.Append(output);

        //        int rIndex = _outputMessages.ToString().IndexOf("\r\n");

        //        while (rIndex > -1)
        //        {
        //            string line = _outputMessages.ToString().Substring(0, rIndex);
        //            _outputMessages = _outputMessages.Remove(0, rIndex + 2);

        //            this.ProcessOutputLine(line);

        //            rIndex = _outputMessages.ToString().IndexOf("\r\n");
        //        }

        //        if (_stdIO_Callback != null)
        //        {
        //            _stdIO_Callback.StdOut(output);
        //        }
        //    }
        //}

        //#endregion

        //#region OnStdIoError

        //private void OnStdIoError(string error)
        //{
        //    lock (_errorMessages)
        //    {
        //        _outputMessages.Append(error);

        //        int rIndex = _errorMessages.ToString().IndexOf("\r\n");

        //        while (rIndex > -1)
        //        {
        //            string line = _errorMessages.ToString().Substring(0, rIndex);
        //            _errorMessages = _errorMessages.Remove(0, rIndex + 2);

        //            this.ProcessErrorLine(line);

        //            rIndex = _errorMessages.ToString().IndexOf("\r\n");
        //        }

        //        if (_stdIO_Callback != null)
        //        {
        //            _stdIO_Callback.StdError(error);
        //        }
        //    }
        //}

        //#endregion

        //#region ProcessOutputLine

        //private void ProcessOutputLine(string line)
        //{
        //    if (line.StartsWith("Processing pages"))
        //    {
        //        string[] chunks = line.Split(EMPTY_SPACE_SPLIT);
        //        _totalPages = int.Parse(chunks[chunks.Length - 1].TrimEnd('.'));
        //    }
        //    else if (line.StartsWith("Page"))
        //    {
        //        string[] chunks = line.Split(EMPTY_SPACE_SPLIT);
        //        int currentPage = int.Parse(chunks[chunks.Length - 1]);

        //        this.OnProcessing(new GhostscriptProcessorProcessingEventArgs(currentPage, _totalPages));
        //    }
        //}

        //#endregion

        //#region ProcessErrorLine

        //private void ProcessErrorLine(string line)
        //{
        //    this.OnError(new GhostscriptProcessorErrorEventArgs(line));
        //}

        //#endregion

        #region IsRunning

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        #endregion

    }
}
