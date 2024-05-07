using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projet_ga_v2.Models;
namespace projet_ga_v2.DAO
{
    internal class DAO_Admin
    {
        public bool DoesAdminExist(Admin admin)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                bool adminExist = context.Admins.Any(a => a.Username == admin.Username && a.Password == admin.Password);
                return adminExist;
            }
        }
    }
}
