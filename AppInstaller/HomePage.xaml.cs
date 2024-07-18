using System.Windows;
using System.Windows.Controls;

namespace AppInstaller
{
    public partial class Home : Page
    {
        private MainWindow window;
        private Configuration configuration;

        public Home(MainWindow window, Configuration configuration)
        {
            this.window = window;
            this.configuration = configuration;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.Navigate(NavPage.PATHS);
        }
    }
}
