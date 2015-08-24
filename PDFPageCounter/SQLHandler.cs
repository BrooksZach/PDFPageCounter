using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PDFPageCounter
{
    public class SQLHandler
    {
        private static string ConnectionString = @"Server=LLC1GPPSQL04;Database=DocMgmt;User ID=SRT_SA;Password=s8jZebjNyMUsGAyKVFeJ;";

        public static DataTable getListOfPDFs(DateTime StartDate, DateTime EndDate)
        {
            DataTable PDFList = new DataTable();
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConnectionString;
            SqlCommand cmd = new SqlCommand(PullPDFS, Conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            cmd.Parameters.Add("@Start", SqlDbType.DateTime);
            cmd.Parameters.Add("@End", SqlDbType.DateTime);
            cmd.Parameters["@Start"].Value = StartDate.ToString("MM/dd/yyyy");
            cmd.Parameters["@End"].Value = EndDate.ToString("MM/dd/yyyy");

            adp.Fill(PDFList);

            return PDFList;
        }

        private static string PullPDFS = @"
  with NoAutoIndex as
  (
  select
  d.dateadded,
  d.FileLocation,
  d.DocumentID, 
  substring(d.FileLocation, charindex('\' ,substring(d.FileLocation, 41, 6)) + 43, 6) LoanID,
  c.Name,
  d.IndexedBy
  from Documents d
  inner join Applications..Loans l on l.LoanID =   substring(d.FileLocation, charindex('\' ,substring(d.FileLocation, 41, 6)) + 43, 6)
  inner join Programs..Clients c on l.ClientID = c.ClientID
  where dateadded between @Start and @End
  and d.IndexedBy not like 'LLN_Doc%'
  ) , YesAutoIndex as
  (
  select
  d.Dateadded,
  d.FileLocation,
  d.DocumentID, 
  substring(d.FileLocation, charindex('\' ,substring(d.FileLocation, 41, 6)) + 43, 6) LoanID,
  c.Name + ' AutoIndexed' as Name,
  d.IndexedBy
  from Documents d
  inner join Applications..Loans l on l.LoanID =   substring(d.FileLocation, charindex('\' ,substring(d.FileLocation, 41, 6)) + 43, 6)
  inner join Programs..Clients c on l.ClientID = c.ClientID
  where dateadded between @Start and @End
  and d.IndexedBy like 'LLN_Doc%'
  ), Part1 as(

  select * from NoAutoIndex
  union all
  select * from YesAutoIndex
  ), Part2 as
  (
       select
       LoanID,
       'Originated' as origin
       from Applications..WorkQueue
       Where ItemNote like 'Purch%'
  )
  select 
  p.*,
  p2.origin
  from Part1 p
   left join Part2 p2 on p.LoanId = p2.LoanID
  where name like 'ZFC%' or name like '%CITI%' or name like '%deeph%' ";
    }
}
