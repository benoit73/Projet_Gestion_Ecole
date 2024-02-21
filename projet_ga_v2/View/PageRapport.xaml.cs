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
        }
    }
}
