using CenterInfor.Domain.Models;
using CenterInform.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace CenterInform.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly EmployeDbContext _context;

        public Repository(EmployeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employe> Get()
        {
            return _context.Employes.ToList();
        }

        public void Remove(Employe emp)
        {
            try
            {
               var result = _context.Employes.Remove(emp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employe> FindById(Guid id)
        {
            try
            {
                return await _context.Employes.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///  в репозитории не должно быть логики
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public async Task Update(Employe emp)
        {
            try
            {
                var entity = await FindById(emp.Id);
                if (entity != null)
                {
                    _context.Entry<Employe>(entity).State = EntityState.Detached;
                    _context.Employes.Update(emp);
                }
                else
                {
                    Create(emp);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///  в репозитории не должно быть логики
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public async Task<bool> ExcludeEntityFromDbTable(IEnumerable<Employe> list)
        {
            try
            {
                var listToDelete = _context.Employes.Where(x => !list.Contains(x)).Select(x => x).ToList();
                if (listToDelete != null && listToDelete.Count > 0)
                {
                    listToDelete.ForEach(x => Remove(x));
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return await Task.FromResult(false);
        }

        public void Create(Employe emp)
        {
            try
            {
                _context.Employes.Add(emp);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Employe> Get(Func<Employe, bool> predicate)
        {
            return _context.Employes.AsNoTracking().Where(predicate).ToList();
        }

        public async Task CommitChanges(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
