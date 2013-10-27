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
    }
}