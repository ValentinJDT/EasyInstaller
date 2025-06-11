using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppInstaller
{
    /// <summary>
    /// Logique d'interaction pour PathsPage.xaml
    /// </summary>
    public partial class PathsPage : Page
    {
        private MainWindow window;
        private Configuration configuration;

        public PathsPage(MainWindow window, Configuration configuration)
        {
            this.window = window;
            this.configuration = configuration;
            InitializeComponent();
            this.defaultWorkdir.Text = configuration.defaultWorkdir;
            this.tempDir.Text = configuration.tempDirectory;
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            window.Navigate(NavPage.REQUIRES);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            window.Navigate(NavPage.HOME);
        }

        private void changeDir_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    defaultWorkdir.Text = fbd.SelectedPath;
                }
            }
        }

        private void defaultWorkdir_TextChanged(object sender, TextChangedEventArgs e)
        {
            configuration.defaultWorkdir = defaultWorkdir.Text;
        }

        private void changeTemp_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tempDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void tempDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            configuration.tempDirectory = tempDir.Text;
        }
    }
}
