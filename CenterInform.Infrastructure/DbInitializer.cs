using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterInform.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(EmployeDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
