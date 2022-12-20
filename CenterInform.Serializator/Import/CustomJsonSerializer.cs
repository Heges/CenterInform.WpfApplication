using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using CenterInfor.Domain.Models;
using CenterInform.Serializator.Interfaces;

namespace CenterInform.Serializator
{
    public class CustomJsonSerializer : ISerializer<CustomJsonSerializer>
    {
        /// <summary>
        /// это просто обертка над работающим классом
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
                var json = JsonSerializer.Serialize(tvalue);
                await File.WriteAllTextAsync(pathSave + "\\" + name + ".json", json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
