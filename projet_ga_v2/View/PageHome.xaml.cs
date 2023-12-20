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

namespace projet_ga_v2.View
{
    /// <summary>
    /// Logique d'interaction pour PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {
        public PageHome()
        {
            InitializeComponent();
            CurrentPage = new Uri("PageRapport.xaml", UriKind.Relative);
            DataContext = this;
        }

        public Uri CurrentPage { get; set; }

        private void BtnSide_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content is StackPanel stackPanel)
            {
                foreach (UIElement element in stackPanel.Children)
                {
                    if (element is Label label)
                    {
                        string labelText = label.Content.ToString();

                        string pageName = "View/Page" + labelText + ".xaml";
                        Uri pageUri = new Uri(pageName, UriKind.Relative);

                        // Utilisez la méthode Navigate pour charger la page
                        SideFrame.Navigate(pageUri);
                        break;
                    }
                }
            }
        }

        private void BtnDeconnection_Click(object sender, RoutedEventArgs e)
        {
            PageConnection pageConnection = new PageConnection();
            NavigationService.Navigate(pageConnection);
        }
    }
}
