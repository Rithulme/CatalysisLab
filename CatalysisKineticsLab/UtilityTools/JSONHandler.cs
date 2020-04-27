using Newtonsoft.Json;
using System.IO;

namespace UtilityTools
{
    public class JSONHandler
    {
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer(); serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                //serialize object directly into file stream
                serializer.Serialize(file, serializableObject, typeof(T));
            }
        }

        public T DeSerializeObject<T>(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                };
                //read and deserialize
                var JSONString = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(JSONString, jsonSettings);
            }
        }
    }
}
