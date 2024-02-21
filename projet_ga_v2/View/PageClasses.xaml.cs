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
using projet_ga_v2.DAO;
using projet_ga_v2.Models;

namespace projet_ga_v2.View
{
    /// <summary>
    /// Logique d'interaction pour PageClasses.xaml
    /// </summary>
    /// 

    public partial class PageClasses : Page
    {
        Classe classe;
        DAO_Classe dao_classe;
        List<Eleve> allElevesNotInClass;
        List<Eleve> allElevesNotInClassFiltred;
        public PageClasses()
        {
            InitializeComponent();
            classe = new Classe();
            dao_classe = new DAO_Classe();
            allElevesNotInClass = new List<Eleve>();
            allElevesNotInClassFiltred = new List<Eleve>();
            LoadClasses();
            LoadAllElevesNotInClasse(classe);
        }

        private void LoadAllElevesNotInClasse(Classe classe)
        {
            DAO_Eleve dao_eleve = new DAO_Eleve();
            allElevesNotInClass = dao_eleve.GetAllElevesExeptInClass(classe);
            LbAllEleves.ItemsSource = allElevesNotInClass;
        }
        public void LoadClasses()
        {
            List<Classe> classes = dao_classe.GetAllClasses();
            dataGrid.ItemsSource = classes;
        }

        public void LoadElevesOfClasse()
        {
            List<Eleve> eleves = classe.Eleves.ToList();
            DgEleves.ItemsSource = eleves;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Chercher élève...")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Chercher élève...";
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            classe = new Classe();
            classe.NomClasse = TbNom.Text;
            classe.Niveau = CbNiveau.Text;

            if (classe.NomClasse == null || classe.Niveau == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                dao_classe.AddClasse(classe);
            }
            LoadClasses();
            dataGrid.SelectedItem = classe;
            MessageBox.Show("Classe ajoutée");
        }

        private void BtnEdt_Click(object sender, RoutedEventArgs e)
        {
            if (classe.Id == 0)
            {
                MessageBox.Show("Veuillez sélectionner une classe");
            }
            else
            {
                WindowEdt windowEdt = new WindowEdt(classe);
                windowEdt.ShowDialog();
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is Classe)
            {
                classe = (Classe)dataGrid.SelectedItem;
                LoadElevesOfClasse();
                TbNom.Text = classe.NomClasse;
                CbNiveau.SelectedItem = classe.Niveau;
                LoadAllElevesNotInClasse(classe);
            }
            
        }

        private void BtnSuppr_Click(object sender, RoutedEventArgs e)
        {
            dao_classe.DeleteClasse(classe);
            LoadClasses();
            MessageBox.Show("Classe supprimée");
        }

        private void BtnValiderEleve_Click(object sender, RoutedEventArgs e)
        {
            if (classe.Id == 0)
            {
                MessageBox.Show("Veuillez sélectionner une classe");
            }
            else if (LbAllEleves.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un élève");
            }
            else
            {
                Eleve eleve = (Eleve)LbAllEleves.SelectedItem;
                dao_classe.AddEleveToClasse(classe, eleve);
                LoadElevesOfClasse();
                LoadAllElevesNotInClasse(classe);
                LoadClasses();
            }
        }

        private void TbEleve_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbEleve.Text != string.Empty)
            {
                string preFiltre = TbEleve.Text.ToLower();
                string filtre = preFiltre.First().ToString().ToUpper() + preFiltre.Substring(1);
                allElevesNotInClassFiltred = allElevesNotInClass.Where(eleve => eleve.NomEleve.StartsWith(filtre) || eleve.PrenomEleve.StartsWith(filtre)).ToList();
                LbAllEleves.ItemsSource = allElevesNotInClassFiltred;
            }
            else
            {
                LbAllEleves.ItemsSource = allElevesNotInClass;
            }
        }

        private void BtnSupprEleve_Click(object sender, RoutedEventArgs e)
        {
                Eleve eleve = (Eleve)DgEleves.SelectedItem;
                dao_classe.RemoveEleveFromClasse(classe, eleve);
                LoadElevesOfClasse();
                LoadAllElevesNotInClasse(classe);
                MessageBox.Show("Elève supprimé");
            
        }

    }
}
