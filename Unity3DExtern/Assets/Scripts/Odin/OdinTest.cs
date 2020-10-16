using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;

public class OdinTest : SerializedMonoBehaviour
{
    [AssetsOnly]
    public List<GameObject> m_ListGo;
    [SceneObjectsOnly]
    public List<GameObject> m_ListGoScene;
    [SerializeReference]
    private Dictionary<int,GameObject> m_DicOdin;

    public Dictionary<int,GameObject> m_DicOdin2;

    public UnitySerialize m_UnityObj;
    public OdinSerializeTest m_OdinObj;
    void Start() 
    {
        //Debug.Log("m_DicOdin count" + m_DicOdin.Count);
        Debug.Log("m_DicOdin2 count" + m_DicOdin2.Count);
    }
}

[Serializable]
public class UnitySerialize 
{
    [OdinSerialize]
    public Dictionary<int,GameObject> m_DicOdinUnitySer;

}

public class OdinSerializeTest
{
    [OdinSerialize]
    public Dictionary<int,GameObject> m_DicOdinUnitySer;
}
