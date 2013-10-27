using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace JLLSco.Views
{
    public partial class MainUI : MetroWindow
    {

        Models.RemoteDBHandler handler = new Models.RemoteDBHandler();

        public MainUI()
        {
            InitializeComponent();
        }

        public void AddTestDBButtonHandler(RoutedEventHandler handler)
        {
            testDBButton.Click += handler;
        }

        public void AddSwitchUIButtonHandler(RoutedEventHandler handler) 
        {
            switchUIButton.Click += handler;
        }

        public void AddUserListSelectionChangedHandler(SelectionChangedEventHandler handler)
        {
            UserList.SelectionChanged += handler;
        }

        private void creatUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != "" && lastName.Text != "" && email.Text != "" && phone.Text != "" && type.Text != "")
            {
                string fname = firstName.Text;
                string lname = lastName.Text;
                string userEmail = email.Text;
                string phoneNo = phone.Text;
                string userType = "";
                if (type.Text == "Admin")
                {
                    userType = "a";
                }
                else if (type.Text == "Hairdresser")
                {
                    userType = "h";
                }
                else
                {
                    userType = "u";
                }

                handler.addNewUser(fname, lname, userEmail, "test", phoneNo, userType);
                populateUserList();

            }
        }

        private void populateUserList()
        {
            UserList.Items.Clear();
            ArrayList names = handler.getUserList();
            for (int i = 0; i < names.Count; i++)
            {
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
    }
}