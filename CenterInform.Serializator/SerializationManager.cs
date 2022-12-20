using CenterInform.Serializator.Export;
using CenterInform.Serializator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterInform.Serializator
{
    /// <summary>
    /// класс контроллер для серилизаторов
    /// </summary>
    public class SerializationManager
    {
        /// <summary>
        /// не лучший подход
        /// но работает
        /// </summary>
        private readonly ISerializer<CustomJsonSerializer> _customJsonSerializer;
        private readonly ISerializer<CustomXmlSerializer> _customXmlSerializer;
        private readonly IDesirializer<CustomJsonDeserializer> _customJsonDeserializer;
        private readonly IDesirializer<CustomXmlDeserializer> _customXmlDeserializer;

        public SerializationManager(ISerializer<CustomJsonSerializer> customJsonSerializer,
            ISerializer<CustomXmlSerializer> customXmlSerializer,
            IDesirializer<CustomJsonDeserializer> customJsonDeserializer,
            IDesirializer<CustomXmlDeserializer> customXmlDeserializer)
        {
            _customJsonSerializer = customJsonSerializer;
            _customXmlSerializer = customXmlSerializer;
            _customJsonDeserializer = customJsonDeserializer;
            _customXmlDeserializer = customXmlDeserializer;
        }

        public async Task ImportToJson<T>(IEnumerable<T> tValue, string pathSave = null)
        {
            await _customJsonSerializer.ImportTo(tValue, pathSave);
        }

        public async Task ImportToXml<T>(IEnumerable<T> tValue, string pathSave = null)
        {
            await _customXmlSerializer.ImportTo(tValue, pathSave);
        }

        public async Task<T> ExportFromJson<T>(string pathSave = null)
        {
            return await _customJsonDeserializer.ExportFrom<T>(pathSave);
        }

        public async Task<T> ExportFromXml<T>(string pathSave = null)
        {
            return await _customXmlDeserializer.ExportFrom<T>(pathSave);
        }
    }
}
