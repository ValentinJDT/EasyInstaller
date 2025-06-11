using System;
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

            title.Content = configuration.title;
            author.Content = $"Créé par {configuration.author}";
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            window.Navigate(NavPage.PATHS);
        }

        private void Button_Click_Quit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
