
namespace eWeLink_API
{
    internal class VarClass
    {
/*        private static string _appid;
        public static string appid
        {
            get
            {
                return _appid;
            }
            set
            {
                _appid = value;
            }
        }*/
        public static string Appid { get; set; }
        public static string Apikey { get; set; }
        public static string At { get; set; } = "";
        public static string Secret { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string Salt_Secret { get; set; } = "15021375505qpwoeirutyalskdjfhgmznxbcv";

    }
}
