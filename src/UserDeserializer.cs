namespace SampleApiProj
{
        public class UserDeserializer
        {
            public User user { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string loginName { get; set; }
            public Name name { get; set; }
            public string roleType { get; set; }
            public Session session { get; set; }
            public Preferences preferences { get; set; }
        }

        public class Name
        {
            public string first { get; set; }
            public string last { get; set; }
        }

        public class Session
        {
            public string userSession { get; set; }
        }

        public class Preferences
        {
            public string currency { get; set; }
            public string timeZone { get; set; }
            public string dateFormat { get; set; }
        }

    }