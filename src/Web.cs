using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SampleApiProj
{
    class Web
    {
        public static string resCode { get; set; }
        public static string httpPost(string url, List<string> authHeader, string body, string queryParam)
        {
            if (queryParam != null)
            {
                url = url + "?" + queryParam;
            }
            Uri apiurl = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
            request.Method = "POST";
            request.ContentType = "application/json";
            if (authHeader != null && authHeader.Count > 0)
            {
                for (int index = 0; index < authHeader.Count; index++)
                {
                    request.Headers.Add(authHeader[index]);
                }

            }
            using (Stream stream = request.GetRequestStream())
                if (body != null)
                {
                    using (StreamWriter requestWriter = new StreamWriter(stream))
                    {
                        requestWriter.Write(body);
                        requestWriter.Close();
                    }
                }
            try
            {
                WebResponse httpResponse = request.GetResponse();
                using (Stream stream = httpResponse.GetResponseStream())
                {

                    if (stream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(stream))
                        {
                            resCode = responseReader.ReadToEnd();

                        }

                    }

                }
                return resCode.ToString();
            }
            catch (WebException e)
            {
                Console.WriteLine("HTTP Post Failed for " + apiurl);
                return null;
            }
        }

        public static string httpGet(string url, List<string> authHeader, Dictionary<string, string> parameters)
        {
            string queryParams = "";
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    if (entry.Value != "" && entry.Value != null)
                    {
                        queryParams = queryParams + entry.Key + "=" + entry.Value + "&";
                    }
                }
                url = url + "?" + queryParams;
            }
            Uri apiurl = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
            if (authHeader != null && authHeader.Count > 0)
            {
                for (int index = 0; index < authHeader.Count; index++)
                {
                    request.Headers.Add(authHeader[index]);
                }
            }
            try
            {
                WebResponse httpResponse = request.GetResponse();
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(stream))
                        {
                            resCode = responseReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("HTTP Get Failed for " + apiurl);
                return null;
            }
            return resCode.ToString();
        }
    }
}

