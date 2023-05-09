using System.Runtime.Serialization;

namespace eWeLink_API
{
    internal class eWelinkLoginJsonClass
    {
        [DataContract]
        public class Rootobject
        {
            [DataMember]
            public string at { get; set; }
            [DataMember]
            public string rt { get; set; }
            [DataMember]
            public User user { get; set; }
            [DataMember]
            public string region { get; set; }
        }
        [DataContract]
        public class User
        {
            [DataMember]
            public Clientinfo clientInfo { get; set; }
            [DataMember]
            public string _id { get; set; }
            [DataMember]
            public string email { get; set; }
            [DataMember]
            public string password { get; set; }
            [DataMember]
            public string appId { get; set; }
            [DataMember]
            public string countryCode { get; set; }
            [DataMember]
            public Accountinfo accountInfo { get; set; }
            [DataMember]
            public string apikey { get; set; }
            [DataMember]
            public string createdAt { get; set; }
            //public DateTime createdAt { get; set; }
            [DataMember]
            public int __v { get; set; }
            [DataMember]
            public Extrapush extraPush { get; set; }
            [DataMember]
            public string language { get; set; }
            [DataMember]
            public string lang { get; set; }
            [DataMember]
            public string currentFamilyId { get; set; }
            [DataMember]
            public Extra extra { get; set; }
            [DataMember]
            public Appinfo[] appInfos { get; set; }
            [DataMember]
            public bool online { get; set; }
            [DataMember]
            public string onlineTime { get; set; }
            //public DateTime onlineTime { get; set; }
            [DataMember]
            public string ip { get; set; }
            [DataMember]
            public string location { get; set; }
            [DataMember]
            public string offlineTime { get; set; }
            //public DateTime offlineTime { get; set; }
        }
        [DataContract]
        public class Clientinfo
        {
            [DataMember]
            public string model { get; set; }
            [DataMember]
            public string os { get; set; }
            [DataMember]
            public string imei { get; set; }
            [DataMember]
            public string romVersion { get; set; }
            [DataMember]
            public string appVersion { get; set; }
        }
        [DataContract]
        public class Accountinfo
        {
            [DataMember]
            public int level { get; set; }
        }
        [DataContract]
        public class Extrapush
        {
            [DataMember]
            public Uw83ekzfxdif7xfxesrpduz5yyjp7ntl Uw83EKZFxdif7XFXEsrpduz5YyjP7nTl { get; set; }
        }
        [DataContract]
        public class Uw83ekzfxdif7xfxesrpduz5yyjp7ntl
        {
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public Info info { get; set; }
        }
        [DataContract]
        public class Info
        {
            [DataMember]
            public string token { get; set; }
        }
        [DataContract]
        public class Extra
        {
            [DataMember]
            public string ipCountry { get; set; }
        }
        [DataContract]
        public class Appinfo
        {
            [DataMember]
            public string os { get; set; }
            [DataMember]
            public string appVersion { get; set; }
        }

    }
}
