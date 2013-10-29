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
        public MainUI()
        {
            InitializeComponent();
        }

        public void AddTestDBButtonHandler(RoutedEventHandler handler)
        {
            testDBButton.Click += handler;
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

        public void AddExpanderHairdresserHandler(RoutedEventHandler handler) {
            expanderHairdresser.Expanded += handler;
        }
    }
}