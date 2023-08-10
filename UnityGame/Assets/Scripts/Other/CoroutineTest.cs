using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    public bool needCoroutine;

    public int frame;
    // Start is called before the first frame update
    void Start()
    {
        needCoroutine = true;
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        //Debug.Log(String.Format("Update at frame:{0}",frame));
        if(needCoroutine)
        {
            needCoroutine = false;
            StartCoroutine("MyCoroutine");   
        }
    }

    IEnumerator MyCoroutine()
    {
        //Debug.Log(String.Format("coroutine 1 Invoke at frame:{0}",frame));
        yield return 0;
        //Debug.Log(String.Format("coroutine 2 Invoke at frame:{0}",frame));
        yield return 1;
        //Debug.Log(String.Format("coroutine 3 Invoke at frame:{0}",frame));
    }

    private void LateUpdate()
    {
        //Debug.Log(String.Format("LateUpdate at frame:{0}",frame));
        if (frame >= 5)
        {
            this.enabled = false;
        }
    }
    
    
}