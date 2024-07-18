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
        }

    }
}
