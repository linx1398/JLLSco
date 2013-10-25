using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JLLSco.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdminUI : MetroWindow
    {
        Models.RemoteDBHandler handler = new Models.RemoteDBHandler();

        public AdminUI()
        {
            InitializeComponent();
            populateUserList();

        }

        public void AddOpenUserUIButtonHandler(RoutedEventHandler handler)
        {
            openUserUIButton.Click += handler;
        }

        private void creatUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != "" && lastName.Text != "" && email.Text !="" && phone.Text != "" && type.Text !="") {
                string fname = firstName.Text;
                string lname = lastName.Text;
                string userEmail = email.Text;
                string phoneNo = phone.Text;
                string userType = "";
                if (type.Text == "Admin") {
                    userType = "a";
                }
                else if (type.Text == "Hairdresser")
                {
                    userType = "h";
                }
                else {
                    userType = "u";
                }

                handler.addNewUser(fname, lname, userEmail, "test", phoneNo, userType);
                populateUserList();
                
            }
        }



        private void populateUserList() {
            UserList.Items.Clear();
            ArrayList names = handler.getUserList();
            for (int i = 0; i < names.Count; i++) {
                UserList.Items.Add(names[i].ToString());
            }
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = UserList.SelectedItem.ToString();
            string[] nArray = name.Split();
            string fName = nArray[0];
            string sName = nArray[1];
            ArrayList details = handler.getUserDetails(fName, sName);

            firstName.Text = details[0].ToString();
            lastName.Text = details[1].ToString();
            email.Text = details[2].ToString();
            phone.Text = details[3].ToString();
        }

        private void openUserUIButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
