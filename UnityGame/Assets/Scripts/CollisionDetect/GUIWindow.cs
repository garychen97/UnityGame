using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GUIWindow : MonoBehaviour
{
    private string x = "";
    private string y = "";
    private string width = "";
    private string height = "";
    private string amount = "";
    private string textToServer = "";
    private void OnGUI()
    {
        //大 黑
        GUIStyle fontStyle1 = new GUIStyle(); 
        fontStyle1.alignment=TextAnchor.MiddleCenter;
        fontStyle1.fontSize=25;
        fontStyle1.normal.textColor=Color.black;

        //中 白
        GUIStyle fontStyle2 = GUI.skin.textField;
        fontStyle2.alignment=TextAnchor.MiddleCenter;
        fontStyle2.fontSize=20;
        fontStyle2.normal.textColor=Color.white;
        
        //大 白
        GUIStyle fontStyle3 = GUI.skin.button;
        fontStyle2.alignment=TextAnchor.MiddleCenter;
        fontStyle3.fontSize=25;
        fontStyle3.normal.textColor=Color.white;

        if (!GameManager.Instance.ShowGameManagerTool)
        {
            if (GUI.Button(RectManager.Instance.AddRect(true, 1000,200, 100), new GUIContent(GameManager.Instance.ShowGameManagerTool?"Close Tool":"Open Tool"),fontStyle3))
            {
                GameManager.Instance.ShowGameManagerTool = !GameManager.Instance.ShowGameManagerTool;
                RectManager.Instance.ClearCache();
            }
            return;
        }

        GUI.Label(RectManager.Instance.AddRect(true, 11,200, 50),"请输入x",fontStyle1);
        GUI.Label(RectManager.Instance.AddRect(false, 12,200, 50),"请输入y",fontStyle1);
        GUI.Label(RectManager.Instance.AddRect(false, 17,200, 50),"请输入width",fontStyle1);
        GUI.Label(RectManager.Instance.AddRect(false, 18,200, 50),"请输入height",fontStyle1);
        GUI.Label(RectManager.Instance.AddRect(false, 16,200, 50),"请输入数量",fontStyle1);

        x = GUI.TextField(RectManager.Instance.AddRect(true, 13, 200, 50), string.IsNullOrEmpty(x) ? "-10" : x,fontStyle2);
        y = GUI.TextField(RectManager.Instance.AddRect(false, 14, 200, 50), string.IsNullOrEmpty(y) ? "-4" : y,fontStyle2);
        width = GUI.TextField(RectManager.Instance.AddRect(false, 19, 200, 50), string.IsNullOrEmpty(width) ? "20" : width,fontStyle2);
        height = GUI.TextField(RectManager.Instance.AddRect(false, 20, 200, 50), string.IsNullOrEmpty(height) ? "8" : height,fontStyle2);
        amount = GUI.TextField(RectManager.Instance.AddRect(false, 15, 200, 50), string.IsNullOrEmpty(amount) ? "10" : amount,fontStyle2);

        if (GUI.Button(RectManager.Instance.AddRect(true, 1,200, 50), new GUIContent("生成Enemy"),fontStyle3))
        {
            Debug.Log("生成Enemy");
            Rect rect = new Rect(int.Parse(x), int.Parse(y), int.Parse(width), int.Parse(height));
            GameManager.Instance.CreateRandomEnemy(rect,int.Parse(amount));
        }
        if (GUI.Button(RectManager.Instance.AddRect(false, 3,200, 50), new GUIContent("清楚所有Enemy"),fontStyle3))
        {
            Debug.Log("清除Enemy");
            GameManager.Instance.ClearEnemy();;
        }
        if (GUI.Button(RectManager.Instance.AddRect(false, 4,200, 50), new GUIContent(GameManager.Instance.ShowDetectedArea?"隐藏检测区域":"显示检测区域"),fontStyle3))
        {
            if (!GameManager.Instance.ShowDetectedArea)
            {
                GameManager.Instance.ShowDetectedArea = true;
                GameManager.Instance.DrawDetectedTile();
            }
            else
            {
                GameManager.Instance.ShowDetectedArea = false;   
                DrawManager.ClearAllTiles();
            }
        }
        if (GUI.Button(RectManager.Instance.AddRect(false, 5,200, 50), new GUIContent(GameManager.Instance.ShowDetectedEnemy?"隐藏检测物体":"显示检测物体"),fontStyle3))
        {
            if (!GameManager.Instance.ShowDetectedEnemy)
            {
                GameManager.Instance.ShowDetectedEnemy = true;
                GameManager.Instance.DrawDetectedEnemy();
            }
            else
            {
                GameManager.Instance.ShowDetectedEnemy = false;   
                GameManager.Instance.RevertDetectedEnemy();
            }
        }
        if (GUI.Button(RectManager.Instance.AddRect(false, 6,200, 50), 
                new GUIContent(NetworkManager.Instance.ClientSocket == null || !NetworkManager.Instance.ClientSocket.Connected?"连接服务器":"断开服务器"),fontStyle3))
        {
            if (NetworkManager.Instance.ClientSocket == null || !NetworkManager.Instance.ClientSocket.Connected)
            {
                NetworkManager.Instance.ConnetToServer();   
            }
            else
            {
                NetworkManager.Instance.DisConnetToServer();  
            }
        }
        
        textToServer = GUI.TextField(RectManager.Instance.AddRect(true, 1002, 500, 50), textToServer,fontStyle2);
        if (GUI.Button(RectManager.Instance.AddRect(false, 1003,200, 50), "发送消息",fontStyle3))
        {
            NetworkManager.Instance.SendTextToServer(textToServer);
        }

            if (GUI.Button(RectManager.Instance.AddRect(true, 1001,200, 100), new GUIContent(GameManager.Instance.ShowGameManagerTool?"Close Tool":"Open Tool"),fontStyle3))
        {
            GameManager.Instance.ShowGameManagerTool = !GameManager.Instance.ShowGameManagerTool;
            RectManager.Instance.ClearCache();
        }
    }
}

