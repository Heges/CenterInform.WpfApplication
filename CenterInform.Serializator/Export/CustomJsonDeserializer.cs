using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CenterInform.Serializator.Interfaces;

namespace CenterInform.Serializator.Export
{
    public class CustomJsonDeserializer : IDesirializer<CustomJsonDeserializer>
    {
        public async Task<Tvalue> ExportFrom<Tvalue>(string pathSave = null)
        {
            try
            {
                string name = DateTime.Now.ToFileTimeUtc().ToString();

                using (FileStream fs = new FileStream(pathSave, FileMode.Open))
                {
                    return await JsonSerializer.DeserializeAsync<Tvalue>(fs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }
    }
}
