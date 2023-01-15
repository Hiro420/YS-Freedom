using System;
using System.Collections.Generic;
using System.Text;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models
{
    public class LoginGranterResponse
    {
        public class Data
        {
            public string combo_id { get; set; }
            public string open_id { get; set; }
            public string combo_token { get; set; }
            public string data { get; set; }
            public bool heartbeat { get; set; }
            public int account_type { get; set; }
        }

        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
