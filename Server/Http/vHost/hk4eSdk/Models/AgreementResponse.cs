using System;
using System.Collections.Generic;
using System.Text;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models
{
    public class AgreementResponse
    {
        public class Data
        {
            public object[] marketing_agreements { get; set; }
        }
        public int retcode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
