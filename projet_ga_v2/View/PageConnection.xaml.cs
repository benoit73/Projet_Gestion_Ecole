using projet_ga_v2.Models;
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
    /// Logique d'interaction pour PageConnection.xaml
    /// </summary>
    public partial class PageConnection : Page
    {
        DAO.DAO_Admin daoAdmin;
        public PageConnection()
        {
            InitializeComponent();
            daoAdmin = new DAO.DAO_Admin();
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Username = TbUsername.Text;
            admin.Password = TbPassword.Password;
            if (daoAdmin.DoesAdminExist(admin))
            {
                PageHome pageHome = new PageHome();
                NavigationService?.Navigate(pageHome);
            }
            else
            {
                MessageBox.Show("Identifiants incorrects");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Name.Substring(2);
            if (textBox.Text == text)
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Name.Substring(2);
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = text;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            passwordBox.Password = string.Empty;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = "Password";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageSettings settings = new PageSettings();
            NavigationService.Navigate(settings);
        }
    }
}
