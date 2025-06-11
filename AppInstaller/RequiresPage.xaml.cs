using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppInstaller
{
    /// <summary>
    /// Logique d'interaction pour RequiresPage.xaml
    /// </summary>
    public partial class RequiresPage : Page
    {
        private MainWindow window;
        private Configuration configuration;

        public RequiresPage(MainWindow window, Configuration configuration)
        {
            this.window = window;
            this.configuration = configuration;
            InitializeComponent();

            this.requiredList.ItemsSource = this.configuration.requires.Select(t => t.name).ToList();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            window.Navigate(NavPage.PATHS);
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            window.Navigate(NavPage.INSTALLATION);
        }

    }
}
