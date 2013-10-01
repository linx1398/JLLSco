using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Npgsql;

namespace JLLSco.Models
{
    public class DBHandler
    {


        private static NpgsqlConnection connection;
        private static string server, databaseName, uid, password, port;

        public static void testConnection()
        {
            server = "ec2-184-73-162-34.compute-1.amazonaws.com:5432";
            databaseName = "d8gkfgn82k83n1";
            uid = "jeuhypnnmpolvm";
            password = "X5SJdA_McTmIGR2tewHGxCLe4M";
            port = "5432";

            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", 
                server, port, uid, password, databaseName);

            Debug.WriteLine(connectionString);
            try
            {
                connection = new NpgsqlConnection(connectionString);
                Debug.WriteLine("Connection string was accepted.");
                try
                {
                    connection.Open();
                }
                catch
                {
                    Debug.WriteLine("Connection did not open.");
                }
            }
            catch
            {
                Debug.WriteLine("Connection String was not accepted");
            }
        }

    }

}