public class RectManager
{
    private static RectManager instance;

    public static RectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RectManager();
            }
            return instance;
        }
    }

    public RectManager()
    {
        currentRow = 0;
        currentY = 0;
        rowList = new List<RowInfo>();
        Cache = new Dictionary<int, Rect>();
    }

    public int currentRow;
    public float currentY;
    public List<RowInfo> rowList;
    public Dictionary<int, Rect> Cache;

    /// <summary>
    /// 在_rowId对应行添加一个Rect对象
    /// </summary>
    /// <param name="_rowId">_rowId从1开始</param>
    /// <param name="_width"></param>
    /// <param name="_height"></param>
    /// <returns></returns>
    public Rect AddRect(bool newRow,int _rectId, float _width, float _height)
    {
        if (newRow)
        {
            currentRow++;
            RowInfo rowInfo = new RowInfo(currentY);
            rowList.Add(rowInfo);
        }
        if (Cache.ContainsKey(_rectId)) return Cache[_rectId];

        Rect rect = rowList[currentRow - 1].AddRectInRow(_width, _height);
        Cache[_rectId] = rect;
        return rect;
    }

    public void ClearCache()
    {
        currentRow = 0;
        currentY = 0;
        rowList = new List<RowInfo>();
        Cache = new Dictionary<int, Rect>();
    }
}

/// <summary>
/// 绘制一行的信息
/// </summary>
public class RowInfo
{
    public float rowStartY;//改行开始的Y值
    public float height;//该行当前最大高度
    public float currentTailX;//该行当前尾部X值

    public RowInfo(float _rowStartY)
    {
        rowStartY = _rowStartY;
        height = 0;
        currentTailX = 0;
    }
    
    /// <summary>
    /// 在改行添加一个元素
    /// </summary>
    public Rect AddRectInRow(float _width, float _height)
    {
        if (_height > height)
        {
            RectManager.Instance.currentY += (_height - height);
            height = _height;
        }
        Rect rect = new Rect(currentTailX, rowStartY, _width, _height);
        currentTailX += _width;
        return rect;
    }
    
}