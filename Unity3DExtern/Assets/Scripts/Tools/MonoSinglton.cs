using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSinglton : MonoBehaviour
{
    private static bool m_IsApplicationQuit = false;

    public static bool isApplicationQuit
    {
        get => m_IsApplicationQuit;
        set => m_IsApplicationQuit = value;
    }

    public static K CreateMonoSingleton<K>() where K : MonoBehaviour, ISinglton
    {
        if (m_IsApplicationQuit)
        {
            return null;
        }

        K instance = null;
        if (instance == null && !m_IsApplicationQuit)
        {
            instance = GameObject.FindObjectOfType(typeof(K)) as K;
            if (instance == null)
            {
                System.Reflection.MemberInfo info = typeof(K);
                object[] attributes = info.GetCustomAttributes(true);
                for (int i = 0; i < attributes.Length; i++)
                {
                    MonoSingletonAttribute defineAttri = attributes[i] as MonoSingletonAttribute;
                    if (defineAttri == null)
                    {
                        continue;
                    }

                    instance = CreateComponentOnGameobject<K>(defineAttri.AbsolutePath, true);
                    break;
                }

                if (instance == null)
                {
                    GameObject obj = new GameObject("Singleton of " + typeof(K).Name);
                    UnityEngine.Object.DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<K>();
                }
            }
            
            instance.OnSingletonInit();
        }

        return instance;
    }

    public static K CreateComponentOnGameobject<K>(string path, bool dontdestoryOnload) where K : MonoBehaviour
    {
        GameObject obj = GameObject.Find(path);
        if (obj == null)
        {
            obj = new GameObject("Singleton of "+ typeof(K).Name);
            if (dontdestoryOnload)
            {
                DontDestroyOnLoad(obj);
            }
        }

        return obj.AddComponent<K>();
    }
}
