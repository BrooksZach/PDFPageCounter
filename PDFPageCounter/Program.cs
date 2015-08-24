using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace PDFPageCounter
{
    class Program
    {
        public Dictionary<string, int> pageCount = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            DateTime Start = new DateTime(2015, 7, 01);
            DateTime End =  new DateTime(2015, 7, 02);
            DataTable PDFs = SQLHandler.getListOfPDFs(Start, End);

            Console.WriteLine(PDFs.Rows[0].ItemArray[0]);
            Console.ReadKey();
        }
    }
}
