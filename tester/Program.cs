using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfiumViewerHB.Processor;
using PdfiumViewerHB;
using System.Runtime.InteropServices;

namespace tester
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfiumProcessor pp = new PdfiumProcessor();
            var pd = PdfDocument.Load(@"E:\PDF_TEST\pdfs\2928.pdf");
            var pc = pd.PageCount;

            //testnonstatic tns = new testnonstatic();
            //var i = tns.yo(@"E:\PDF_TEST\PdfiumViewerHBproclib\tester\bin\Debug\pdfium.dll");
        }
    }


    //public class testnonstatic
    //{
    //   [DllImport("kernel32.dll")]
    //   public static extern IntPtr LoadLibrary(string lpLibFileName);

    //    public IntPtr yo(string xx)
    //    {
    //        return LoadLibrary(xx);
    //    }
    
    //}

}
