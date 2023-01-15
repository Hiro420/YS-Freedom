using System;
using System.Collections.Generic;
using System.Text;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models
{
    public class GetConfigResponse
    {
        public class Data
        {
            public bool protocol { get; set; }
            public bool qr_enabled { get; set; }
            public string log_level { get; set; }
            public string announce_url { get; set; }
            public int push_alias_type { get; set; }
            public bool disable_ysdk_guard { get; set; }
            public bool enable_announce_pic_popup { get; set; }
        }
        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
