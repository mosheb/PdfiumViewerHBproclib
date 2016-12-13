//
// GhostscriptLibrary.cs
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
using System.IO;
using Microsoft.WinAny.Interop;
using System.Runtime.InteropServices;
using static PdfiumViewerHB.NativeMethods;

namespace PdfiumViewerHB
{
    /// <summary>
    /// Represents a native Ghostscript library.
    /// </summary>
    public class PdfiumLibrary : IDisposable
    {

        #region Private variables

        private bool _disposed = false;
        private DynamicNativeLibrary _library;
        private bool _loadedFromMemory = false;
        private int _revision;

        #endregion

       #region Constructor - version, fromMemory

        /// <summary>
        /// Initializes a new instance of the Ghostscript.NET.GhostscriptLibrary class
        /// from the GhostscriptVersionInfo object.
        /// </summary>
        /// <param name="version">GhostscriptVersionInfo instance that tells which Ghostscript library to use.</param>
        /// <param name="fromMemory">Tells if the Ghostscript should be loaded from the memory or directly from the disk.</param>
        public PdfiumLibrary()
        {

            _loadedFromMemory = true;

            // load native Ghostscript library into the memory
            byte[] buffer = File.ReadAllBytes("pdfium.dll");

                // create DynamicNativeLibrary instance from the memory buffer
            _library = new DynamicNativeLibrary(buffer);

            // get and map native library symbols
            this.Initialize();
        }

        #endregion

        #region Destructor

        ~PdfiumLibrary()
        {
            Dispose(false);
        }

        #endregion

        #region Dispose

        #region Dispose

        /// <summary>
        /// Releases all resources used by the Ghostscript.NET.GhostscriptLibrary instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Dispose - disposing

        /// <summary>
        /// Releases all resources used by the Ghostscript.NET.GhostscriptLibrary instance.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _library.Dispose(); 
                    _library = null;
                }

