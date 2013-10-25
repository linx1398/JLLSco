
using System;
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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Views.MainUI mainUI;
        private Views.AdminUI adminUI;
        Models.RemoteDBHandler handler = new Models.RemoteDBHandler();

        public App()
            : base()
        {
            //Create long-lived objects
            mainUI = new Views.MainUI();

            //Wire up event handlers
            mainUI.AddTestDBButtonHandler(HandleTestDBButton);
            mainUI.AddAdminButtonHandler(adminButton_Click);

            //Show view(s)
            mainUI.Show();
        }

        private void HandleTestDBButton(object sender, RoutedEventArgs e)
        {
            handler.testConnection();
        }

        void adminButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("CLICK");
            adminUI = new Views.AdminUI();
            adminUI.Owner = mainUI;
            adminUI.Show();
            
        }


    }
}
