using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SampleApiProj
{
    class Fastlink
    {
        public string GetFilePath(string inFile)
        {
            string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string fpath = Path.Combine(outPutDirectory, "ProjResources\\" + inFile);
            string filePath = new Uri(fpath).LocalPath;
            return filePath;
        }

        public string WriteTokenstoFile(string path)
        {
            string cobSession = Authentication.authTokens["cobSession"];
            string userSession = Authentication.authTokens["userSession"];
            string token = Authentication.authTokens["accessToken"];
            string appId = "10003600";
            string text = "";
            try
            {
              text = File.ReadAllText(path);
            }
            catch(IOException e)
            {
                Logger.Log("Unable to read from " + path);
                throw e;
            }
                text = text.Replace("$appID", appId);
                text = text.Replace("$rsession", userSession);
                text = text.Replace("$token", token);
                text = text.Replace("$redirectReq", "true");
                string fName = Path.GetFileNameWithoutExtension(path);
                string fDir = Path.GetDirectoryName(path);
                string fExt = Path.GetExtension(path);
                string outfile = Path.Combine(fDir, String.Concat(fName, "_dmp", fExt));
            try
            {
                File.WriteAllText(outfile, text);
            }
            catch(IOException e)
            {
                Logger.Log("Unable to write to " + outfile);
                throw e;
            }           
            return outfile;
        }
    }
}
