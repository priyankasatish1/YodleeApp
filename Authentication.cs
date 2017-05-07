using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace SampleApiProj
{
    public class Authentication
    {
        public static Dictionary<string, string> authTokens = new Dictionary<string, string>();
        public static Enum GetCobrandSessionToken()
        {
            string clientData = GetClientInfo("cobrand");
            string cobrandApiUrl = "https://developer.api.yodlee.com:443/ysl/restserver/v1/cobrand/login";
            string response = Web.httpPost(cobrandApiUrl, null, clientData, null);
            if (response == null)
            {
                Console.WriteLine("Wrong cobrand username/password");
                return Status.FAILURE;
            }
            else
            {
                var rootObject = JsonConvert.DeserializeObject<Deserializer>(response);
                authTokens["cobSession"] = rootObject.session.cobSession;
                return Status.SUCCESS;
            }
        }

        private static string GetClientInfo(string session)
        {
            Dictionary<string, string> userCredentials = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string> >sessionObj = new Dictionary<string, Dictionary<string, string>>();
            if (session == "cobrand")
            {
                userCredentials["cobrandLogin"] = "sbCobphalageri";
                userCredentials["cobrandPassword"] = "5e26696f-028d-41bd-9c0a-7ad3f6956702";
            } else if(session == "user")
            {
                userCredentials["loginName"] = "sbMemphalageri1";
                userCredentials["password"] = "sbMemphalageri1#123";
            }
            sessionObj[session] = userCredentials;
            return JsonConvert.SerializeObject(sessionObj);
        }

        public static Enum GetUserSessionToken()
        {
            string clientData = GetClientInfo("user");
            string memberLoginApi = "https://developer.api.yodlee.com:443/ysl/restserver/v1/user/login";
            string cobrandSessionToken = authTokens["cobSession"];
            List<string> header = new List<string>();
            header.Add("Authorization:{cobSession= " + cobrandSessionToken + "}");
            string response = Web.httpPost(memberLoginApi, header, clientData, null);
            if (response == null)
            {
                Console.WriteLine("Please verify your username/password");
                return Status.FAILURE;

            }
            else
            {
                var rootObject = JsonConvert.DeserializeObject<UserDeserializer>(response);
                authTokens["userSession"] = rootObject.user.session.userSession;
                return Status.SUCCESS;
            }
        }

        public static Enum getAccessToken()
        {
            string accessTokenApi = "https://developer.api.yodlee.com:443/ysl/restserver/v1/user/accessTokens";
            List<string> header = new List<string>();
            header.Add("Authorization:{cobSession=" + authTokens["cobSession"] + ",userSession=" + authTokens["userSession"] + "}");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["appIds"] = "10003600";
            string response = Web.httpGet(accessTokenApi, header, parameters);
            if(response == null)
            {
                Console.WriteLine("Your session has timed out\n \r");
                return Status.FAILURE;
            }
            else
            {
                var rootObject = JsonConvert.DeserializeObject<DeserializeAccess>(response);
                authTokens["accessToken"] = rootObject.user.accessTokens[0].value;
                authTokens["nodeUrl"] = rootObject.user.accessTokens[0].value;
                return Status.SUCCESS;
            }
        }

        public static Enum getAccounts()
        {
            string getAccountUrl = "https://developer.api.yodlee.com:443/ysl/restserver/v1/accounts";
            List<string> header = new List<string>();
            header.Add("Authorization:{cobSession= " + authTokens["cobSession"] + ",userSession=" + authTokens["userSession"] + "}");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["status"] = "";
            parameters["container"] = "";
            parameters["providerAccountID"] = "";
            string jsonResponse = Web.httpGet(getAccountUrl, header, parameters);
            if (jsonResponse == null)
            {
                Console.WriteLine("Your session has timed out\n \r");
                return Status.FAILURE;
            }
            else
            {
                XmlHandler.CreateXMLOutFile(jsonResponse,"accounts.xml");
                return Status.SUCCESS;    
            }
        }
        public static Enum getTransactions()
        {
            string getAccountUrl = "https://developer.api.yodlee.com:443/ysl/restserver/v1/accounts";
            List<string> headers = new List<string>();
            headers.Add("Authorization:{cobSession= " + authTokens["cobSession"] + ",userSession=" + authTokens["userSession"] + "}");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["container"] = "bank";
            string jsonResponse = Web.httpGet(getAccountUrl, headers, parameters);
            if (jsonResponse == null)
            {
                Console.WriteLine("Your session has timed out\n \r");
                return Status.FAILURE;
            }
            else
            {
                XmlHandler.CreateXMLOutFile(jsonResponse,"transactions.xml");
                return Status.SUCCESS;
            }
        }
    }
}

/* Assumptions made in this class with respect to inputs:
cobrandLogin      = "sbCobphalageri"
cobrandPassword   = "5e26696f-028d-41bd-9c0a-7ad3f6956702"
loginName         = "sbMemphalageri1"
password          = "sbMemphalageri1#123"
appIds            = "10003600"
container         = "bank"


Optional:            
status            = ""
container         = ""
providerAccountID = ""
*/
