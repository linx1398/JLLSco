using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace JLLSco
{
    public class DBHandler
    {
        private static MySqlConnection connection;
        private static string server, database, uid, password;

        public static void testConnection()
        {
            server = "jdbc:mysql://jllscohair.db.11636855.hostedresource.com:3306";
            database = "jllscohair";
            uid = "jllscohair";
            password = "Passw0rd132!";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                }
                catch
                {
                    Debug.WriteLine("Connection did not open");
                }
                Debug.WriteLine("Worked?");
            }
            catch
            {
                Debug.WriteLine("Failure");
            }
        }

    }

}
