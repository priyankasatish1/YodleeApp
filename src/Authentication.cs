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
                Logger.Log("Wrong cobrand username/password");
                return Status.FAILURE;
            }
            else
            {
                var rootObject = JsonConvert.DeserializeObject<Deserializer>(response);
                authTokens["cobSession"] = rootObject.session.cobSession;
                Logger.Log("Cobrand session token obtained");
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
                Logger.Log("Getting " + session + " Token for " + userCredentials["cobrandLogin"]);
            } else if(session == "user")
            {
                userCredentials["loginName"] = "sbMemphalageri2";
                userCredentials["password"] = "sbMemphalageri2#123";
                Logger.Log("Getting " + session + " Token for " + userCredentials["loginName"]);
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
                Logger.Log("Please verify your username/password");
                return Status.FAILURE;

            }
            else
            {
                var rootObject = JsonConvert.DeserializeObject<UserDeserializer>(response);
                authTokens["userSession"] = rootObject.user.session.userSession;
                Logger.Log("User session token obtained");
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
            Logger.Log("Getting access token for Fastlink for appId " + parameters["appIds"]);
            if (response == null)
            {
                Logger.Log("Your session has timed out");
                return Status.FAILURE;
            }
            else
            {
                var rootObject = JsonConvert.DeserializeObject<DeserializeAccess>(response);
                authTokens["accessToken"] = rootObject.user.accessTokens[0].value;
                authTokens["nodeUrl"] = rootObject.user.accessTokens[0].value;
                Logger.Log("Access token obtained");
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
            Logger.Log("Obtaining Account information");
            string jsonResponse = Web.httpGet(getAccountUrl, header, parameters);
            if (jsonResponse == null)
            {
                Logger.Log("Your session has timed out\n \r");
                return Status.FAILURE;
            }
            else
            {
                string fName = "accounts.json";
                JsonFileHandler.CreateJsonOutFile(jsonResponse, fName);
                Logger.Log("Writing account information to file " + fName);
                return Status.SUCCESS;    
            }
        }
        public static Enum getTransactions()
        {
            string getTransactionsUrl = "https://developer.api.yodlee.com:443/ysl/restserver/v1/transactions";
            List<string> headers = new List<string>();
            headers.Add("Authorization:{cobSession= " + authTokens["cobSession"] + ",userSession=" + authTokens["userSession"] + "}");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["fromDate"] = "2013-01-01";
            parameters["toDate"] = "2017-05-05";
            Logger.Log("Obtaining Transactions");
            string jsonResponse = Web.httpGet(getTransactionsUrl, headers, parameters);
            if (jsonResponse == null)
            {
                Logger.Log("Your session has timed out\n \r");
                return Status.FAILURE;
            }
            else
            {
                string fName = "transactions.json";
                JsonFileHandler.CreateJsonOutFile(jsonResponse, fName);
                Logger.Log("Writing account transaction information to file " + fName);
                return Status.SUCCESS;
            }
        }
    }
}

/* Assumptions made in this class with respect to inputs:
cobrandLogin      = "sbCobphalageri"
cobrandPassword   = "5e26696f-028d-41bd-9c0a-7ad3f6956702"
loginName         = "sbMemphalageri2"
password          = "sbMemphalageri2#123"
appIds            = "10003600"
container         = "bank"


Optional:            
status            = ""
container         = ""
providerAccountID = ""
*/
