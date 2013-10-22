using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Npgsql;

namespace JLLSco.Models
{
    class RemoteDBHandler
        : DBHandler
    {
        public void connectToDB()
        {
            throw new NotImplementedException();
        }

        public void sendQuery(string request)
        {
            throw new NotImplementedException();
        }

        public string getResponse()
        {
            throw new NotImplementedException();
        }

        public void testConnection()
        {
            NpgsqlConnection connection;
            //Build connection string
            string server, databaseName, uid, password, port, ssl, sslFactory;
            server = "ec2-184-73-162-34.compute-1.amazonaws.com";
            databaseName = "d8gkfgn82k83n1";
            uid = "jeuhypnnmpolvm";
            password = "X5SJdA_McTmIGR2tewHGxCLe4M";
            port = "5432";
            ssl = "true";
            sslFactory = "org.postgresql.ssl.NonValidatingFactory";

            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};ssl={5}",
                server, port, uid, password, databaseName, ssl);

            Debug.WriteLine(connectionString);
            try
            {
                connection = new NpgsqlConnection(connectionString);
                Debug.WriteLine("Connection string was accepted.");
                try
                {
                    connection.Open();
                    Debug.WriteLine("Connection Succesful");
                    NpgsqlCommand command = new NpgsqlCommand("select * from users", connection);
                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            Debug.WriteLine("{0} \t", dr[i]);
                        }
                        
                    }
                }
                catch (NpgsqlException msg)
                {
                    Debug.WriteLine("Connection did not open: " + msg);
                    Debug.WriteLine(connection.ToString());
                }
            }
            catch
            {
                Debug.WriteLine("Connection String was not accepted");
            }
        }
    }

}
