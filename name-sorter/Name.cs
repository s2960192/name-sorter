using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_sorter
{
    internal class Name
    {
        public static void Insert_stagingTableRecordsFromTextFile(string connectionString, string path)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine(sqlConnection.ToString() + " Connection to database successful \n");

            SqlCommand command = new SqlCommand("dbo.updStagingTable", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@piPath", SqlDbType.VarChar).Value = path;

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static void Insert_nameListRecordsFromStagingTable(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("dbo.updNameList", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static void Upadate_nameListRecordsOrder(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("dbo.updOrderNameList", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static void WriteOutputToFile(string connectionString, string path)
        {
            string query = "SELECT [FirstName],[GivenName2],[GivenName3],[LastName] FROM [dbo].[nameList]";

            StreamWriter myFile = new StreamWriter(path + @"\sorted-names-list.txt");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        string firstName = "";
                        string givenName2 = "";
                        string givenName3 = "";
                        string lastName = "";

                        if (!String.IsNullOrEmpty(reader[0].ToString())) { firstName = Convert.ToString(reader[0].ToString() + " "); };
                        if (!String.IsNullOrEmpty(reader[1].ToString())) { givenName2 = Convert.ToString(reader[1].ToString() + " "); };
                        if (!String.IsNullOrEmpty(reader[2].ToString())) { givenName3 = Convert.ToString(reader[2].ToString() + " "); };
                        if (!String.IsNullOrEmpty(reader[3].ToString())) { lastName = Convert.ToString(reader[3].ToString() + " "); };

                        myFile.WriteLine(String.Format("{0}{1}{2}{3}", firstName, givenName2, givenName3, lastName));
                    }
                }
                finally
                {
                    reader.Close();
                    myFile.Close();
                }
            }

        }

        public static void PrintNamesToConsole(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("selNameList", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                //if (!String.IsNullOrEmpty(dataReader[0].ToString())) { Console.Write(dataReader[0].ToString() + " "); };
                if (!String.IsNullOrEmpty(dataReader[1].ToString())) { Console.Write(dataReader[1].ToString() + " "); };
                if (!String.IsNullOrEmpty(dataReader[2].ToString())) { Console.Write(dataReader[2].ToString() + " "); };
                if (!String.IsNullOrEmpty(dataReader[3].ToString())) { Console.Write(dataReader[3].ToString() + " "); };
                Console.WriteLine(dataReader[4].ToString());

            }
            Console.Read();

            dataReader.Close();
            sqlConnection.Close();
        }

        public static void Delete_TruncateAllTables(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("dbo.delTruncateTables", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}
