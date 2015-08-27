using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;


namespace PDFPageCounter
{
    public class PageCounter
    {
        public int CountPages(string filePath)
        {
            int numPages = 0;
            if (filePath.EndsWith(".pdf"))
            {
                try
                {
                    PdfReader pdfReader = new PdfReader(filePath);
                    numPages = pdfReader.NumberOfPages;
                }
                catch
                {
                    Console.WriteLine("Error Loading File. Make sure it is not an Encrypted PDF");
                    Console.ReadKey();
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("File is not a PDF");
                Console.ReadKey();
                return 0;
            }
            return numPages;
        }
    }
}
