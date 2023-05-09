using System.Runtime.Serialization;

namespace eWeLink_API.Classes.JsonClasses
{
    internal class DeviceSwitchPayloadClass
    {
        [DataContract]
        public class Rootobject
        {
            [DataMember]
            public string deviceid { get; set; }
            [DataMember]
            public Params _params { get; set; }
            [DataMember]
            public string appid { get; set; }
            [DataMember]
            public string nonce { get; set; }
            [DataMember]
            public long ts { get; set; }
            [DataMember]
            public int version { get; set; }
        }
        [DataContract]
        public class Params
        {
            [DataMember]
            public string _switch { get; set; }
        }

    }
}
