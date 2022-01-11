using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSButtonExtern : MonoBehaviour
{
    private Text m_Text;
    private bool m_Inited;
    public void Start()
    {
        if (m_Text == null)
        {
            m_Text = GetComponentInChildren<Text>();
        }

        m_Inited = true;
    }

    public void SetText(string text)
    {
        m_Text.text = text;
    }

    static public TSButtonExtern Get(GameObject btn)
    {
        if (btn == null)
        {
            return null;
        }
        var ext = btn.GetComponent<TSButtonExtern>();
        if (ext == null)
        {
            ext = btn.AddComponent<TSButtonExtern>();
        }

        if (!ext.m_Inited)
        {
            ext.Start();
        }
        return ext;
    }


}