                _disposed = true;
            }
        }

        #endregion

        #endregion

        #region functions

        public FPDF_ActionGetType @FPDF_ActionGetType = null;
        public FPDF_AddRef @FPDF_AddRef = null;
        public FPDF_BookmarkGetAction @FPDF_BookmarkGetAction = null;
        public FPDF_BookmarkGetDest @FPDF_BookmarkGetDest = null;
        public FPDF_BookmarkGetFirstChild @FPDF_BookmarkGetFirstChild = null;
        public FPDF_BookmarkGetNextSibling @FPDF_BookmarkGetNextSibling = null;
        public FPDF_BookmarkGetTitle @FPDF_BookmarkGetTitle = null;
        public FPDF_CloseDocument @FPDF_CloseDocument = null;
        public FPDF_ClosePage @FPDF_ClosePage = null;
        public FPDF_CreateNewDocument @FPDF_CreateNewDocument = null;
        public FPDF_GetDocPermissions @FPDF_GetDocPermissions = null;
        public FPDF_GetPageCount @FPDF_GetPageCount = null;
        public FPDF_GetPageHeight @FPDF_GetPageHeight = null;
        public FPDF_GetPageSizeByIndex @FPDF_GetPageSizeByIndex = null;
        public FPDF_GetPageWidth @FPDF_GetPageWidth = null;
        public FPDF_ImportPages @FPDF_ImportPages = null;
        public FPDF_LoadMemDocument @FPDF_LoadMemDocument = null;
        public FPDF_LoadPage @FPDF_LoadPage = null;
        public FPDF_PageToDevice @FPDF_PageToDevice = null;
        public FPDF_Release @FPDF_Release = null;
        public FPDF_RenderPage @FPDF_RenderPage = null;
        public FPDF_RenderPageBitmap @FPDF_RenderPageBitmap = null;
        public FPDF_SaveAsCopy @FPDF_SaveAsCopy = null;
        public FPDF_SetFormFieldHighlightAlpha @FPDF_SetFormFieldHighlightAlpha = null;
        public FPDF_SetFormFieldHighlightColor @FPDF_SetFormFieldHighlightColor = null;
        public FPDFAction_GetURIPath @FPDFAction_GetURIPath = null;
        public FPDFBitmap_CreateEx @FPDFBitmap_CreateEx = null;
        public FPDFBitmap_Destroy @FPDFBitmap_Destroy = null;
        public FPDFBitmap_FillRect @FPDFBitmap_FillRect = null;
        public FPDFDest_GetPageIndex @FPDFDest_GetPageIndex = null;
        public FPDFDOC_ExitFormFillEnviroument @FPDFDOC_ExitFormFillEnviroument = null;
        public FPDFDOC_InitFormFillEnvironment @FPDFDOC_InitFormFillEnvironment = null;
        public FPDFLink_Enumerate @FPDFLink_Enumerate = null;
        public FPDFLink_GetAction @FPDFLink_GetAction = null;
        public FPDFLink_GetAnnotRect @FPDFLink_GetAnnotRect = null;
        public FPDFLink_GetDest @FPDFLink_GetDest = null;
        //public FPDFPAGE_AACTION @FPDFPAGE_AACTION = null;
        public FPDFPage_GetMediaBox @FPDFPage_GetMediaBox = null;
        public FPDFPage_SetMediaBox @FPDFPage_SetMediaBox = null;
        public FPDFText_ClosePage @FPDFText_ClosePage = null;
        public FPDFText_FindClose @FPDFText_FindClose = null;
        public FPDFText_FindNext @FPDFText_FindNext = null;
        public FPDFText_FindStart @FPDFText_FindStart = null;
        public FPDFText_GetCharBox @FPDFText_GetCharBox = null;
        public FPDFText_GetSchCount @FPDFText_GetSchCount = null;
        public FPDFText_GetSchResultIndex @FPDFText_GetSchResultIndex = null;
        public FPDFText_GetText @FPDFText_GetText = null;
        public FPDFText_LoadPage @FPDFText_LoadPage = null;

        #endregion 

        #region Initialize

        /// <summary>
        /// Get the native library symbols and map them to the appropriate functions/delegates.
        /// </summary>
        private void Initialize()
        {

            //  string symbolMappingError = "Delegate of an exported function couldn't be created for symbol '{0}'";
            this.FPDF_ActionGetType = _library.GetDelegateForFunction<FPDF_ActionGetType>("FPDF_ActionGetType");
            this.FPDF_AddRef = _library.GetDelegateForFunction<FPDF_AddRef>("FPDF_AddRef");
            this.FPDF_BookmarkGetAction = _library.GetDelegateForFunction<FPDF_BookmarkGetAction>("FPDF_BookmarkGetAction");
            this.FPDF_BookmarkGetDest = _library.GetDelegateForFunction<FPDF_BookmarkGetDest>("FPDF_BookmarkGetDest");
            this.FPDF_BookmarkGetFirstChild = _library.GetDelegateForFunction<FPDF_BookmarkGetFirstChild>("FPDF_BookmarkGetFirstChild");
            this.FPDF_BookmarkGetNextSibling = _library.GetDelegateForFunction<FPDF_BookmarkGetNextSibling>("FPDF_BookmarkGetNextSibling");
            this.FPDF_BookmarkGetTitle = _library.GetDelegateForFunction<FPDF_BookmarkGetTitle>("FPDF_BookmarkGetTitle");
            this.FPDF_CloseDocument = _library.GetDelegateForFunction<FPDF_CloseDocument>("FPDF_CloseDocument");
            this.FPDF_ClosePage = _library.GetDelegateForFunction<FPDF_ClosePage>("FPDF_ClosePage");
            this.FPDF_CreateNewDocument = _library.GetDelegateForFunction<FPDF_CreateNewDocument>("FPDF_CreateNewDocument");
            this.FPDF_GetDocPermissions = _library.GetDelegateForFunction<FPDF_GetDocPermissions>("FPDF_GetDocPermissions");
            this.FPDF_GetPageCount = _library.GetDelegateForFunction<FPDF_GetPageCount>("FPDF_GetPageCount");
            this.FPDF_GetPageHeight = _library.GetDelegateForFunction<FPDF_GetPageHeight>("FPDF_GetPageHeight");
            this.FPDF_GetPageSizeByIndex = _library.GetDelegateForFunction<FPDF_GetPageSizeByIndex>("FPDF_GetPageSizeByIndex");
            this.FPDF_GetPageWidth = _library.GetDelegateForFunction<FPDF_GetPageWidth>("FPDF_GetPageWidth");
            this.FPDF_ImportPages = _library.GetDelegateForFunction<FPDF_ImportPages>("FPDF_ImportPages");
            this.FPDF_LoadMemDocument = _library.GetDelegateForFunction<FPDF_LoadMemDocument>("FPDF_LoadMemDocument");
            this.FPDF_LoadPage = _library.GetDelegateForFunction<FPDF_LoadPage>("FPDF_LoadPage");
            this.FPDF_PageToDevice = _library.GetDelegateForFunction<FPDF_PageToDevice>("FPDF_PageToDevice");
            this.FPDF_Release = _library.GetDelegateForFunction<FPDF_Release>("FPDF_Release");
            this.FPDF_RenderPage = _library.GetDelegateForFunction<FPDF_RenderPage>("FPDF_RenderPage");
            this.FPDF_RenderPageBitmap = _library.GetDelegateForFunction<FPDF_RenderPageBitmap>("FPDF_RenderPageBitmap");
            this.FPDF_SaveAsCopy = _library.GetDelegateForFunction<FPDF_SaveAsCopy>("FPDF_SaveAsCopy");
            this.FPDF_SetFormFieldHighlightAlpha = _library.GetDelegateForFunction<FPDF_SetFormFieldHighlightAlpha>("FPDF_SetFormFieldHighlightAlpha");
            this.FPDF_SetFormFieldHighlightColor = _library.GetDelegateForFunction<FPDF_SetFormFieldHighlightColor>("FPDF_SetFormFieldHighlightColor");
            this.FPDFAction_GetURIPath = _library.GetDelegateForFunction<FPDFAction_GetURIPath>("FPDFAction_GetURIPath");
            this.FPDFBitmap_CreateEx = _library.GetDelegateForFunction<FPDFBitmap_CreateEx>("FPDFBitmap_CreateEx");
            this.FPDFBitmap_Destroy = _library.GetDelegateForFunction<FPDFBitmap_Destroy>("FPDFBitmap_Destroy");
            this.FPDFBitmap_FillRect = _library.GetDelegateForFunction<FPDFBitmap_FillRect>("FPDFBitmap_FillRect");
            this.FPDFDest_GetPageIndex = _library.GetDelegateForFunction<FPDFDest_GetPageIndex>("FPDFDest_GetPageIndex");
            this.FPDFDOC_ExitFormFillEnviroument = _library.GetDelegateForFunction<FPDFDOC_ExitFormFillEnviroument>("FPDFDOC_ExitFormFillEnviroument");
            this.FPDFDOC_InitFormFillEnvironment = _library.GetDelegateForFunction<FPDFDOC_InitFormFillEnvironment>("FPDFDOC_InitFormFillEnvironment");
            this.FPDFLink_Enumerate = _library.GetDelegateForFunction<FPDFLink_Enumerate>("FPDFLink_Enumerate");
            this.FPDFLink_GetAction = _library.GetDelegateForFunction<FPDFLink_GetAction>("FPDFLink_GetAction");
            this.FPDFLink_GetAnnotRect = _library.GetDelegateForFunction<FPDFLink_GetAnnotRect>("FPDFLink_GetAnnotRect");
            this.FPDFLink_GetDest = _library.GetDelegateForFunction<FPDFLink_GetDest>("FPDFLink_GetDest");
            //this.FPDFPAGE_AACTION = _library.GetDelegateForFunction<FPDFPAGE_AACTION>("FPDFPAGE_AACTION");
            this.FPDFPage_GetMediaBox = _library.GetDelegateForFunction<FPDFPage_GetMediaBox>("FPDFPage_GetMediaBox");
            this.FPDFPage_SetMediaBox = _library.GetDelegateForFunction<FPDFPage_SetMediaBox>("FPDFPage_SetMediaBox");
            this.FPDFText_ClosePage = _library.GetDelegateForFunction<FPDFText_ClosePage>("FPDFText_ClosePage");
            this.FPDFText_FindClose = _library.GetDelegateForFunction<FPDFText_FindClose>("FPDFText_FindClose");
            this.FPDFText_FindNext = _library.GetDelegateForFunction<FPDFText_FindNext>("FPDFText_FindNext");
            this.FPDFText_FindStart = _library.GetDelegateForFunction<FPDFText_FindStart>("FPDFText_FindStart");
            this.FPDFText_GetCharBox = _library.GetDelegateForFunction<FPDFText_GetCharBox>("FPDFText_GetCharBox");
            this.FPDFText_GetSchCount = _library.GetDelegateForFunction<FPDFText_GetSchCount>("FPDFText_GetSchCount");
            this.FPDFText_GetSchResultIndex = _library.GetDelegateForFunction<FPDFText_GetSchResultIndex>("FPDFText_GetSchResultIndex");
            this.FPDFText_GetText = _library.GetDelegateForFunction<FPDFText_GetText>("FPDFText_GetText");
            this.FPDFText_LoadPage = _library.GetDelegateForFunction<FPDFText_LoadPage>("FPDFText_LoadPage");


        }

        #endregion

       
        

    }
}
