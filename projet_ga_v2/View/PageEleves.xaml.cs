using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using projet_ga_v2.BDD;
using System.Windows.Controls;

namespace projet_ga_v2.View
{
    public partial class PageEleves : Page
    {
        public PageEleves()
        {
            InitializeComponent();
            InitialiserDg();
();
        }

        public IEnumerable<Elefe> GetEleves()
        {
            using (Benoit73TestContext context = new Benoit73TestContext())
            {
                var allEleves = context.Eleves.Include(e => e.Classe).ToList();
                return allEleves;
            }
        }

        public void InitialiserDg()
        {
            List<Elefe> list = GetEleves().ToList();
            dataGrid.ItemsSource = list;
            // Utilisez 'list' pour accéder à vos données récupérées.
        }
    }
}
