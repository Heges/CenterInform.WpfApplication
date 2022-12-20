using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CenterInform.Serializator.Interfaces;

namespace CenterInform.Serializator.Export
{
    public class CustomXmlDeserializer : IDesirializer<CustomXmlDeserializer>
    {
        public Task<Tvalue> ExportFrom<Tvalue>(string pathSave = null)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(Tvalue));
                using (XmlReader xmlDoc = XmlReader.Create(pathSave))
                {
                    var result = xml.Deserialize(xmlDoc);

                    return Task.FromResult((Tvalue)result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
