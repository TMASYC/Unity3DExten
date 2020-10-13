using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{

    public static void DestroyAllChildren(this Transform transform)
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
            else
#endif
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }

    }

    public static void DisableAllChildren(this Transform transform)
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public static void SetLayer(this GameObject go, int layerNumber, bool includeInactive = false, bool withChildren = false)
    {
        go.layer = layerNumber;

        if (withChildren)
        {
            foreach (Transform trans in go.GetComponentsInChildren<Transform>(includeInactive))
            {
                trans.gameObject.layer = layerNumber;
            }
        }
    }

    public static void SetLayer(this GameObject go, string layerName, bool includeInactive = false, bool withChildren = false)
    {
        int layer = LayerMask.NameToLayer(layerName);
        go.SetLayer(layer, includeInactive, withChildren);
    }

    public static Transform[] GetFirstLevelChildren(this Transform parent, bool includeInactive)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(includeInactive);
        List<Transform> firstChildren = new List<Transform>();

        foreach (Transform child in children)
        {
            if (child.parent == parent)
            {
                firstChildren.Add(child);
            }
        }
        return firstChildren.ToArray();
    }
}