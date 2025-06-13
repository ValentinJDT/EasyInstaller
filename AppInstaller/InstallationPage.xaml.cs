using System;
using System.Windows;
using System.Windows.Controls;

namespace AppInstaller
{
    /// <summary>
    /// Logique d'interaction pour InstallationPage.xaml
    /// </summary>
    public partial class InstallationPage : Page
    {
        private MainWindow window;
        private Configuration configuration;

        public InstallationPage(MainWindow window, Configuration configuration)
        {
            this.window = window;
            this.configuration = configuration;
            InitializeComponent();

            this.configuration.RequireProgressEventHandler += Configuration_RequireProgressEventHandler;
            this.configuration.ContentProgressEventHandler += Configuration_ContentProgressEventHandler;
        }

        public void StartInstallation()
        {
            this.configuration.InstallRequires();
            this.configuration.Install();
        }

        private void Configuration_ContentProgressEventHandler(object sender, ProgressEventArgs e)
        {
            contentBar.Dispatcher.Invoke(() =>
            {
                contentBar.Value = e.percentage;
            });
        }

        private void Configuration_RequireProgressEventHandler(object sender, ProgressEventArgs e)
        {
            requiredBar.Dispatcher.Invoke(() =>
            {
                requiredBar.Value = e.percentage;
            });
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.configuration.RequireProgressEventHandler -= Configuration_RequireProgressEventHandler;
            this.configuration.ContentProgressEventHandler -= Configuration_ContentProgressEventHandler;
            window.Navigate(NavPage.REQUIRES);
        }
    }
}
