using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using CustomUtilities;

namespace PDFPageCounter
{
    class Program
    {

        public Utilities util = new Utilities();
        static int operationMode = 0;

        static void Main(string[] args)
        {

            ConfigureProgramOperationMode();
            RunOp();

        }

        static void ConfigureProgramOperationMode()
        {
            Console.WriteLine("Welcome to the PDF Page Counter. Please Select the Operation mode to proceed:");
            Console.WriteLine("1) Count Pages From a Database");
            Console.WriteLine("2) Count Pages From a Single PDF");
            bool choice = false;

            while (!choice)
            {
                var crk = Console.ReadKey(true);
                switch (crk.KeyChar)
                {
                    case (char)'1':
                        operationMode = 1;
                        choice = true;
                        break;
                    case (char)'2':
                        operationMode = 2;
                        choice = true;
                        break;

                }
            }
        }

        static void RunOp()
        {
            PageCounter counter = new PageCounter();
            if (operationMode == 1)
            {
                DataTableHandler dteHandler = new DataTableHandler();
                DateTime Start = new DateTime(2015, 7, 01);
                DateTime End = new DateTime(2015, 7, 02);
                DataTable PDFs = SQLHandler.getListOfPDFs(Start, End);
                counter.CountPagesFromDB(PDFs);
                Console.WriteLine("Enter the path and name of the file");
                string fullPath = Console.ReadLine();
                dteHandler.loadExcel(PDFs, fullPath);


                Console.WriteLine("File Written");
            }
            else if (operationMode == 2)
            {
                Console.WriteLine("Enter the fullpath of a pdf file: ");
                string fullFilePath = Console.ReadLine();

                Console.WriteLine("Page count for file: " + counter.CountPages(fullFilePath));
                Console.ReadKey();
            }
        }
    }
}
