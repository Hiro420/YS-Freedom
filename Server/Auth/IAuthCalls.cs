using System;
using System.Threading.Tasks;

namespace YSFreedom.Server.Auth
{
    public interface IAuthCalls
    {
        public Task<MHYAccount> Login(string username, string password);
        public Task<MHYAccount> GetAccountByUID(ulong uid);
    }
}