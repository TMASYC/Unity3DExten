using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PanelType : byte
{
    Bottom,
    Auto,
    Top,
}

public enum PanelHideMask : byte
{
    None,
    UnInteractive = 1,
    Hide = 2,
}

[RequireComponent(typeof(Canvas))]
public class AbstractPanel : MonoBehaviour
{
    
}
