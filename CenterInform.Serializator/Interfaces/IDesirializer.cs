using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterInform.Serializator.Interfaces
{
    public interface IDesirializer<T> where T: class, IDesirializer<T>
    {
        public Task<Tvalue> ExportFrom<Tvalue>(string pathSave = null);
    }
}
