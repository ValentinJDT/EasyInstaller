using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
