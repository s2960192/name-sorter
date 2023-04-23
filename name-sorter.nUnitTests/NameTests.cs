using System.Data.SqlClient;

namespace name_sorter.nUnitTests
{
    public class NameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SqlClientTests()
        {
            string connectionString = "Data Source=BOBBIE_BOT\\MSSQLSERVER01;Initial Catalog=D&D;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string query = "SELECT TOP 1 [FirstName],[GivenName2],[GivenName3],[LastName] FROM [dbo].[nameList]";
                       
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            
            SqlCommand command = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
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

                    Console.WriteLine(String.Format("{0}{1}{2}{3}", firstName, givenName2, givenName3, lastName));
                }
            }
            finally
            {
                reader.Close();
                //myFile.Close();
            }

            Assert.Pass();
        }
    }
}