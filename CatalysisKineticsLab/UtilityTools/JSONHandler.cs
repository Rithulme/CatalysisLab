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
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, serializableObject);
            }
            //var JSONString = JsonConvert.SerializeObject(serializableObject);
            //File.Open(fileName;
        }
    }
}
