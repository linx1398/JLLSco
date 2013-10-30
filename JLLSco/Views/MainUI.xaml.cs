using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JLLSco.Views
{
    public partial class MainUI : MetroWindow
    {
        public MainUI()
        {
            InitializeComponent();
        }

        public void AddSwitchToAdminUIButtonHandler(RoutedEventHandler handler) 
        {
            switchToAdminUIButton.Click += handler;
        }

        public void AddSwitchToUserUIButtonHandler(RoutedEventHandler handler)
        {
            switchToUserUIButton.Click += handler;
        }

        public void AddUserListSelectionChangedHandler(SelectionChangedEventHandler handler)
        {
            UserList.SelectionChanged += handler;
        }

        public void addTimeSlotHandler(RoutedEventHandler handler) 
        {
            nine.Click += handler;
            ninethirty.Click += handler;
            ten.Click += handler;
            tenthirty.Click += handler;
            eleven.Click += handler;
            eleventhirty.Click += handler;
            twelve.Click += handler;
            twelvethirty.Click += handler;
            one.Click += handler;
            onethirty.Click += handler;
            two.Click += handler;
            twothirty.Click += handler;
            three.Click += handler;
            threethirty.Click += handler;
            four.Click += handler;
            fourthirty.Click += handler;
            five.Click += handler;
        }

        public void AddCreateUserButtonHandler(RoutedEventHandler handler)
        {
            createUserButton.Click += handler;
        }

        private void openLoginInformationWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginInformationWindow loginInformationWindow = new LoginInformationWindow();
            loginInformationWindow.Owner = this;
            loginInformationWindow.ShowDialog();
        }

        public void AddDeletedUserHandler(RoutedEventHandler handler)
        {
            deleteUserBttn.Click += handler;
        }

        public void AddTabChangeHandler(MouseButtonEventHandler handler)
        {
            availableTab.MouseLeftButtonUp += handler;
        }

        public void AddTabEditUserHandler(MouseButtonEventHandler handler)
        {
            editUsers.MouseLeftButtonUp += handler;
        }

        public void addCalanderHandler() 
        {
            calendar.SelectedDatesChanged +=calendar_SelectedDatesChanged;
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDay.Visibility = Visibility.Visible;
            timeSlots.Visibility = Visibility.Visible;
            DateTime date = (DateTime)calendar.SelectedDate;
            currentDay.Content = date.DayOfWeek.ToString();
        }

        public void addApplyHandler(RoutedEventHandler handler) 
        {
            applyAvailable.Click += handler;        
        }

        public void addAppointmentsHandler(RoutedEventHandler handler) 
        {
            nine1.Click += handler;
            ninethirty1.Click += handler;
            ten1.Click += handler;
            tenthirty1.Click += handler;
            eleven1.Click += handler;
            eleventhirty1.Click += handler;
            twelve1.Click += handler;
            twelvethirty1.Click += handler;
            one1.Click += handler;
            onethirty1.Click += handler;
            two1.Click += handler;
            twothirty1.Click += handler;
            three1.Click += handler;
            threethirty1.Click += handler;
            four1.Click += handler;
            fourthirty1.Click += handler;
            five1.Click += handler;        
        }

        public void addFindAppointHandler(RoutedEventHandler handler) {
            FindAvail.Click += handler;
        }

   




     








    }
}