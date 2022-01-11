using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEdgeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Material m_material;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        m_material.SetFloat("_EdgeOnly", 0);
       // "shaderTry/ImageEdge"为shader的完整名字，即shader文件中Shader "shaderTry/ImageEdge"{}
        Graphics.Blit(src, dest, m_material);
    }
}

public class TestProperty
{
    public int IntNum
    {
        get;
        set;
    }
}

public class TestMain
{
    public void Main(object[] args)
    {
        TestProperty testProperty = new TestProperty();
        //testProperty.DataBindings.Add
    }
}
