using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleApiProj
{
    enum Status { FAILURE, SUCCESS };
    class Program
    {
        public static void Main(string[] args)
        {
            Logger.Log("Starting Sample Api App");
                        
            // Fetch Cobrand token
            Enum cobrandStatus = Authentication.GetCobrandSessionToken();
            if (cobrandStatus.Equals(Status.SUCCESS))
            {
                Enum memberStatus = Authentication.GetUserSessionToken();
                if (memberStatus.Equals(Status.FAILURE))
                {
                    Logger.Log("Authentication Failed... Check User Credentials.");
                    return;
                }
            }
            else
            {
                Logger.Log("Authentication Failed... Check cobrandLogin Credentials.");
                return;
            }

            // Fetch User Token and open Fastlink
            Enum accessToken = Authentication.getAccessToken();
            if (accessToken.Equals(Status.SUCCESS))
            {
                Fastlink fs = new Fastlink();
                string fetchFilePath = fs.GetFilePath("fastlink.html");
                string outfile = fs.WriteTokenstoFile(fetchFilePath);
                System.Diagnostics.Process.Start(outfile);
            }
            else
            {
                Logger.Log("Unable to authenticate for the given App Id, try again.");
                return;
            }

            // Wait for user input for 2 minutes
            Logger.Log("Waiting for user input on FastLink");
            System.Threading.Thread.Sleep(90000);

            // Get Account info
            Enum userAccountStatus = Authentication.getAccounts();
            if(userAccountStatus.Equals(Status.FAILURE))
            {
                Logger.Log("Unable to get account info... Authenticate and try again.");
                return;
            }

            // Get Transaction info
            Enum userTransactionsStatus = Authentication.getTransactions();


        }
    }
}
