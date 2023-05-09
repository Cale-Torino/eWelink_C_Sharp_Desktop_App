using System.Runtime.Serialization;

namespace eWeLink_API.Classes.JsonClasses
{
    internal class LoginPayloadClass
    {
        [DataContract]
        public class Rootobject
        {
            [DataMember]
            public string appid { get; set; } 
            [DataMember]
            public string email { get; set; } 
            [DataMember]
            public string phoneNumber { get; set; }
            [DataMember]
            public string password { get; set; } 
            [DataMember]
            public long ts { get; set; }
            [DataMember]
            public int version { get; set; } = 8;
            [DataMember]
            public string nonce { get; set; }
        }

    }
}
