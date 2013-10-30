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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace JLLSco.Controllers
{
    public partial class App : Application
    {
        private Views.MainUI mainUI;
        Models.RemoteDBHandler handler = new Models.RemoteDBHandler();
        private bool runOnce = false;
        private string selectedTime = "";

        public App()
            : base()
        {
            //Create long-lived objects
            mainUI = new Views.MainUI();

            //Wire up event handlers
            mainUI.addSwitchToAdminUIButtonHandler(handleSwitchToAdminUIButton_Click);
            mainUI.addUserListSelectionChangedHandler(handleUserList_SelectionChanged);
            mainUI.addSwitchToUserUIButtonHandler(handleSwitchToUserUIButton_Click);
            mainUI.addCreateUserButtonHandler(handleCreateUserButton_Click);
            mainUI.addDeleteUserButtonHandler(handleDeleteUserButton_Click);
            mainUI.addEditUserTabHandler(handleSwitchToEditUserTabHandler);
            mainUI.addCalanderHandler();
            mainUI.addTimeSlotButtonHandler(handleTimeSlotButton_Click);
            mainUI.addApplyButtonHandler(handleApplyButton_Click);
            mainUI.addFindAppointmentButtonHandler(findApointmentsButton_Click);

            //Prepare the view(s)
            refreshHairdresserList();
            refreshHairdresserAvailabilityList();
            mainUI.timeSlots_Copy.Visibility = Visibility.Hidden;

            //Show view(s)
            mainUI.Show();
        }

        private void handleCreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainUI.viewUsers_FirstNameTextBox.Text != "" && mainUI.viewUsers_LastNameTextBox.Text != "" && mainUI.viewUsers_EmailTextBox.Text != "" && mainUI.viewUsers_PhoneTextBox.Text != "" && mainUI.viewUsers_TypeComboBox.Text != "")
            {
                string fname = mainUI.viewUsers_FirstNameTextBox.Text;
                string lname = mainUI.viewUsers_LastNameTextBox.Text;
                string userEmail = mainUI.viewUsers_EmailTextBox.Text;
                string phoneNo = mainUI.viewUsers_PhoneTextBox.Text;
                string userType = "";
                if (mainUI.viewUsers_TypeComboBox.Text == "Admin")
                {
                    userType = "a";
                }
                else if (mainUI.viewUsers_TypeComboBox.Text == "Hairdresser")
                {
                    userType = "h";
                }
                else
                {
                    userType = "u";
                }

                handler.addNewUser(fname, lname, userEmail, "test", phoneNo, userType);
                populateUserList();
                mainUI.clearForm();
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

                mainUI.viewUsers_FirstNameTextBox.Text = details[0].ToString();
                mainUI.viewUsers_LastNameTextBox.Text = details[1].ToString();
                mainUI.viewUsers_EmailTextBox.Text = details[2].ToString();
                mainUI.viewUsers_PhoneTextBox.Text = details[3].ToString();
                string t = details[4].ToString();

                switch (t)
                {
                    case "a":
                        mainUI.viewUsers_TypeComboBox.Text = "Admin";
                        break;
                    case "h":
                        mainUI.viewUsers_TypeComboBox.Text = "Hairdresser";
                        break;
                    case "u":
                        mainUI.viewUsers_TypeComboBox.Text = "User";
                        break;
                    default:
                        break;


                }


            }
            catch (NullReferenceException)
            {

                Debug.WriteLine("Null reference");

            }

        }

        private void handleDeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainUI.UserList.SelectedValue.ToString() != "")
            {
                handler.deleteUser(mainUI.viewUsers_EmailTextBox.Text);
                //mainUI.UserList.SelectedIndex = 0;
                populateUserList();
                mainUI.clearForm();
            }



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

        private void refreshHairdresserList()
        {

            mainUI.hairdresserList.Items.Clear();
            ArrayList hairdressers = handler.getUserList("h");
            for (int i = 0; i < hairdressers.Count; i++)
            {
                mainUI.hairdresserList.Items.Add(hairdressers[i].ToString());
            }
        }

        private void refreshHairdresserAvailabilityList()
        {
            if (!runOnce)
            {
                mainUI.HairDresserList.Items.Clear();
                ArrayList users = handler.getUserList("h");
                for (int i = 0; i < users.Count; i++)
                {
                    mainUI.HairDresserList.Items.Add(users[i].ToString());
                }
                runOnce = true;
                mainUI.timeSlots.Visibility = Visibility.Hidden;
                mainUI.SettingsBox.Visibility = Visibility.Hidden;
                mainUI.currentDay.Visibility = Visibility.Hidden;
            }
        }

        private void handleSwitchToEditUserTabHandler(object sender, RoutedEventArgs e)
        {
            runOnce = false;
        }

        private void handleAvailabilityChange(object sender, RoutedEventArgs e)
        {
        }

        private void handleApplyButton_Click(object sender, RoutedEventArgs e)
        {
            string[] name = mainUI.HairDresserList.SelectedValue.ToString().Split();
            string[] date = mainUI.calendar.SelectedDate.ToString().Split();
            string today = date[0];
            string avail = "";

            if (mainUI.radioAvail.IsChecked == true)
            {
                avail = "Available";
            }
            else if (mainUI.radioBooked.IsChecked == true)
            {
                avail = "Booked";

            }
            else if (mainUI.radioBreak.IsChecked == true)
            {
                avail = "On Break";
            }
            else
            {
                avail = "Unavailable";

            }
            handler.updateAvailability(handler.getEmailFromName(name[0].ToString(), name[1].ToString()), today, selectedTime, avail);
            mainUI.currentStatus.Content = avail;
        }

        private void handleTimeSlotButton_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            Debug.WriteLine(clicked.Content.ToString());
            selectedTime = clicked.Content.ToString();
            mainUI.SettingsBox.Visibility = Visibility.Visible;
            string[] name = mainUI.HairDresserList.SelectedValue.ToString().Split();
            string[] date = mainUI.calendar.SelectedDate.ToString().Split();
            string today = date[0];
            Debug.WriteLine(today);
            Debug.WriteLine(clicked.Content.ToString());
            ArrayList availability = handler.findAvailability(handler.getEmailFromName(name[0], name[1]), today, clicked.Content.ToString());

            int a = -1;
            for (int i = 0; i < availability.Count; i++)
            {
                if (availability[i].ToString() == "True")
                {
                    a = i;
                }
            }

            switch (a)
            {
                case 0:
                    Debug.WriteLine("available");
                    mainUI.radioAvail.IsChecked = true;
                    mainUI.radioBooked.IsChecked = false;
                    mainUI.radioBreak.IsChecked = false;
                    mainUI.radioUnavail.IsChecked = false;
                    mainUI.currentStatus.Content = "Current Status: Available";
                    break;
                case 1:
                    Debug.WriteLine("booked");
                    mainUI.radioAvail.IsChecked = false;
                    mainUI.radioBooked.IsChecked = true;
                    mainUI.radioBreak.IsChecked = false;
                    mainUI.radioUnavail.IsChecked = false;
                    mainUI.currentStatus.Content = "Current Status: Booked";
                    break;
                case 2:
                    Debug.WriteLine("break");
                    mainUI.radioAvail.IsChecked = false;
                    mainUI.radioBooked.IsChecked = false;
                    mainUI.radioBreak.IsChecked = true;
                    mainUI.radioUnavail.IsChecked = false;
                    mainUI.currentStatus.Content = "Current Status: Break";
                    break;
                case 3:
                    Debug.WriteLine("unavailable");
                    mainUI.radioAvail.IsChecked = false;
                    mainUI.radioBooked.IsChecked = false;
                    mainUI.radioBreak.IsChecked = false;
                    mainUI.radioUnavail.IsChecked = true;
                    mainUI.currentStatus.Content = "Current Status: Unavailable";
                    break;
                default:
                    Debug.WriteLine("NOT SET");
                    mainUI.radioAvail.IsChecked = false;
                    mainUI.radioBooked.IsChecked = false;
                    mainUI.radioBreak.IsChecked = false;
                    mainUI.radioUnavail.IsChecked = false;
                    mainUI.currentStatus.Content = "Current Status: Not Set";
                    break;
            }



        }

        private void findApointmentsButton_Click(object sender, RoutedEventArgs e) { 
            string[] name = mainUI.hairdresserList.SelectedValue.ToString().Split();
            string email = handler.getEmailFromName(name[0].ToString(), name[1].ToString());
            string[] date = mainUI.PickAppointment.SelectedDate.ToString().Split();
            string today = date[0];
            ArrayList available = handler.getAvailability(email, today);

            if (available.Contains("9am")) {
                mainUI.nine1.IsEnabled = false;
            }
            if (available.Contains("930am"))
            {
                mainUI.ninethirty1.IsEnabled = false;
            }
            if (available.Contains("10am"))
            {
                mainUI.ten1.IsEnabled = false;
            }
            if (available.Contains("1030am"))
            {
                mainUI.tenthirty1.IsEnabled = false;
            }
            if (available.Contains("11am"))
            {
                mainUI.eleven1.IsEnabled = false;
            }
            if (available.Contains("1130am"))
            {
                mainUI.eleventhirty1.IsEnabled = false;
            }
            if (available.Contains("12pm"))
            {
                mainUI.twelve1.IsEnabled = false;
            }
            if (available.Contains("1230pm"))
            {
                mainUI.twelvethirty1.IsEnabled = false;
            }
            if (available.Contains("1pm"))
            {
                mainUI.one1.IsEnabled = false;
            }
            if (available.Contains("130pm"))
            {
                mainUI.onethirty1.IsEnabled = false;
            }
            if (available.Contains("2pm"))
            {
                mainUI.two1.IsEnabled = false;
            }
            if (available.Contains("230pm"))
            {
                mainUI.twothirty1.IsEnabled = false;
            }
            if (available.Contains("3pm"))
            {
                mainUI.three1.IsEnabled = false;
            }
            if (available.Contains("330pm"))
            {
                mainUI.threethirty1.IsEnabled = false;
            }
            if (available.Contains("4pm"))
            {
                mainUI.four1.IsEnabled = false;
            }
            if (available.Contains("430pm"))
            {
                mainUI.fourthirty1.IsEnabled = false;
            }
            if (available.Contains("5pm"))
            {
                mainUI.five1.IsEnabled = false;
            }
            mainUI.timeSlots_Copy.Visibility = Visibility.Visible;
        }
    }
}
