using Microsoft.Extensions.DependencyInjection;
using CenterInform.Serializator.Interfaces;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using CenterInform.Serializator.Export;

namespace CenterInform.Serializator
{
    public static class DipendencyInjection
    {
        public static IServiceCollection AddJsonSerializer(this IServiceCollection services)
        {
            services.AddScoped<ISerializer<CustomJsonSerializer>, CustomJsonSerializer>();
            services.AddScoped<ISerializer<CustomXmlSerializer>, CustomXmlSerializer>();
            services.AddScoped<IDesirializer<CustomJsonDeserializer>, CustomJsonDeserializer>();
            services.AddScoped<IDesirializer<CustomXmlDeserializer>, CustomXmlDeserializer>();
            services.AddScoped<SerializationManager>();
            return services;
        }
    }
}
