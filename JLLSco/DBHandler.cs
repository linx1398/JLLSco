using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace JLLSco
{
    class DBHandler
    {
        private static MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;


        //Initialize values
        public static void testConnection()
        {
            string server = "jdbc:mysql://jllscohair.db.11636855.hostedresource.com:3306",
            database = "jllscohair",
            uid = "jllscohair",
            password = "Passw0rd132!",
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                connection = new MySqlConnection(connectionString);
                Debug.WriteLine("Worked?");
            }
            catch
            {
                Debug.WriteLine("Failure");
            }
        }

    }

}
