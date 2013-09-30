using MahApps.Metro.Controls;
using System.Windows;

namespace JLLSco
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
    }
}