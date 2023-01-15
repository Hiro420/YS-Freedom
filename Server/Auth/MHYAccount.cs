using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YSFreedom.Server.Auth
{
    public class MHYAccount
    {
        [Key]
        public ulong UID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string RealName { get; set; }
        public string CountryCode { get; set; }

        public string NickName { get; set; }

        public bool VerifyPassword(string pass)
        {
            // TODO: hashing
            return Password == pass;
        }

        public void SetPassword(string pass)
        {
            Password = pass;
        }
    }
}