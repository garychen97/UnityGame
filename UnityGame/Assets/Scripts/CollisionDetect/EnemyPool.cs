using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人对象池
/// </summary>
public class EnemyPool:MonoBehaviour
{
    public List<Enemy> _freeEnemyList = new List<Enemy>();
    public int _freeEnemyCount = 0;
    public int freeEnemyListcapacity = 20;

    #region EnemyPool单例

    private static EnemyPool instance;
    public static EnemyPool Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("EnemyContainer");
                if (go == null)
                {
                    Debug.LogError("未找到名为EnemyContainer的物体");
                }
                instance = go.GetComponent<EnemyPool>();
                if (instance == null)
                {
                    instance = go.AddComponent<EnemyPool>();
                }
            }
            return instance;
        }
    }

    #endregion

    /// <summary>
    /// “生成”一个Enemy对象，实际可能是池子里拿的，若池子里没对象，则生成一个
    /// </summary>
    /// <returns></returns>
    public Enemy InstantiateEnemy()
    {
        Enemy enemy;
        if (Instance._freeEnemyCount > 0)
        {
            _freeEnemyCount--;
            enemy = _freeEnemyList[0];
            _freeEnemyList.Remove(_freeEnemyList[0]);
        }
        else
        {
            enemy = new Enemy();
        }
        enemy.ShowThis();
        return enemy;
    }

    /// <summary>
    /// “销毁”一个Enemy对象,实际是先回收到池子里
    /// </summary>
    /// <param name="enemy"></param>
    public void DestroyEnemy(Enemy enemy)
    {
        enemy.HideThis();
        if (_freeEnemyCount < freeEnemyListcapacity)
        {
            _freeEnemyCount++;
            _freeEnemyList.Add(enemy);   
        }
        else
        {
            DestroyImmediate(enemy.EnemyGo());
        }
    }
    
}

