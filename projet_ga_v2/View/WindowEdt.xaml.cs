using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace projet_ga_v2.View
{

    public partial class WindowEdt : Window
    {
        Classe classe;
        Enseignant enseignantSelected;
        DAO_Matiere dao_matiere;
        DAO_Enseignant dao_enseignant;
        DAO_Classe dao_classe;
        DAO_Cour dao_cour;
        int semaine = 1;
        int nbTb = 1;
        int first = 0;
        public WindowEdt(Classe classee)
        {
            InitializeComponent();
            dao_classe = new DAO_Classe();
            dao_matiere = new DAO_Matiere();
            dao_enseignant = new DAO_Enseignant();
            dao_cour = new DAO_Cour();
            classe = dao_classe.GetClasseById(classee);
            InitMatieres();
            InitAddMatiere();
            SetRectangles();
            InitEdt();
            LbDuree.SelectedItem = LbDuree.Items[1];
        }

        public void SetRectangles()
        {
            for (int i = 1; i <= gridEdt.RowDefinitions.Count; i++)
            {
                for (int j = 1; j <= gridEdt.ColumnDefinitions.Count; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Stroke = Brushes.Black;
                    rectangle.Fill = Brushes.White;
                    rectangle.MouseEnter += rectangle_MouseEnter;
                    rectangle.MouseDown += rectangle_MouseDown;
                    Grid.SetRow(rectangle, i);
                    Grid.SetColumn(rectangle, j);
                    Grid.SetZIndex(rectangle, -1);
                    gridEdt.Children.Add(rectangle);
                }
            }
        }
        private TextBlock TbCour(Cour cours)
        {
            TextBlock tb = new TextBlock();
            tb.Background = Brushes.LightBlue;
            tb.Name = "tb" + nbTb;
            tb.Text = "test"; /*cours.Enseignant.Matiere.NomMatiere + "\n" + cours.Enseignant.NomEnseignant + " " + cours.Enseignant.PrenomEnseignant;*/
            nbTb++;
            return tb;
        }
        public void InitEdt()
        {
            classe = dao_classe.GetClasseById(classe);

            List<UIElement> elementsToRemove = new List<UIElement>();

            foreach (UIElement element in gridEdt.Children)
            {
                if (element is TextBlock)
                {
                    TextBlock tb = (TextBlock)element;
                    if (tb.Name.Contains("tb"))
                    {
                        elementsToRemove.Add(element);
                    }
                }
            }

            foreach (UIElement elementToRemove in elementsToRemove)
            {
                gridEdt.Children.Remove(elementToRemove);
            }


            foreach (Cour cours in classe.Cours)
            {
                if (cours.Semaine == semaine)
                {
                    TextBlock tb = TbCour(cours);
                    Grid.SetRow(tb, cours.Debut - 15);
                    Grid.SetColumn(tb, cours.Jour);
                    Grid.SetRowSpan(tb, cours.Duree);
                    gridEdt.Children.Add(tb);
                }

            }
        }
        private void BtnSupprMatiere_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Enseignant enseignant = (Enseignant)btn.Tag;
            dao_enseignant.RemoveEnseignantFromClass(enseignant, classe);
            classe.Enseignants.Remove(enseignant);
            InitEdt();
        }// supprime une matiere et un prof de la classe
        private Button BtnMatiere(Enseignant enseignant)
        {
            Button btn = new Button();
            StackPanel sp2 = new StackPanel();
            sp2.Orientation = Orientation.Horizontal;
            btn.Content = sp2;
            btn.Click += BtnMatiere_Click;
            btn.Tag = enseignant;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp2.Children.Add(sp);

            TextBlock tbMatiere = new TextBlock();
            tbMatiere.Text = "test";
            sp.Children.Add(tbMatiere);

            TextBlock tbProf = new TextBlock();
            tbProf.Text = enseignant.NomEnseignant + " " + enseignant.PrenomEnseignant;
            sp.Children.Add(tbProf);

            Button btnSuppr = new Button();
            btnSuppr.Content = "Supprimer";
            btnSuppr.Click += BtnSupprMatiere_Click;
            btnSuppr.Tag = enseignant;
            sp2.Children.Add(btnSuppr);


            return btn;
        }// créer un modele de bouton pour chaque matiere et prof de la classe
        private void InitMatieres() // initialise la liste des matieres et des profs de la classe
        {
            CbMatiereClasse.Items.Clear();
            TbNomClasse.Text = classe.NomClasse;
            foreach (Enseignant enseignant in classe.Enseignants)
            {
                Button btn = BtnMatiere(enseignant);
                CbMatiereClasse.Items.Add(btn);
            }
        }

        private void BtnAddMatiere_Click(object sender, RoutedEventArgs e) // ouvre la fenetre d'ajout de matiere
        {
            gridAdd.Visibility = Visibility.Visible;
            InitAddMatiere();
        }

        private void InitAddMatiere()
        {
            List<Matiere> matieres = dao_matiere.GetAllMatieres();
            LbMatiere.ItemsSource = matieres;

            List<Enseignant> enseignants = dao_enseignant.GetAllEnseignantsExeptInClass(classe);
            LbProf.ItemsSource = enseignants;

        }// initialise la fenetre d'ajout de matiere
        private void LbMatiere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Enseignant> enseignants = dao_enseignant.GetAllEnseignantsInMatiereNotInClasse((Matiere)LbMatiere.SelectedItem, classe);
            LbProf.ItemsSource = enseignants;
        }// quand une matiere est selectionnée, on affiche les profs qui enseignent cette matiere et qui ne sont pas dans la classe

        private void BtnValidMatProf_Click(object sender, RoutedEventArgs e)
        {
            if (LbMatiere.SelectedItem != null && LbProf.SelectedItem != null)
            {
                Matiere matiere = (Matiere)LbMatiere.SelectedItem;
                Enseignant enseignant = (Enseignant)LbProf.SelectedItem;
                dao_enseignant.AddEnseignantToClass(enseignant, classe);
                gridAdd.Visibility = Visibility.Hidden;
                classe.Enseignants.Add(enseignant);
                InitMatieres();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une matiere et un professeur");
            }
        }// ajoute une matiere et un professeur a la classe et ferme la fenêtre AddMatiere


        private void BtnQuitMatProf_Click(object sender, RoutedEventArgs e) // ferme la fenetre d'ajout de matiere
        {
            gridAdd.Visibility = Visibility.Hidden;
            LbMatiere.SelectedItem = null;
            LbProf.SelectedItem = null;
        }

        private void BtnValiderEdt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            semaine++;
            TbSemaine.Text = "Semaine " + semaine;
            InitEdt();
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (semaine > 1)
            {
                semaine--;
                TbSemaine.Text = "Semaine " + semaine;
                InitEdt();
            }
        }
        private void BtnMatiere_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Enseignant enseignant = (Enseignant)button.Tag;
            enseignantSelected = enseignant;
        }

        public TextBlock TbCour2(Enseignant enseignant)
        {
            TextBlock tb = new TextBlock();
            tb.IsHitTestVisible = false;
            tb.Background = Brushes.LightBlue;
            ListBoxItem selectedItem = (ListBoxItem)LbDuree.SelectedItem;
            int tagValue = int.Parse(selectedItem.Tag.ToString());
            Grid.SetRowSpan(tb, tagValue);
            tb.Name = "tb" + nbTb;
            tb.Text = "test"; /*enseignant.Matieres.NomMatiere + "\n" + enseignant.NomEnseignant + " " + enseignant.PrenomEnseignant;*/
            nbTb++;
            return tb;
        }

        private void rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            if (enseignantSelected != null)
            {
                if (first > 0)
                {
                    gridEdt.Children.Remove(gridEdt.Children[gridEdt.Children.Count - 1]);
                }

                TextBlock TbC = TbCour2(enseignantSelected);

                var cell = sender as UIElement;
                if (cell != null)
                {
                    Grid.SetColumn(TbC, Grid.GetColumn(cell));
                    Grid.SetRow(TbC, Grid.GetRow(cell));
                    gridEdt.Children.Add(TbC);
                }
                first = 1;
            }
        }


        private void rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            if (gridEdt.Children[gridEdt.Children.Count - 1] is TextBlock)
            {
                TextBlock tb = gridEdt.Children[gridEdt.Children.Count - 1] as TextBlock;
                first = 0;
                Cour cour = new Cour();
                cour.Jour = Grid.GetColumn(tb);
                cour.Debut = (Grid.GetRow(tb) + 15);
                cour.Duree = Grid.GetRowSpan(tb);
                cour.Semaine = semaine;
                cour.EnseignantId = enseignantSelected.Id;
                cour.ClasseId = classe.Id;
                dao_cour.AddCour(cour);
            }
        }

        

    }
}
