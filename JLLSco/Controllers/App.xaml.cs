using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public App()
            : base()
        {
            //Create long-lived objects
            mainUI = new Views.MainUI();

            //Wire up event handlers
            mainUI.AddTestDBButtonHandler(HandleTestDBButton);

            //Show view(s)
            mainUI.Show();
        }

        private void HandleTestDBButton(object sender, RoutedEventArgs e)
        {
            Models.DBHandler.testConnection();
        }


    }
}
