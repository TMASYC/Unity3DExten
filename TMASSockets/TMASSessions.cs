using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TMAS
{
    public class TMASSessions <T> where T : TMASMsg
    {
        private Socket m_Skt;
        private Action m_CloseCB;

        #region Receive

        public void StartReceiveData(Socket skt, Action CloseCb) 
        {
            try
            {
                this.m_Skt = skt;
                this.m_CloseCB = CloseCb;

                OnConnected();
                TMASPkg pack = new TMASPkg();
                m_Skt.BeginReceive(pack.headBuffer,
                    0,
                    pack.headLen,
                    SocketFlags.None,
                    new AsyncCallback(ReceiveHeadData),
                    pack
                );

            }
            catch (System.Exception e)
            {
                
                throw;
            }

        }

        void ReceiveHeadData(IAsyncResult result) 
        {
            try
            {
                TMASPkg pack = (TMASPkg)result.AsyncState;
                //如果可用的链接数 等于0
                if (m_Skt.Available == 0) 
                {
                    OnDisConnected();
                    Clear();
                    return;
                }

                int len = m_Skt.EndReceive(result);
                if (len > 0) 
                {
                    pack.headIndex += len;
                    if (pack.headIndex < pack.headLen) 
                    {
                        m_Skt.BeginReceive(
                            pack.headBuffer,
                            pack.headIndex,
                            pack.headLen - pack.headIndex,
                            SocketFlags.None,
                            new AsyncCallback(ReceiveHeadData),
                            pack
                        );
                    }else 
                    {
                        pack.InitBodyBuff();
                        m_Skt.BeginReceive(
                            pack.bodyBuffer,
                            0,
                            pack.bodyLen,
                            SocketFlags.None,
                            new AsyncCallback(ReceiveBodyData),
                            pack
                        );
                    }
                }
                else 
                {
                    OnDisConnected();
                    Clear();
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }

        }

        void ReceiveBodyData(IAsyncResult result)
        {
            try
            {
                TMASPkg pack = new TMASPkg();
                int len = m_Skt.EndReceive(result);
                if (len > 0) 
                {
                    pack.bodyIndex += len;
                    if (pack.bodyIndex < pack.bodyLen) 
                    {
                        m_Skt.BeginReceive(pack.bodyBuffer,
                            pack.bodyIndex,
                            pack.bodyLen - pack.bodyIndex,
                            SocketFlags.None,
                            new AsyncCallback(ReceiveBodyData),
                            pack

                        );
                    }
                    else 
                    {
                        // T msg = 
                    }
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        #endregion


        protected void Clear() 
        { 

        }


        protected virtual void OnConnected() 
        {

        }
        protected virtual void OnDisConnected() 
        {
            
        }
        protected virtual void ONReceiveMsg(T msg) 
        {
            
        }
    }
}