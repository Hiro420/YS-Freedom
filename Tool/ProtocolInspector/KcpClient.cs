using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Native;

namespace ProtocolInspector
{
    public class KcpClient
    {
        public delegate int OutputDelegate(byte[] buffer);

        public OutputDelegate Output;
        private UIntPtr ikcpHandle;
        private object ikcpLock = new object();

        public KcpClient(UInt32 _Conv, UInt32 _Token)
        {
            ikcpHandle = IKCP.ikcp_create(_Conv, _Token, UIntPtr.Zero);
            IKCP.ikcp_setoutput(ikcpHandle, thunk_Output);
        }

        public void Input(byte[] buffer)
        {
            var status = IKCP.ikcp_input(ikcpHandle, buffer, buffer.Length);
        }

        public byte[] Get()
        {
            int size = IKCP.ikcp_peeksize(ikcpHandle);
            if (size < 0) return null;

            var buffer = new byte[size];
            int trueSize = IKCP.ikcp_recv(ikcpHandle, buffer, buffer.Length);
            if (trueSize != size) throw new Exception("Unexpected state");

            return buffer;
        }

        private int thunk_Output(byte[] buffer, int len, UIntPtr kcp, UIntPtr userdata)
        {
            if (Output == null) return 0;
            return Output(buffer);
        }
    }
}
