using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static Transform baseRoot;
    public static Transform BaseRoot
    {
        get
        {
            if (baseRoot == null)
            {
                baseRoot = GameObject.Find("BaseRoot").transform;
            }

            return baseRoot;
        }
    }
    
    private static GameObject goPrefab;
    public static GameObject GoPrefab
    {
        get
        {
            if (goPrefab == null)
            {
                goPrefab = GameObject.Find("BaseGo");
            }

            return goPrefab;
        }
    }
    
    private static T instance;
    
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find(typeof(T).Name);
                if (go == null)
                {
                    //Debug.Log("未找到名为" + typeof(T).Name + "的物体，帮你生成了");
                    go = Instantiate(GoPrefab, BaseRoot);
                    go.name = typeof(T).Name;
                }

                instance = go.GetComponent<T>();
                if (instance == null)
                {
                    //Debug.Log("未找到名为" + typeof(T).Name + "的组件，帮你生成了");
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}
