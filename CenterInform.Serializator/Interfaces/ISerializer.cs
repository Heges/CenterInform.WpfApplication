using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterInform.Serializator.Interfaces
{
    public interface ISerializer<T> where T : class, ISerializer<T>
    {
        public Task ImportTo<Tvalue>(IEnumerable<Tvalue> tvalue, string pathSave = null);
    }
}
