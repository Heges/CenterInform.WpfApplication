using CenterInfor.Domain.Models;
using CenterInform.Application.Configuration;
using CenterInform.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenterInform.Infrastructure
{
    public class EmployeDbContext : DbContext, IEmployeDbContext
    {
        public DbSet<Employe> Employes { get; set; }

        public EmployeDbContext(DbContextOptions<EmployeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
