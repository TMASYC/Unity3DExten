using System;
using System.Net.Sockets;

namespace TMAS
{
    public class TMASPkg 
    {
        //头包长度
        public int headLen = 4;
        public byte[] headBuffer = null;
        public int headIndex;

        public int bodyLen = 0;
        public byte[] bodyBuffer = null;
        public int bodyIndex;

        public TMASPkg() 
        {
            headBuffer = new byte[headLen];
        }

        public void InitBodyBuff() 
        {
            bodyLen = BitConverter.ToInt32(headBuffer, 0);
            bodyBuffer = new byte[bodyLen];
        }

        public void ResetData() 
        {
            headIndex = 0;
            bodyLen = 0;
            bodyBuffer = null;
            bodyIndex = 0;

        }
    }
}