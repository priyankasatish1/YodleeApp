using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApiProj
{
    public class DeserializeAccess
    {
        public User_Access user { get; set; }
    }

    public class User_Access
    {
        public Accesstoken[] accessTokens { get; set; }
    }

    public class Accesstoken
    {
        public string appId { get; set; }
        public string value { get; set; }
        public string url { get; set; }
    }
}
