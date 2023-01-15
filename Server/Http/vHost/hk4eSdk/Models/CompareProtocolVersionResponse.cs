using System;
using System.Collections.Generic;
using System.Text;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models
{
    public class CompareProtocolVersionResponse
    {
        public class Data
        {
            public class Protocol
            {
                public int id { get; set; }
                public int app_id { get; set; }
                public string language { get; set; }
                public string user_proto { get; set; }
                public string priv_proto { get; set; }
                public int major { get; set; }
                public int minimum { get; set; }
                public string create_time { get; set; }
            }
            public bool modified { get; set; }
            public Protocol protocol { get; set; }
        }
        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
