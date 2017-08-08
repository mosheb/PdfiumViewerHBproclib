# PdfiumViewerHBproclib
fork of PdfiumViewer experimenting with dynamic dll load for multithreaded use

PDFium.dll is not threadsafe, as can be seen in the c# wrapper, each function call is locked.
When testing with parallel.foreach, the process will eventually lock up.

This project is an experiment using the dynamic library loader from https://github.com/jhabjan/Ghostscript.NET modified to target pdfium.dll instead of ghostscript.dll

Incomplete at this time.
