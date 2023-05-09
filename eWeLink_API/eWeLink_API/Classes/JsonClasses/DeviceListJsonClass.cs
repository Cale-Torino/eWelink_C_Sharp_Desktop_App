using System.Runtime.Serialization;

namespace eWeLink_API
{
    internal class DeviceListJsonClass
    {
        [DataContract]
        public class Rootobject
        {
            [DataMember]
            public int error { get; set; }
            [DataMember]
            public Devicelist[] devicelist { get; set; }
        }
        [DataContract]
        public class Devicelist
        {
            [DataMember]
            public Settings settings { get; set; }
            [DataMember]
            public Family family { get; set; }
            [DataMember]
            public string group { get; set; }
            [DataMember]
            public bool online { get; set; }
            [DataMember]
            public object[] groups { get; set; }
            [DataMember]
            public object[] devGroups { get; set; }
            [DataMember]
            public string _id { get; set; }
            [DataMember]
            public object[] relational { get; set; }
            [DataMember]
            public string deviceid { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public string apikey { get; set; }
            [DataMember]
            public Extra extra { get; set; }
            [DataMember]
            public Params _params { get; set; }
            [DataMember]
            public string createdAt { get; set; }
            [DataMember]
            public int __v { get; set; }
            [DataMember]
            public string onlineTime { get; set; }
            [DataMember]
            public string ip { get; set; }
            [DataMember]
            public string location { get; set; }
            [DataMember]
            public Tags tags { get; set; }
            [DataMember]
            public string offlineTime { get; set; }
            [DataMember]
            public Sharedby sharedBy { get; set; }
            [DataMember]
            public string devicekey { get; set; }
            [DataMember]
            public string deviceUrl { get; set; }
            [DataMember]
            public string brandName { get; set; }
            [DataMember]
            public bool showBrand { get; set; }
            [DataMember]
            public string brandLogoUrl { get; set; }
            [DataMember]
            public string productModel { get; set; }
            [DataMember]
            public Devconfig devConfig { get; set; }
            [DataMember]
            public int uiid { get; set; }
            [DataMember]
            public string address { get; set; }
        }
        [DataContract]
        public class Settings
        {
            [DataMember]
            public int opsNotify { get; set; }
            [DataMember]
            public int opsHistory { get; set; }
            [DataMember]
            public int alarmNotify { get; set; }
            [DataMember]
            public int wxAlarmNotify { get; set; }
            [DataMember]
            public int wxOpsNotify { get; set; }
            [DataMember]
            public int wxDoorbellNotify { get; set; }
            [DataMember]
            public int appDoorbellNotify { get; set; }
            [DataMember]
            public int offlineNotify { get; set; }
        }
        [DataContract]
        public class Family
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public int index { get; set; }
            [DataMember]
            public object[] members { get; set; }
        }
        [DataContract]
        public class Extra
        {
            [DataMember]
            public Extra1 extra { get; set; }
            [DataMember]
            public string _id { get; set; }
        }
        [DataContract]
        public class Extra1
        {
            [DataMember]
            public int uiid { get; set; }
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public string brandId { get; set; }
            [DataMember]
            public string apmac { get; set; }
            [DataMember]
            public string mac { get; set; }
            [DataMember]
            public string ui { get; set; }
            [DataMember]
            public string modelInfo { get; set; }
            [DataMember]
            public string model { get; set; }
            [DataMember]
            public string manufacturer { get; set; }
            [DataMember]
            public string chipid { get; set; }
            [DataMember]
            public string staMac { get; set; }
        }
        [DataContract]
        public class Params
        {
            [DataMember]
            public int version { get; set; }
            [DataMember]
            public string sledOnline { get; set; }
            [DataMember]
            public string _switch { get; set; }
            [DataMember]
            public string fwVersion { get; set; }
            [DataMember]
            public int rssi { get; set; }
            [DataMember]
            public string staMac { get; set; }
            [DataMember]
            public string startup { get; set; }
            [DataMember]
            public int init { get; set; }
            [DataMember]
            public string pulse { get; set; }
            [DataMember]
            public int pulseWidth { get; set; }
            [DataMember]
            public Only_Device only_device { get; set; }
            [DataMember]
            public string ssid { get; set; }
            [DataMember]
            public string bssid { get; set; }
            [DataMember]
            public string alarmType { get; set; }
            [DataMember]
            public int[] alarmVValue { get; set; }
            [DataMember]
            public int[] alarmCValue { get; set; }
            [DataMember]
            public int[] alarmPValue { get; set; }
            [DataMember]
            public string power { get; set; }
            [DataMember]
            public string voltage { get; set; }
            [DataMember]
            public string current { get; set; }
            [DataMember]
            public string oneKwh { get; set; }
            [DataMember]
            public int uiActive { get; set; }
            [DataMember]
            public string hundredDaysKwh { get; set; }
            [DataMember]
            public int timeZone { get; set; }
            [DataMember]
            public object[] timers { get; set; }
            [DataMember]
            public string endTime { get; set; }
            [DataMember]
            public string startTime { get; set; }
            [DataMember]
            public long demNextFetchTime { get; set; }
            [DataMember]
            public Switch[] switches { get; set; }
            [DataMember]
            public Configure[] configure { get; set; }
        }
        [DataContract]
        public class Only_Device
        {
            [DataMember]
            public string ota { get; set; }
        }
        [DataContract]
        public class Switch
        {
            [DataMember]
            public string _switch { get; set; }
            [DataMember]
            public int outlet { get; set; }
        }
        [DataContract]
        public class Configure
        {
            [DataMember]
            public string startup { get; set; }
            [DataMember]
            public int outlet { get; set; }
        }
        [DataContract]
        public class Tags
        {
            [DataMember]
            public string m_77d1_cale { get; set; }
            [DataMember]
            public string m_85c7_mstm { get; set; }
            [DataMember]
            public string m_85d2_vmal { get; set; }
            [DataMember]
            public string m_b0f2_annd { get; set; }
            [DataMember]
            public Disable_Timers[] disable_timers { get; set; }
            [DataMember]
            public string m_dc74_carr { get; set; }
        }
        [DataContract]
        public class Disable_Timers
        {
            [DataMember]
            public string mId { get; set; }
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public string at { get; set; }
            [DataMember]
            public string coolkit_timer_type { get; set; }
            [DataMember]
            public int enabled { get; set; }
            [DataMember]
            public Do _do { get; set; }
            [DataMember]
            public string period { get; set; }
        }
        [DataContract]
        public class Do
        {
            [DataMember]
            public string _switch { get; set; }
        }
        [DataContract]
        public class Sharedby
        {
            [DataMember]
            public string email { get; set; }
            [DataMember]
            public string apikey { get; set; }
            [DataMember]
            public string nickname { get; set; }
            [DataMember]
            public int permit { get; set; }
            [DataMember]
            public long shareTime { get; set; }
        }
        [DataContract]
        public class Devconfig
        {
        }

    }
}
