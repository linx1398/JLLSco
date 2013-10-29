using MahApps.Metro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace JLLSco.Controllers
{
    public partial class App : Application
    {
        private Views.MainUI mainUI;
        Models.RemoteDBHandler handler = new Models.RemoteDBHandler();

        public App()
            : base()
        {
            //Create long-lived objects
            mainUI = new Views.MainUI();

            //Wire up event handlers
            mainUI.AddTestDBButtonHandler(handleTestDBButton);
            mainUI.AddSwitchToAdminUIButtonHandler(handleSwitchToAdminUIButton_Click);
            mainUI.AddUserListSelectionChangedHandler(handleUserList_SelectionChanged);
            mainUI.AddSwitchToUserUIButtonHandler(handleSwitchToUserUIButton_Click);
            mainUI.AddCreateUserButtonHandler(handleCreateUserButton_Click);
            mainUI.AddDeletedUserHandler(handleDeleteUser_Click);
            mainUI.AddExpanderHairdresserHandler(handleHairdresserExpand);

            //Show view(s)
            mainUI.Show();
        }

        private void handleTestDBButton(object sender, RoutedEventArgs e)
        {
            handler.testConnection();
        }

        private void handleCreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainUI.firstName.Text != "" && mainUI.lastName.Text != "" && mainUI.email.Text != "" && mainUI.phone.Text != "" && mainUI.type.Text != "")
            {
                string fname = mainUI.firstName.Text;
                string lname = mainUI.lastName.Text;
                string userEmail = mainUI.email.Text;
                string phoneNo = mainUI.phone.Text;
                string userType = "";
                if (mainUI.type.Text == "Admin")
                {
                    userType = "a";
                }
                else if (mainUI.type.Text == "Hairdresser")
                {
                    userType = "h";
                }
                else
                {
                    userType = "u";
                }

                handler.addNewUser(fname, lname, userEmail, "test", phoneNo, userType);
                populateUserList();
                clearForm();
            }
        }

        private void handleSwitchToAdminUIButton_Click(object sender, RoutedEventArgs e)
        {
            mainUI.userControls.IsEnabled = false;
            mainUI.userControls.Visibility = Visibility.Collapsed;
            ThemeManager.ChangeTheme(mainUI, new Accent("Orange", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Orange.xaml")), Theme.Light);
            if (mainUI.adminControls.IsEnabled == false)
            {
                mainUI.adminControls.IsEnabled = true;
                populateUserList();
            }
            mainUI.adminControls.Visibility = Visibility.Visible;
            mainUI.Title = "JLLSco Administrator Options";
            mainUI.switchToAdminUIButton.Visibility = Visibility.Collapsed;
            mainUI.switchToUserUIButton.Visibility = Visibility.Visible;
        }

        private void handleSwitchToUserUIButton_Click(object sender, RoutedEventArgs e)
        {
            mainUI.adminControls.Visibility = Visibility.Collapsed;
            ThemeManager.ChangeTheme(mainUI, new Accent("Purple", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Purple.xaml")), Theme.Light);
            mainUI.userControls.IsEnabled = true;
            mainUI.userControls.Visibility = Visibility.Visible;
            mainUI.Title = "JLLSco Hairdressing";
            mainUI.switchToUserUIButton.Visibility = Visibility.Collapsed;
            mainUI.switchToAdminUIButton.Visibility = Visibility.Visible;
        }

        private void handleUserList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = mainUI.UserList.SelectedItem.ToString();
                string[] nArray = name.Split();
                string fName = nArray[0];
                string sName = nArray[1];
                ArrayList details = handler.getUserDetails(fName, sName);

                mainUI.firstName.Text = details[0].ToString();
                mainUI.lastName.Text = details[1].ToString();
                mainUI.email.Text = details[2].ToString();
                mainUI.phone.Text = details[3].ToString();
                string t = details[4].ToString();

                switch (t)
                {
                    case "a":
                        mainUI.type.Text = "Admin";
                        break;
                    case "h":
                        mainUI.type.Text = "Hairdresser";
                        break;
                    case "u":
                        mainUI.type.Text = "User";
                        break;
                    default:
                        break;


                }


            }
            catch (NullReferenceException n)
            {

                Debug.WriteLine("Null reference");

            }

        }

        private void handleDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (mainUI.UserList.SelectedValue.ToString() != "")
            {
                handler.deleteUser(mainUI.email.Text);
                //mainUI.UserList.SelectedIndex = 0;
                populateUserList();
                clearForm();
            }



        }

        private void clearForm()
        {
            mainUI.firstName.Text = "";
            mainUI.lastName.Text = "";
            mainUI.email.Text = "";
            mainUI.phone.Text = "";
            mainUI.type.Text = "";

        }

        private void populateUserList()
        {
            mainUI.UserList.Items.Clear();
            ArrayList users = handler.getUserList();
            for (int i = 0; i < users.Count; i++)
            {
                mainUI.UserList.Items.Add(users[i].ToString());
            }
        }

        private void handleHairdresserExpand(object sender, RoutedEventArgs e)
        {

            mainUI.hairdresserList.Items.Clear();
            ArrayList hairdressers = handler.getUserList("h");
            for (int i = 0; i < hairdressers.Count; i++)
            {
                mainUI.hairdresserList.Items.Add(hairdressers[i].ToString());
            }
        }
    }
}
