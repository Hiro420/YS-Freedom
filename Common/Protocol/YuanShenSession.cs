using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSFreedom.Common.Net;
using YSFreedom.Common.Crypto;
using YSFreedom.Common.Util;


namespace YSFreedom.Common.Protocol
{
    public class YuanShenSession
    {
        public YuanShenKey Key = null;
        private KCP kcpConn = null;

        public YuanShenSession(KCP conn)
        {
            kcpConn = conn;
        }

        private void Crypt(byte[] buffer)
        {
            if (Key == null)
                Key = KeyRecovery.FindKey(buffer);

            if (Key == null)
                throw new Exception("Unable to determine XOR key for session");

            Key.Crypt(buffer);
        }

        public async Task<byte[]> ReceiveAsync()
        {
            var ret = await kcpConn.ReceiveAsync();
            if (ret.GetUInt16(0, true) != 0x4567)
                Crypt(ret);

            return ret;
        }

        public async Task<int> SendAsync(byte[] data, bool crypt = true)
        {
            var copy = data.ToArray();
            if (crypt) Crypt(copy);

            return await kcpConn.SendAsync(copy);
        }
    }
}
