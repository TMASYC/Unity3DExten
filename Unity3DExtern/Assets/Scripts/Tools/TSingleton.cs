

    public class TSingleton<T> : ISinglton where T : TSingleton<T>, new()
    {
        protected static T m_Instance;
        protected static object s_Lock = new object();


        public static T S
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (s_Lock)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new T();
                            m_Instance.OnSingletonInit();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public static T ResetInstance()
        {
            m_Instance = new T();
            m_Instance.OnSingletonInit();
            return m_Instance;
        }

        public virtual void OnSingletonInit()
        {
            
        }
    }
