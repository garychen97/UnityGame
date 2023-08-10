using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MethodCollection
{
    /// <summary>
    /// 随机生成2维的位置列表
    /// </summary>
    /// <param name="range">生成范围</param>
    /// <param name="density">生成个数</param>
    public static List<Vector2> CreateRandom2DPosList(Rect range, int count)
    {
        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(range.xMin, range.xMax);
            float y = Random.Range(range.yMin, range.yMax);
            Vector2 pos = new Vector2(x, y);
            list.Add(pos);
            Debug.Log(String.Format("添加了一个位置({0},{1})",x,y));
        }
        return list;
    }

    public static List<QuadTreeNode> UnionTwoList(List<QuadTreeNode> l1, List<QuadTreeNode> l2)
    {
        foreach (var node in l2)
        {
            l1.Add(node);
        }
        return l1;
    }


    public static Rect GetCrossAreaOfTwoRect(Rect rect1, Rect rect2)
    {
        if (rect1.xMin >= rect2.xMax) return new Rect(0, 0, 0, 0);
        if (rect1.xMax <= rect2.xMin) return new Rect(0, 0, 0, 0);
        if (rect1.yMin >= rect2.yMax) return new Rect(0, 0, 0, 0);
        if (rect1.yMax <= rect2.yMin) return new Rect(0, 0, 0, 0);
        float x_min = rect1.xMin > rect2.xMin ? rect1.xMin : rect2.xMin;
        float x_max = rect1.xMax < rect2.xMax ? rect1.xMax : rect2.xMax;
        float y_min = rect1.yMin > rect2.yMin ? rect1.yMin : rect2.yMin;
        float y_max = rect1.yMax < rect2.yMax ? rect1.yMax : rect2.yMax;
        return new Rect(x_min, y_min, x_max - x_min, y_max - y_min);
    }
}
