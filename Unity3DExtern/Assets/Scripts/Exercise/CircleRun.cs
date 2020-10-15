using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Run(int maxcount, int inner) 
    {
        var list = new List<int>();
        for (int i = 0; i < maxcount; i++)
        {
            list.Add(i);
        }
    }
}
