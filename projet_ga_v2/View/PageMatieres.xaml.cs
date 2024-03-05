using projet_ga_v2.DAO;
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
    /// Logique d'interaction pour PageMatieres.xaml
    /// </summary>
    public partial class PageMatieres : Page
    {
        DAO_Matiere dAO_Matiere;

        public PageMatieres()
        {
            InitializeComponent();
            dAO_Matiere = new DAO_Matiere();
            RefreshDG();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbMatiere.Text == "")
            {
                MessageBox.Show("Veuillez remplir le champ");
                return;
            }
            string nom = TbMatiere.Text;
            Matiere matiere = new Matiere();
            matiere.NomMatiere = nom;

            dAO_Matiere.AddMatiere(matiere);
            RefreshDG();
            TbMatiere.Text = "";
            MessageBox.Show("Matière ajoutée");
        }

        public void RefreshDG()
        {
            List<Matiere> matieres = dAO_Matiere.GetAllMatieres();
            dataGrid.ItemsSource = matieres;
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Matiere matiere = (Matiere)dataGrid.SelectedValue as Matiere;
            dAO_Matiere.DeleteMatiere(matiere);
            RefreshDG();
        }
    }
}
