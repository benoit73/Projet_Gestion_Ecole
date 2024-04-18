using projet_ga_v2.DAO;
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
    /// Logique d'interaction pour PageRapport.xaml
    /// </summary>
    public partial class PageRapport : Page
    {
        DAO_Classe daoClasse;
        DAO_Eleve daoEleve;
        DAO_Enseignant daoEnseignant;
        public PageRapport()
        {
            InitializeComponent();
            daoClasse = new DAO_Classe();
            daoEleve = new DAO_Eleve();
            daoEnseignant = new DAO_Enseignant();
            Init();
        }

        public void Init()
        {
            TbTotalClasses.Content = daoClasse.GetAllClasses().Count;
            TbTotalEleves.Content = daoEleve.GetAllEleves().Count;
            TbTotalProf.Content = daoEnseignant.GetAllEnseignants().Count;
            IEnumerable<object> test = daoClasse.GetNbClassesNiveaux();
            var dynamicList = test.Select(obj => (dynamic)obj).ToList();
            var premiere = dynamicList.FirstOrDefault(obj => obj.Niveau == "Premiere");
            if (premiere != null)
            {
                // Accéder au nombre de classes pour le niveau "premiere"
                int countPremiere = premiere.Nb;
                TbCP.Content = countPremiere.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "premiere" n'a été trouvé
                TbCP.Content = "0";
            }
            var seconde = dynamicList.FirstOrDefault(obj => obj.Niveau == "Seconde");
            if (seconde != null)
            {
                // Accéder au nombre de classes pour le niveau "seconde"
                int countSeconde = seconde.Nb;
                TbCS.Content = countSeconde.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "seconde" n'a été trouvé
                TbCS.Content = "0";
            }
            var terminale = dynamicList.FirstOrDefault(obj => obj.Niveau == "Terminale");
            if (terminale != null)
            {
                // Accéder au nombre de classes pour le niveau "terminale"
                int countTerminale = terminale.Nb;
                TbCT.Content = countTerminale.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "terminale" n'a été trouvé
                TbCT.Content = "0";
            }
            var elevesByNiveau = daoEleve.GetElevesByNiveau();
            var dynamicListEleves = elevesByNiveau.Select(obj => (dynamic)obj).ToList();
            var premiereEleves = dynamicListEleves.FirstOrDefault(obj => obj.Niveau == "Premiere");
            if (premiereEleves != null)
            {
                // Accéder au nombre d'élèves pour le niveau "premiere"
                int countPremiereEleves = premiereEleves.Nb;
                TbEP.Content = countPremiereEleves.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "premiere" n'a été trouvé
                TbEP.Content = "0";
            }
            var secondeEleves = dynamicListEleves.FirstOrDefault(obj => obj.Niveau == "Seconde");
            if (secondeEleves != null)
            {
                // Accéder au nombre d'élèves pour le niveau "seconde"
                int countSecondeEleves = secondeEleves.Nb;
                TbES.Content = countSecondeEleves.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "seconde" n'a été trouvé
                TbES.Content = "0";
            }
            var terminaleEleves = dynamicListEleves.FirstOrDefault(obj => obj.Niveau == "Terminale");
            if (terminaleEleves != null)
            {
                // Accéder au nombre d'élèves pour le niveau "terminale"
                int countTerminaleEleves = terminaleEleves.Nb;
                TbET.Content = countTerminaleEleves.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "terminale" n'a été trouvé
                TbET.Content = "0";
            }
            
            var enseignantsByNiveauClasse = daoEnseignant.GetEnseignantsByNiveauClasse();
            var dynamicListEnseignants = enseignantsByNiveauClasse.Select(obj => (dynamic)obj).ToList();
            var premiereEnseignants = dynamicListEnseignants.FirstOrDefault(obj => obj.Niveau == "Premiere");
            if (premiereEnseignants != null)
            {
                // Accéder au nombre d'enseignants pour le niveau "premiere"
                int countPremiereEnseignants = premiereEnseignants.Nb;
                TbPP.Content = countPremiereEnseignants.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "premiere" n'a été trouvé
                TbPP.Content = "0";
            }
            var secondeEnseignants = dynamicListEnseignants.FirstOrDefault(obj => obj.Niveau == "Seconde");
            if (secondeEnseignants != null)
            {
                // Accéder au nombre d'enseignants pour le niveau "seconde"
                int countSecondeEnseignants = secondeEnseignants.Nb;
                TbPS.Content = countSecondeEnseignants.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "seconde" n'a été trouvé
                TbPS.Content = "0";
            }
            var terminaleEnseignants = dynamicListEnseignants.FirstOrDefault(obj => obj.Niveau == "Terminale");
            if (terminaleEnseignants != null)
            {
                // Accéder au nombre d'enseignants pour le niveau "terminale"
                int countTerminaleEnseignants = terminaleEnseignants.Nb;
                TbPT.Content = countTerminaleEnseignants.ToString();
            }
            else
            {
                // Gérer le cas où aucun résultat pour "terminale" n'a été trouvé
                TbPT.Content = "0";
            }

        }
    }
}
