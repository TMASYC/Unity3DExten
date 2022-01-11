using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Sockets;

namespace TMAS
{
    public class TMASSockets <T>
    {
        private Socket m_Skt;
        private int m_BackLog;

        public TMASSockets() 
        {

            m_Skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        #region server

        public void StartAsServer(string ip, int port) 
        {
            try
            {
                m_Skt.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                //排队等待链接的最大数量
                m_Skt.Listen(m_BackLog);
                //开始接受链接，并在链接后回调
                m_Skt.BeginAccept(new AsyncCallback(ClientConnectCB), m_Skt);
                
                //因为此类在客户端和服务端同时使用，所以log打印的方法需要进行重组
            }
            catch (System.Exception e)
            {
                
                throw;
            }  
        }

        void ClientConnectCB(IAsyncResult result) 
        {
            try
            {
                Socket clientSkt = m_Skt.EndAccept(result);
                //根据每个socket去创建对应的Session
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }


        #endregion
    }
}