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
        EnseignantMatiereClasse enseignantMatiereClasseSelected;
        DAO_Matiere dao_matiere;
        DAO_Enseignant dao_enseignant;
        DAO_Classe dao_classe;
        DAO_Cour dao_cour;
        DAO_EnseignantMatiereClasse dao_enseignantMatiereClasse;
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
            dao_enseignantMatiereClasse = new DAO_EnseignantMatiereClasse();
            classe = dao_classe.GetClasseById(classee);
            InitMatieres();
            InitAddMatiere();
            SetRectangles();
            InitEdt();
            LbDuree.SelectedItem = LbDuree.Items[1];
        }


        public TextBlock TbCour2(EnseignantMatiereClasse emc)
        {
            TextBlock tb = new TextBlock();
            tb.Style = (Style)FindResource("EdtTextBlockStyle");
            tb.IsHitTestVisible = false;
            ListBoxItem selectedItem = (ListBoxItem)LbDuree.SelectedItem;
            int tagValue = int.Parse(selectedItem.Tag.ToString());
            Grid.SetRowSpan(tb, tagValue);
            tb.Name = "tb" + nbTb;
            tb.Text = emc.Matiere.NomMatiere + "\n" + emc.Enseignant.NomEnseignant + " " + emc.Enseignant.PrenomEnseignant;
            nbTb++;
            return tb;
        }
        private TextBlock TbCour(Cour cours)
        {
            TextBlock tb = new TextBlock();
            tb.Style = (Style)FindResource("EdtTextBlockStyle");
            tb.Name = "tb" + nbTb;
            tb.Text = cours.EnseignantMatiereClasse.Matiere.NomMatiere + "\n" + cours.EnseignantMatiereClasse.Enseignant.NomEnseignant + " " + cours.EnseignantMatiereClasse.Enseignant.PrenomEnseignant;
            nbTb++;
            return tb;
        }
        private Button BtnMatiere(EnseignantMatiereClasse emc)
        {
            Button btn = new Button();
            StackPanel sp2 = new StackPanel();
            sp2.Orientation = Orientation.Horizontal;
            btn.Content = sp2;
            btn.Click += BtnMatiere_Click;
            btn.Tag = emc;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp2.Children.Add(sp);

            TextBlock tbMatiere = new TextBlock();
            tbMatiere.Text = emc.Matiere.NomMatiere;
            sp.Children.Add(tbMatiere);

            TextBlock tbProf = new TextBlock();
            tbProf.Text = emc.Enseignant.NomEnseignant + " " + emc.Enseignant.PrenomEnseignant;
            sp.Children.Add(tbProf);

            Button btnSuppr = new Button();
            btnSuppr.Content = "Supprimer";
            btnSuppr.Click += BtnSupprMatiere_Click;
            btnSuppr.Tag = emc;
            sp2.Children.Add(btnSuppr);

            
            return btn;
        }// créer un modele de bouton pour chaque matiere et prof de la classe
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
        public void InitClasse()
        {
            classe = dao_classe.GetClasseById(classe);
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


            foreach (EnseignantMatiereClasse emc in classe.EnseignantMatiereClasses)
            {
                foreach (Cour cours in emc.Cours)
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
        }
        private void BtnSupprMatiere_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            EnseignantMatiereClasse emc = (EnseignantMatiereClasse)btn.Tag;
            Enseignant enseignant = emc.Enseignant;
            Matiere matiere = emc.Matiere;
            dao_enseignantMatiereClasse.DeleteEnseignantMatiereClasse(enseignant, matiere, classe);
            InitMatieres();
            InitEdt();
        }
        
        private void InitMatieres() // initialise la liste des matieres et des profs de la classe
        {
            InitClasse();
            CbMatiereClasse.Items.Clear();
            TbNomClasse.Text = classe.NomClasse;
            foreach (EnseignantMatiereClasse emc in classe.EnseignantMatiereClasses)
            {
                Button btn = BtnMatiere(emc);
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
                dao_enseignantMatiereClasse.AddEnseignantMatiereClasse(enseignant, matiere, classe);
                gridAdd.Visibility = Visibility.Hidden;
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
            Close();
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
        

        private void rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            if (enseignantMatiereClasseSelected != null)
            {
                if (first > 0)
                {
                    gridEdt.Children.Remove(gridEdt.Children[gridEdt.Children.Count - 1]);
                }

                TextBlock TbC = TbCour2(enseignantMatiereClasseSelected);

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

        private void BtnMatiere_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            EnseignantMatiereClasse emc = (EnseignantMatiereClasse)button.Tag;
            enseignantMatiereClasseSelected = emc;
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
                cour.EnseignantMatiereClasse = enseignantMatiereClasseSelected;
                cour.EnseignantMatiereClasseId = enseignantMatiereClasseSelected.Id;
                cour.EnseignantMatiereClasse.EnseignantId = enseignantMatiereClasseSelected.Enseignant.Id;
                cour.EnseignantMatiereClasse.ClasseId = classe.Id;
                cour.EnseignantMatiereClasse.MatiereId = enseignantMatiereClasseSelected.Matiere.Id;
                dao_cour.AddCour(cour);
            }
        }

        

    }
}
