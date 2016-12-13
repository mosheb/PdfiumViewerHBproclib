//
// iapi.h.cs
// This file is part of Ghostscript.NET library
//
// Author: Josip Habjan (habjan@gmail.com, http://www.linkedin.com/in/habjan) 
// Copyright (c) 2013-2015 by Josip Habjan. All rights reserved.
//
// Author ported parts of this code from AFPL Ghostscript. 
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
using System.Runtime.InteropServices;
using System.Text;

namespace PdfiumViewerHB
{
    [UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Ansi)]
    public delegate bool FPDF_ImportPages(IntPtr dest_doc, IntPtr src_doc, string pagerange, int index);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate bool FPDF_SaveAsCopy(IntPtr document, FPDF_FILEWRITE saveData, int flag);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate bool FPDFLink_Enumerate(IntPtr page, ref int startPos, out IntPtr linkAnnot);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate bool FPDFLink_GetAnnotRect(IntPtr linkAnnot, FS_RECTF rect);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate bool FPDFPage_GetMediaBox(IntPtr page, out float left, out float bottom, out float right, out float top);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate bool FPDFText_FindNext(IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate double FPDF_GetPageHeight(IntPtr page);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate double FPDF_GetPageWidth(IntPtr page);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int FPDF_GetPageCount(IntPtr document);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int FPDF_GetPageSizeByIndex(IntPtr document, int page_index, out double width, out double height);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int FPDFText_GetSchCount(IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int FPDFText_GetSchResultIndex(IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int FPDFText_GetText(IntPtr page, int start_index, int count, byte[] result);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDF_BookmarkGetAction(IntPtr bookmark);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDF_BookmarkGetDest(IntPtr document, IntPtr bookmark);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDF_BookmarkGetFirstChild(IntPtr document, IntPtr bookmark);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDF_BookmarkGetNextSibling(IntPtr document, IntPtr bookmark);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDF_CreateNewDocument();
    [UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Ansi)]
    public delegate IntPtr FPDF_LoadMemDocument(byte[] data_buf, int size, string password);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDF_LoadPage(IntPtr document, int page_index);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFBitmap_CreateEx(int width, int height, int format, IntPtr first_scan, int stride);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFBitmap_Destroy(IntPtr bitmapHandle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFDOC_InitFormFillEnvironment(IntPtr document, ref FPDF_FORMFILLINFO formInfo);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFLink_GetAction(IntPtr link);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFLink_GetDest(IntPtr document, IntPtr link);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFText_FindStart(IntPtr page, byte[] findWhat, FPDF_SEARCH_FLAGS flags, int start_index);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr FPDFText_LoadPage(IntPtr page);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate uint FPDF_ActionGetType(IntPtr action);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate uint FPDF_BookmarkGetTitle(IntPtr bookmark, byte[] buffer, uint buflen);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate uint FPDF_GetDocPermissions(IntPtr document);
    [UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Ansi)]
    public delegate uint FPDFAction_GetURIPath(IntPtr document, IntPtr action, StringBuilder buffer, uint buflen);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate uint FPDFDest_GetPageIndex(IntPtr document, IntPtr dest);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FORM_DoDocumentAAction(IntPtr hHandle, FPDFDOC_AACTION aaType);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FORM_DoDocumentJSAction(IntPtr hHandle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FORM_DoDocumentOpenAction(IntPtr hHandle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FORM_DoPageAAction(IntPtr page, IntPtr _form, FPDFPAGE_AACTION fPDFPAGE_AACTION);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FORM_OnAfterLoadPage(IntPtr page, IntPtr _form);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FORM_OnBeforeClosePage(IntPtr page, IntPtr _form);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_AddRef();
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_CloseDocument(IntPtr document);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_ClosePage(IntPtr page);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_PageToDevice(IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, double page_x, double page_y, out int device_x, out int device_y);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_Release();
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_RenderPage(IntPtr dc, IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, FPDF flags);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_RenderPageBitmap(IntPtr bitmapHandle, IntPtr page, int start_x, int start_y, int size_x, int size_y, int rotate, FPDF flags);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_SetFormFieldHighlightAlpha(IntPtr hHandle, byte alpha);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDF_SetFormFieldHighlightColor(IntPtr hHandle, int fieldType, uint color);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDFBitmap_FillRect(IntPtr bitmapHandle, int left, int top, int width, int height, uint color);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDFDOC_ExitFormFillEnviroument(IntPtr hHandle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDFPage_SetMediaBox(IntPtr page, float left, float bottom, float right, float top);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDFText_ClosePage(IntPtr text_page);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDFText_FindClose(IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void FPDFText_GetCharBox(IntPtr page, int index, out double left, out double right, out double bottom, out double top);


    [StructLayout(LayoutKind.Sequential)]
    public class FPDF_FORMFILLINFO
    {
        public int version;

        private IntPtr Release;

        private IntPtr FFI_Invalidate;

        private IntPtr FFI_OutputSelectedRect;

        private IntPtr FFI_SetCursor;

        private IntPtr FFI_SetTimer;

        private IntPtr FFI_KillTimer;

        private IntPtr FFI_GetLocalTime;

        private IntPtr FFI_OnChange;

        private IntPtr FFI_GetPage;

        private IntPtr FFI_GetCurrentPage;

        private IntPtr FFI_GetRotation;

        private IntPtr FFI_ExecuteNamedAction;

        private IntPtr FFI_SetTextFieldFocus;

        private IntPtr FFI_DoURIAction;

        private IntPtr FFI_DoGoToAction;

        private IntPtr m_pJsPlatform;
    }

    public enum FPDF_UNSP
    {
        DOC_XFAFORM = 1,
        DOC_PORTABLECOLLECTION = 2,
        DOC_ATTACHMENT = 3,
        DOC_SECURITY = 4,
        DOC_SHAREDREVIEW = 5,
        DOC_SHAREDFORM_ACROBAT = 6,
        DOC_SHAREDFORM_FILESYSTEM = 7,
        DOC_SHAREDFORM_EMAIL = 8,
        ANNOT_3DANNOT = 11,
        ANNOT_MOVIE = 12,
        ANNOT_SOUND = 13,
        ANNOT_SCREEN_MEDIA = 14,
        ANNOT_SCREEN_RICHMEDIA = 15,
        ANNOT_ATTACHMENT = 16,
        ANNOT_SIG = 17
    }

    public enum FPDFDOC_AACTION
    {
        WC = 0x10,
        WS = 0x11,
        DS = 0x12,
        WP = 0x13,
        DP = 0x14
    }

    public enum FPDFPAGE_AACTION
    {
        OPEN = 0,
        CLOSE = 1
    }

    [Flags]
    public enum FPDF
    {
        ANNOT = 0x01,
        LCD_TEXT = 0x02,
        NO_NATIVETEXT = 0x04,
        GRAYSCALE = 0x08,
        DEBUG_INFO = 0x80,
        NO_CATCH = 0x100,
        RENDER_LIMITEDIMAGECACHE = 0x200,
        RENDER_FORCEHALFTONE = 0x400,
        PRINTING = 0x800,
        REVERSE_BYTE_ORDER = 0x10
    }

    [Flags]
    public enum FPDF_SEARCH_FLAGS
    {
        FPDF_MATCHCASE = 1,
        FPDF_MATCHWHOLEWORD = 2
    }

    [StructLayout(LayoutKind.Sequential)]
    public class FS_RECTF
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class FPDF_FILEWRITE
    {
        /// <summary>Version number of the interface. Currently must be 1.</summary>
        private int Version = 1;
        /// <summary>
        /// UserCallbackFunction. See <see cref="T:Patagames.Pdf.WriteBlockCallback" /> delegate for detail
        /// </summary>
        /// <remarks>Required: Yes.</remarks>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WriteBlockCallback WriteBlock;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public delegate bool WriteBlockCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FILEWRITE pThis, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, int buflen);



}
