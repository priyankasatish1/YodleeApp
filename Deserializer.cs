using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApiProj
{
    class Deserializer
    {

            public int cobrandId { get; set; }
            public string applicationId { get; set; }
            public string locale { get; set; }
            public Session session { get; set; }

        public class Session
        {
            public string cobSession { get; set; }
        }

    }
}
