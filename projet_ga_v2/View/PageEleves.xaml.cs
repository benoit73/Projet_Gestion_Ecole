using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using projet_ga_v2.DAO;
using projet_ga_v2.Models;
using System.Windows;

namespace projet_ga_v2.View
{
    public partial class PageEleves : Page
    {
        DAO_Eleve daoEleve = new DAO_Eleve();

        public PageEleves()
        {
            InitializeComponent();
            InitialiserDg();
        }

        public void InitialiserDg()
        {
            List<Eleve> Eleves = daoEleve.GetAllElevesWithClasse();
            dataGrid.ItemsSource = Eleves;
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TbNom.Text == "" || TbPrenom.Text == "" || TbEmail.Text == "" || TbDate.Text == "" || TbEmailParent.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }
            Eleve eleve = new Eleve();
            Parent parent = new Parent();
            parent.Email = TbEmailParent.Text;
            eleve.Parents = parent;
            eleve.NomEleve = TbNom.Text;
            eleve.PrenomEleve = TbPrenom.Text;
            eleve.MailEleve = TbEmail.Text;
            eleve.DateNaissance = TbDate.Text;
            daoEleve.AddEleve(eleve);
            InitialiserDg();
            TbEmail.Text = "";
            TbNom.Text = "";
            TbPrenom.Text = "";
            TbDate.Text = "";
            MessageBox.Show("Elève ajouté");
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un élève");
                return;
            }
            Eleve eleve = (Eleve)dataGrid.SelectedItem;
            daoEleve.DeleteEleve(eleve);
            InitialiserDg();
            MessageBox.Show("Elève supprimé");
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un élève");
                return;
            }
            if (TbNom.Text == "" || TbPrenom.Text == "" || TbEmail.Text == "" || TbDate.Text == "" || TbEmailParent.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }
            Eleve eleve = (Eleve)dataGrid.SelectedItem;
            eleve.NomEleve = TbNom.Text;
            eleve.PrenomEleve = TbPrenom.Text;
            eleve.MailEleve = TbEmail.Text;
            eleve.DateNaissance = TbDate.Text;
            daoEleve.UpdateEleve(eleve);
            InitialiserDg();
            MessageBox.Show("Elève modifié");
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                return;
            }
            Eleve eleve = (Eleve)dataGrid.SelectedItem;
            TbNom.Text = eleve.NomEleve;
            TbPrenom.Text = eleve.PrenomEleve;
            TbEmail.Text = eleve.MailEleve;
            TbDate.Text = eleve.DateNaissance;
        }
    }
}
