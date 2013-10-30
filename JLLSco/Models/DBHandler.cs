using System.Collections;

namespace JLLSco.Models
{
    public interface DBHandler
    {
        void connectToDB();
        void testConnection();
        void sendQuery(string request);
        string getResponse();
        ArrayList getUserList();
        ArrayList getUserList(string type);
        ArrayList getUserDetails(string fName, string sName);
        void deleteUser(string email);
        void addNewUser(string fName, string sName, string email, string pass, string phone, string type);
        ArrayList findAvailability(string email, string date, string time);
        string getEmailFromName(string fname, string sname);
        void updateAvailability(string email, string date, string time, string avail);
        ArrayList getAvailability(string email, string date);
    }

}
