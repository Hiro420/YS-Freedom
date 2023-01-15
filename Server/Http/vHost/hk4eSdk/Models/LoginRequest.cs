using System;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models
{
    public class LoginRequest
    {
        public string account { get; set; }
        public string password { get; set; }

        public bool is_crypto { get; set; }
    }
}