using CenterInfor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenterInform.Application.Interfaces
{
    public interface IRepository
    {
        public void Create(Employe emp);
        public IEnumerable<Employe> Get();
        public IEnumerable<Employe> Get(Func<Employe, bool> predicate);
        public Task<Employe> FindById(Guid id);
        public Task Update(Employe emp);
        public void Remove(Employe emp);
        public Task<bool> ExcludeEntityFromDbTable(IEnumerable<Employe> list);

        public Task CommitChanges(CancellationToken token = default);
    }
}
