using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using CustomUtilities;
using System.Data;


namespace PDFPageCounter
{
    public class PageCounter
    {
        public Utilities util = new Utilities();
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
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("File is not a PDF");
                return 0;
            }
            return numPages;
        }

        public int CountPagesForDB(string filePath)
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

                    return 0;
                }
            }
            else
            {
                return 0;
            }
            return numPages;
        }

        public void CountPagesFromDB(DataTable dte)
        {
            dte.Columns.Add("PageCount");
            int i = 1;
            foreach (DataRow row in dte.Rows)
            {
                util.DrawProgressBar(i, dte.Rows.Count, 50, '#');
                string path = row["FileLocation"].ToString();
                int pageCount = CountPagesForDB(path);
                row["PageCount"]= pageCount;
                i++;
            }
        }
        /*
        public double CalculatePageCost(int pageCount, DataRow row)
        {
            try
            {
                if (row["IndexedBy"] == "LLN_DocIndexer02")
                {
                    return 
                }
            }
            catch
            {

            }
            return 0.0;
        }
        **/
       
    }
}
