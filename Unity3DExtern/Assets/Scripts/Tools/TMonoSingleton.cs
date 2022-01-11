using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TMonoSingleton<T> : MonoSinglton, ISinglton where T : TMonoSingleton<T>
{
    private static T m_instance = null;
    private static object m_lock = new object();

    public static T S
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = CreateMonoSingleton<T>();
                    }
                }
            }

            return m_instance;
        }
    }

    public virtual void OnSingletonInit()
    {
        
    }
}

