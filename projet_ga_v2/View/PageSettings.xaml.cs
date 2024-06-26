﻿using System;
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
    /// Logique d'interaction pour PageSettings.xaml
    /// </summary>
    public partial class PageSettings : Page
    {
        public PageSettings()
        {
            InitializeComponent();
            Initialiser();
        }

        public void Initialiser()
        {
            TbDatabase_name.Text = Properties.Settings.Default.DbName;
            TbServer_address.Text = Properties.Settings.Default.ServerAdress;
            TbUsername.Text = Properties.Settings.Default.Username;
            TbPassword.Text = Properties.Settings.Default.Pwd;
            TbPort.Text = Properties.Settings.Default.Port;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Name.Substring(2).Replace('_', ' ');
            if (textBox.Text == text)
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Name.Substring(2).Replace('_', ' ');
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = text;
            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DbName = TbDatabase_name.Text;
            Properties.Settings.Default.ServerAdress = TbServer_address.Text;
            Properties.Settings.Default.Username = TbUsername.Text;
            Properties.Settings.Default.Pwd = TbPassword.Text;
            Properties.Settings.Default.Port = TbPort.Text;
            Properties.Settings.Default.Save();
            PageConnection pageConnection = new PageConnection();
            NavigationService.Navigate(pageConnection);
        }
    }
}
