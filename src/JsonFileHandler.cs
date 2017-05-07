using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SampleApiProj
{
    class JsonFileHandler
    {
        public static void CreateJsonOutFile(string jsonResponse, string inFile)
        {
            Fastlink fs = new Fastlink();
            string path = fs.GetFilePath(inFile);
            try
            {
                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(path, serializedJson);
            }
            catch(Exception e)
            {
                Logger.Log("Cannot write to file " + path);
                throw e;
            }
        }
    }
}
