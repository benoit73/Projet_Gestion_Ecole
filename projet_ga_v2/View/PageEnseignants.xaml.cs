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
    /// Logique d'interaction pour PageEnseignants.xaml
    /// </summary>
    public partial class PageEnseignants : Page
    {
        DAO_Enseignant daoEnseignant;
        DAO_Matiere daoMatiere;
        public PageEnseignants()
        {
            InitializeComponent();
            daoEnseignant = new DAO_Enseignant();
            daoMatiere = new DAO_Matiere();
            Refresh();
        }

        public void Refresh()
        {
            List<Enseignant> listEnseignants = daoEnseignant.GetAllEnseignantsWithMatiereAndClasses();
            dataGrid.ItemsSource = listEnseignants;

            CbMatiere.ItemsSource = daoMatiere.GetAllMatieres();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbNom.Text != null && TbPrenom.Text != null && TbEmail.Text != null && CbMatiere.SelectedItems != null)
            {
                Enseignant enseignant = new Enseignant();
                enseignant.NomEnseignant = TbNom.Text;
                enseignant.PrenomEnseignant = TbPrenom.Text;
                ICollection<Matiere> matieres = new List<Matiere>();
                foreach (Matiere matiere in CbMatiere.SelectedItems)
                {
                    matieres.Add(matiere);
                }
                enseignant.Matieres = matieres;
                daoEnseignant.AddEnseignant(enseignant);
                Refresh();
                TbEmail.Text = "";
                TbNom.Text = "";
                TbPrenom.Text = "";
                MessageBox.Show("Enseignant ajouté");
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un enseignant");
                return;
            }
            Enseignant enseignant = dataGrid.SelectedItem as Enseignant;
            daoEnseignant.DeleteEnseignant(enseignant);
            MessageBox.Show("Enseignant supprimé");
            Refresh();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un enseignant");
                return;
            }
            if (CbMatiere.SelectedItems == null || TbNom.Text == null || TbPrenom.Text == null || TbEmail.Text == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }

            Enseignant selectedEnseignant = dataGrid.SelectedItem as Enseignant;
            Enseignant enseignant = dataGrid.SelectedItem as Enseignant;
            enseignant.Id = selectedEnseignant.Id;
            ICollection<Matiere> matieres = new List<Matiere>();
            foreach (Matiere matiere in CbMatiere.SelectedItems)
            {
                matieres.Add(matiere);
            }
            enseignant.Matieres = matieres; 
            enseignant.NomEnseignant = TbNom.Text;
            enseignant.PrenomEnseignant = TbPrenom.Text;
            enseignant.Email = TbEmail.Text;
            daoEnseignant.UpdateEnseignant(enseignant);
            MessageBox.Show("Enseignant modifié");
            Refresh();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Enseignant enseignant = dataGrid.SelectedItem as Enseignant;
                TbNom.Text = enseignant.NomEnseignant;
                TbPrenom.Text = enseignant.PrenomEnseignant;
                TbEmail.Text = enseignant.Email;
            }
        }
    }
}
