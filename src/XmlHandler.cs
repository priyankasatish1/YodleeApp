using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace SampleApiProj
{
    class XmlHandler
    {
        public static XmlDocument xmlFile;

        public static void CreateXMLOutFile(string jsonResponse, string inFile)
        {
            Fastlink fs = new Fastlink();
            string path = fs.GetFilePath(inFile);
            xmlFile = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonResponse, "root");
            try
            {
                xmlFile.Save(path);
            }
            catch(Exception e)
            {
                Console.WriteLine("Cannot write to file " + path);
                throw e;
            }
        }
    }
}
