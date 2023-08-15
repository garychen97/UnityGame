using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : SingletonMonoBehaviour<DrawManager>
{
    public GameObject linePrefab;
    public GameObject TileContainer;
    public GameObject tilePrefab;
    private static DrawManager instance;

/*
    #region 单例
    
    public static DrawManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("DrawManager");
                if (go == null)
                {
                    Debug.LogError("未找到名为DrawManager的物体");
                }
                instance = go.GetComponent<DrawManager>();
                if (instance == null)
                {
                    instance = go.AddComponent<DrawManager>();
                }
            }
            return instance;
        }
    }

    #endregion
    */

    public static void DrawLine(LineType type,float v1, float v2,float v3)
    {
        float from = Math.Min(v1, v2);
        float to = Math.Max(v1, v2);
        if (type == LineType.Horizontal)
        {
            GameObject line = Instantiate(Instance.linePrefab);
            line.transform.parent = Instance.gameObject.transform;
            line.transform.position = new Vector3((from + to) / 2, v3, 0);
            line.transform.localScale = new Vector3(to - from,0.05f,1);
        }
        if (type == LineType.Vertical)
        {
            GameObject line = Instantiate(Instance.linePrefab);
            line.transform.parent = Instance.gameObject.transform;
            line.transform.position = new Vector3(v3, (from + to) / 2, 0);
            line.transform.localScale = new Vector3(0.05f,to - from,1);
        }
    }

    public static void DrawTile(Rect rect)
    {
        GameObject tile = Instantiate(Instance.tilePrefab);
        tile.transform.parent = Instance.TileContainer.transform;
        tile.transform.position = new Vector3(rect.center.x,rect.center.y,0);
        tile.transform.localScale = new Vector3(rect.width, rect.height, 1);

    }

    public static void ClearAllLines()
    {
        while (Instance.transform.childCount != 0)
        {
            DestroyImmediate(Instance.transform.GetChild(0).gameObject);
        }
        
    }

    public static void ClearAllTiles()
    {
        while (Instance.TileContainer.transform.childCount != 0)
        {
            DestroyImmediate(Instance.TileContainer.transform.GetChild(0).gameObject);
        }
    }
}

public enum LineType
{
    Horizontal,
    Vertical
}