
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
            mainUI.AddSwitchUIButtonHandler(switchUIButton_Click);
            mainUI.AddUserListSelectionChangedHandler(userList_SelectionChanged);

            //Show view(s)
            mainUI.Show();
        }

        private void handleTestDBButton(object sender, RoutedEventArgs e)
        {
            handler.testConnection();
        }

        private void switchUIButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Switching user interface");
            bool adminUIAlreadyEnabled = mainUI.adminControls.IsEnabled ? true : false;
            if (!adminUIAlreadyEnabled)
            {
                mainUI.userControls.IsEnabled = false;
                mainUI.userControls.Visibility = Visibility.Collapsed;
                ThemeManager.ChangeTheme(mainUI, new Accent("Orange", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Orange.xaml")), Theme.Light);
                populateUserList();
                mainUI.adminControls.IsEnabled = true;
                mainUI.adminControls.Visibility = Visibility.Visible;
                mainUI.Title = "JLLSco Administrator Options";
            }
            else
            {
                mainUI.adminControls.IsEnabled = false;
                mainUI.adminControls.Visibility = Visibility.Collapsed;
                ThemeManager.ChangeTheme(mainUI, new Accent("Purple", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Purple.xaml")), Theme.Light);
                mainUI.userControls.IsEnabled = true;
                mainUI.userControls.Visibility = Visibility.Visible;
                mainUI.Title = "JLLSco Hairdressing";
            }
        }

        private void userList_SelectionChanged(object sender, RoutedEventArgs e)
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
        }

        private void populateUserList() {
                    mainUI.UserList.Items.Clear();
                    ArrayList names = handler.getUserList();
                    for (int i = 0; i < names.Count; i++) {
                        mainUI.UserList.Items.Add(names[i].ToString());
                    }
                }
        }
    }
