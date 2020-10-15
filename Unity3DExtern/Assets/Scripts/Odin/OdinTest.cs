using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class OdinTest : MonoBehaviour
{
    [AssetsOnly]
    public List<GameObject> m_ListGo;
    [SceneObjectsOnly]
    public List<GameObject> m_ListGoScene;
    [SerializeReference]
    private Dictionary<int,GameObject> m_DicOdin;
}
