using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TMAS
{
    public class TMASTools
    {
        public static byte[] PackNetMsg<T>(T msg) where T : TMASMsg 
        {
            return PackLenInfo();
        }

        public static byte[] PackLenInfo(byte[] data) 
        {
            int len = data.Length;
            byte[] pkg = new byte[len + 4];
            byte[] head = BitConverter.GetBytes(len);
            head.CopyTo(pkg, 4);
            data.CopyTo(pkg, 4);
            return pkg;
        }

        public static byte[] Serialize<T>(T msg) where T :  TMASMsg
        {
            using(MemoryStream ms = new MemoryStream())
         }
    }
}