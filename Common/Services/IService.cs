using System;
using System.Threading.Tasks;

namespace YSFreedom.Common.Services
{
    public delegate IService IServiceConstructor();
    public interface IService
    {
        public Task Start();
        public void Stop();
        public void Configure(object config);
    }
}