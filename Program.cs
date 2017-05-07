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
            Console.WriteLine("Starting Sample Api App");
                        
            Enum cobrandStatus = Authentication.GetCobrandSessionToken();

            if (cobrandStatus.Equals(Status.SUCCESS))
            {
                Enum memberStatus = Authentication.GetUserSessionToken();
                if (memberStatus.Equals(Status.FAILURE))
                {
                    Console.WriteLine("Authentication Failed... Check User Credentials.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Authentication Failed... Check cobrandLogin Credentials.");
                return;
            }


            Enum userAccountStatus = Authentication.getAccounts();
            if(userAccountStatus.Equals(Status.FAILURE))
            {
                Console.WriteLine("Unable to get account info... Authenticate and try again.");
                return;
            }


            Enum userTransactionsStatus = Authentication.getTransactions();

            Enum accessToken = Authentication.getAccessToken();
            if(accessToken.Equals(Status.SUCCESS))
            {
                Fastlink fs = new Fastlink();
                string fetchFilePath = fs.GetFilePath("fastlink.html");
                string outfile = fs.WriteTokenstoFile(fetchFilePath);
                System.Diagnostics.Process.Start(outfile);
            }
            else
            {
                Console.WriteLine("Unable to authenticate for the given App Id, try again.");
                return;
            }
        }
    }
}
