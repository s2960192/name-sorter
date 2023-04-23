using name_sorter;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;

namespace name_sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Grabs the connection string from App.Config
            //string connString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
            // Switched to the below so that users can easily point at their Data Source
            string connString = "Data Source=BOBBIE_BOT\\MSSQLSERVER01;Initial Catalog=D&D;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //Console.WriteLine("connString = " + connString);

            string path = Path.Combine(Directory.GetCurrentDirectory());

            Name.Delete_TruncateAllTables(connString);
            Name.Insert_stagingTableRecordsFromTextFile(connString, path);
            Name.Insert_nameListRecordsFromStagingTable(connString);
            Name.Upadate_nameListRecordsOrder(connString);
            Name.WriteOutputToFile(connString, path);
            Name.PrintNamesToConsole(connString);
        }
    }
}