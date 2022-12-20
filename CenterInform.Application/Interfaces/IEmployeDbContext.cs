using CenterInfor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CenterInform.Application.Interfaces
{
    public interface IEmployeDbContext
    {
        DbSet<Employe> Employes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancelationToken);
    }
}
