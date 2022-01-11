using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIExtern
{
    
    public static void SetBtnText(this Button btn, string str)
    {
        var ts =  TSButtonExtern.Get(btn.gameObject);
        // ReSharper disable once Unity.NoNullPropagation
        ts?.SetText(str);
    }
}
