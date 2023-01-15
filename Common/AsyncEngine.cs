using System;
using System.Threading.Tasks;
using Jint;
using Jint.Runtime;

namespace YSFreedom.Common.Scripting
{
    public class AsyncEngine : Engine
    {
        private object engineLock = new Object();

        public AsyncEngine() : base()
        {
        }

        public AsyncEngine(Options options) : base(options)
        {
        }

        public AsyncEngine(Action<Options> actionOptions) : base(actionOptions)
        {
        }

        public async Task<object> ExecuteAsync(string source)
        {
            await Task.Yield();
            lock (engineLock)
            {
                return this.Execute(source).GetCompletionValue().ToObject();
            }
        }

        public async Task<object> InvokeAsync(string name, params object[] args)
        {
            await Task.Yield();
            lock (engineLock)
            {
                return this.Invoke(name, args).ToObject();
            }
        }
    }
}