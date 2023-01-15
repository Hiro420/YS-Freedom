using System;
using System.Collections.Generic;
using System.Text;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models
{

    public class LoginResponse
    {
        public class Data
        {
            public class Account
            {
                public string uid { get; set; }
                public string name { get; set; }
                public string email { get; set; }
                public string mobile { get; set; }
                public string is_email_verify { get; set; }
                public string realname { get; set; }
                public string identity_card { get; set; }
                public string token { get; set; }
                public string safe_mobile { get; set; }
                public string facebook_name { get; set; }
                public string google_name { get; set; }
                public string twitter_name { get; set; }
                public string game_center_name { get; set; }
                public string apple_name { get; set; }
                public string sony_name { get; set; }
                public string tap_name { get; set; }
                public string country { get; set; }
                public string reactivate_ticket { get; set; }
                public string area_code { get; set; }
            }
            public Account account { get; set; }
            public bool device_grant_required { get; set; }
            public bool safe_mobile_required { get; set; }
            public bool realperson_required { get; set; }
            public bool reactivate_required { get; set; }
        }

        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
