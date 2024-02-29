using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Serilog;

namespace OireachtasAPI.Services.LoadData
{
    public class LocalFileMeanService : ILocalFileMeanService
    {
        public TModel Load<TModel>(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (var resourceStream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}"))
            {
                if (resourceStream == null)
                {
                    throw new ArgumentException($"Resource");
                }

                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    return JsonConvert.DeserializeObject<TModel>(reader.ReadToEnd());
                }
            }
        }
    }

    public interface ILocalFileMeanService
    {
        TModel Load<TModel>(string fileName);
    }
}