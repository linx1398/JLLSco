using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Npgsql;

namespace JLLSco.Models
{
    public interface DBHandler
    {
        void connectToDB();
        void testConnection();
        void sendQuery(string request);
        string getResponse();
    }

}
