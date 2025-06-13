using System.Windows;

namespace AppInstaller
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Configuration config = Configuration.Load();

        public MainWindow()
        {
            InitializeComponent();
            Navigate(NavPage.HOME);
        }

        public void Navigate(NavPage page) {
            switch (page)
            {
                case NavPage.HOME:
                    frame.Content = new Home(this, config);
                    break;
                case NavPage.PATHS:
                    frame.Content = new PathsPage(this, config);
                    break;
                case NavPage.REQUIRES:
                    frame.Content = new RequiresPage(this, config);
                    break;
                case NavPage.INSTALLATION:
                    InstallationPage installationPage = new InstallationPage(this, config);
                    frame.Content = installationPage;

                    installationPage.StartInstallation();
                    break;
            }
        }
    }
}
