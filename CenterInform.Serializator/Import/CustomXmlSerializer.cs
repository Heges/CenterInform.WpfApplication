using CenterInform.Serializator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CenterInform.Serializator
{
    public class CustomXmlSerializer : ISerializer<CustomXmlSerializer>
    {
        /// <summary>
        /// простая обертка над работающим классом
        /// мне же не нужно было сериализовать нативно?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tvalue"></param>
        /// <param name="pathSave"></param>
        /// <returns></returns>
        public async Task ImportTo<Tvalue>(IEnumerable<Tvalue> tvalue, string pathSave = null)
        {
            string workingDirectory = Environment.CurrentDirectory;

            pathSave ??= Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\SerializationFolder";
            string name = DateTime.Now.ToFileTimeUtc().ToString();

            try
            {
                XmlSerializer xml = new XmlSerializer(tvalue.GetType());

                using (FileStream fs = new FileStream(pathSave + "\\" + name + ".xml", FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, tvalue);

                    await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            await Task.FromResult(false);
        }
    }
}
