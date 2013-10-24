using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows;

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

        private void testDBButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void AddAdminButtonHandler(RoutedEventHandler handler) {

            adminButton.Click += handler;
        }




    }
}