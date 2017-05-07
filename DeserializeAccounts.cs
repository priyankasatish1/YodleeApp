using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApiProj
{

    public class DeserializeAccounts
    {
        public Account[] account { get; set; }
    }

    public class Account
    {
        public string CONTAINER { get; set; }
        public int providerAccountId { get; set; }
        public string accountName { get; set; }
        public string accountStatus { get; set; }
        public string url { get; set; }
        public string accountNumber { get; set; }
        public bool isAsset { get; set; }
        public Balance balance { get; set; }
        public int id { get; set; }
        public DateTime lastUpdated { get; set; }
        public bool includeInNetWorth { get; set; }
        public string providerId { get; set; }
        public string providerName { get; set; }
        public string accountType { get; set; }
        public Availablecash availableCash { get; set; }
        public Availablecredit availableCredit { get; set; }
        public Totalcashlimit totalCashLimit { get; set; }
        public Totalcreditline totalCreditLine { get; set; }
        public bool isManual { get; set; }
        public DateTime createdDate { get; set; }
        public Refreshinfo refreshinfo { get; set; }
        public Availablebalance availableBalance { get; set; }
        public Currentbalance currentBalance { get; set; }
        public Holderprofile[] holderProfile { get; set; }
        public string nickname { get; set; }
        public string memo { get; set; }
        public string loginName { get; set; }
        public string password { get; set; }
    }

    public class Balance
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Availablecash
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class Availablecredit
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class Totalcashlimit
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class Totalcreditline
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class Refreshinfo
    {
        public int statusCode { get; set; }
        public string statusMessage { get; set; }
        public DateTime lastRefreshed { get; set; }
        public DateTime lastRefreshAttempt { get; set; }
        public DateTime nextRefreshScheduled { get; set; }
    }

    public class Availablebalance
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Currentbalance
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Holderprofile
    {
        public HName name { get; set; }
    }

    public class HName
    {
        public string displayed { get; set; }
    }

}
