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
            using (MemoryStream ms = new MemoryStream()) 
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T msg = (T)bf.Deserialize(ms, msg);
                    ms.Seek(0, SeekOrigin.Begin);
                    return Compress(ms.ToArray());
                }
                catch (System.Exception)
                {
                    
                    throw;
                    return null;
                }
             }
        }

        public static T DeSerialize<T>(byte[] bytes) where T : TMASMsg 
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T msg = (T)bf.Deserialize(ms);
                    return msg;
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }
        }


        public static byte[] Compress(byte[] input) 
        {
            using (MemoryStream outms = new MemoryStream()) 
            { 
                try
                {
                    using (GZipStream gzs = new GZipStream(outms, CompressionMode.Compress, true)) 
                    {
                        gzs.Write(input, 0 , input.Length);
                        gzs.Close();
                        return outms.ToArray();
                    }
                }
                catch (System.Exception)
                {
                    
                    throw;
                    return null;
                }
            }
        }

        public static byte[] DeCompress(byte[] input) 
        {
            using (MemoryStream inoutMs = new MemoryStream(input))
            {
                using (MemoryStream OutMs = new MemoryStream())
                {
                    using (GZipStream gzs = new GZipStream(inoutMs, CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int len = 0;
                        while ((len = gzs.Read(bytes, 0, bytes.Length)) > 0) 
                        {
                            OutMs.Write(bytes, 0, len);
                        }
                        gzs.Close();
                        return OutMs.ToArray();

                    }
                }
            }
        }
    }
}