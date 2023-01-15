using System;
using System.Collections.Generic;
using System.Text;

namespace YSFreedom.Common.HttpApi.vHost.TakumiApi.Models
{
    internal class CheckModel
    {
        internal class Data
        {
            public string id { get; set; }
            public string action { get; set; }
            public object geetest { get; set; }
        }

        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
