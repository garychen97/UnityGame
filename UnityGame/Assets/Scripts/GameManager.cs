using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;


public class GameManager : SingletonMonoBehaviour<GameManager>
{
    /// <summary>
    /// 是否显示被检测区域
    /// </summary>
    public bool ShowDetectedArea = false;
    /// <summary>
    /// 是否显示被检测物体
    /// </summary>
    public bool ShowDetectedEnemy = false;

    /// <summary>
    /// 是否打开工具
    /// </summary>
    public bool ShowGameManagerTool = false;
    
    private List<Enemy> enemyList = new List<Enemy>();
    public GameObject enemyPrefab;
    public QuadTreeNode root;
    public Rect playerRect = new Rect(-0.5f,-0.5f,1,1);
    /*
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
                if (go == null)
                {
                    Debug.LogError("未找到名为GameManager的物体");
                }
                instance = go.GetComponent<GameManager>();
                if (instance == null)
                {
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
*/
    /// <summary>
    /// 生成随机敌人
    /// </summary>
    public void CreateRandomEnemy(Rect range, int amount)
    {
        ClearEnemy();
        enemyList = new List<Enemy>();
        foreach (var vec2 in MethodCollection.CreateRandom2DPosList(range,amount))
        {
            Enemy enemy = EnemyPool.Instance.InstantiateEnemy();
            enemy.SetEnemyPos(vec2);
            enemyList.Add(enemy);
        }
        
        root = QuadTreeNode.BuildTree(range, enemyList);
        root.DrawRect();
    }
    
    /// <summary>
    /// 清除所有敌人
    /// </summary>
    public void ClearEnemy()
    {
        foreach(Enemy enemy in enemyList)
        {
            enemy.DestorySelf();
        }
        enemyList.Clear();
        root = null;
        DrawManager.ClearAllLines();
        DrawManager.ClearAllTiles();
    }

    /// <summary>
    /// 获取需要检测的区域块
    /// </summary>
    /// <returns></returns>
    public List<QuadTreeNode> GetNeedDetectArea(Rect playerRect)
    {
        if (root != null)
        {
            return root.CrossNode(playerRect);   
        }

        return new List<QuadTreeNode>();
    }

    /// <summary>
    /// 绘制检测区域
    /// </summary>
    public void DrawDetectedTile()
    {
        DrawManager.ClearAllTiles();
        foreach (var node in GetNeedDetectArea(playerRect))
        {
            DrawManager.DrawTile(node.rect);
        }
    }
    
    /// <summary>
    /// 展示检测区域的物体颜色
    /// </summary>
    public void DrawDetectedEnemy()
    {
        RevertDetectedEnemy();
        foreach (var node in GetNeedDetectArea(playerRect))
        {
            foreach (var enemy in node.enemyList)
            {
                enemy.ChangeColor();
            }
        }   
    }
    
    /// <summary>
    /// 还原检测区域的物体颜色
    /// </summary>
    public void RevertDetectedEnemy()
    {
        foreach (var enemy in enemyList)
        {
            enemy.RevertColor();
        }
    }
}

