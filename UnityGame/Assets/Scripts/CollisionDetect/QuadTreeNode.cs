using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode
{
    public static int DIVISION_THRESHOLD = 4;
    public QuadTreeNode parent = null;
    public List<Enemy> enemyList = new List<Enemy>();
    public QuadTreeNode LT = null;
    public QuadTreeNode LB = null;
    public QuadTreeNode RT = null;
    public QuadTreeNode RB = null;
    public Rect rect;
    public bool isFinal = false;
    /// <summary>
    /// 建树
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static QuadTreeNode BuildTree(Rect rect, List<Enemy> enemyList)
    {
        QuadTreeNode root = new QuadTreeNode(rect,enemyList);
        return root;
    }

    public void DrawRect()
    {
        if (LT != null)
            LT.DrawRect();
        if(LB != null)
            LB.DrawRect();
        if(RT != null)
            RT.DrawRect();
        if(RB != null)
            RB.DrawRect();
        else
        {
            DrawManager.DrawLine(LineType.Horizontal,rect.xMin,rect.xMax,rect.yMin);
            DrawManager.DrawLine(LineType.Horizontal,rect.xMin,rect.xMax,rect.yMax);
            DrawManager.DrawLine(LineType.Vertical,rect.yMin,rect.yMax,rect.xMin);
            DrawManager.DrawLine(LineType.Vertical,rect.yMin,rect.yMax,rect.xMax);
        }
    }

    public List<QuadTreeNode> CrossNode(Rect playerRect)
    {
        if (isFinal == true)
        {
            return new List<QuadTreeNode>(){this};
        }
        List<QuadTreeNode> result = new List<QuadTreeNode>();
        //左上
        if (playerRect.xMin < rect.center.x && playerRect.yMax > rect.center.y)
        {
            if (!LT.isFinal)
            {
                Rect partOfPlayerRect = MethodCollection.GetCrossAreaOfTwoRect(playerRect, rect);
                result = MethodCollection.UnionTwoList(result, LT.CrossNode(partOfPlayerRect));   
            }
            else
            {
                result.Add(LT);
            }
        }

        //左下
        if (playerRect.xMin < rect.center.x && playerRect.yMin < rect.center.y)
        {
            if (!LB.isFinal)
            {
                Rect partOfPlayerRect = MethodCollection.GetCrossAreaOfTwoRect(playerRect, rect);
                //Rect partOfPlayerRect = new Rect(playerRect.xMin, playerRect.yMin, rect.center.x - playerRect.xMin,rect.center.y - playerRect.yMin);
                result = MethodCollection.UnionTwoList(result, LB.CrossNode(partOfPlayerRect));
            }
            else
            {
                result.Add(LB);
            }
        }
        //右上
        if (playerRect.xMax > rect.center.x && playerRect.yMax > rect.center.y)
        {
            if (!RT.isFinal)
            {
                Rect partOfPlayerRect = MethodCollection.GetCrossAreaOfTwoRect(playerRect, rect);
                result = MethodCollection.UnionTwoList(result, RT.CrossNode(partOfPlayerRect));                
            }
            else
            {
                result.Add(RT);
            }
        }
        //右下
        if (playerRect.xMax > rect.center.x && playerRect.yMin < rect.center.y)
        {
            if (!RB.isFinal)
            {
                Rect partOfPlayerRect = MethodCollection.GetCrossAreaOfTwoRect(playerRect, rect);
                result = MethodCollection.UnionTwoList(result, RB.CrossNode(partOfPlayerRect));   
            }
            else
            {
                result.Add(RB);
            }
        }
        return result;
    }
    
    public QuadTreeNode(Rect _rect, List<Enemy> _enemyList,QuadTreeNode _parent = null)
    {
        rect = _rect;
        if (_parent != null)
        {
            parent = _parent;
        }
        //该区域节点大于阈值，需要分裂
        if (_enemyList.Count > DIVISION_THRESHOLD)
        {
            List<Enemy> LTnodes = new List<Enemy>();
            List<Enemy> LBnodes = new List<Enemy>();
            List<Enemy> RTnodes = new List<Enemy>();
            List<Enemy> RBnodes = new List<Enemy>();
            foreach (var enemy in _enemyList)
            {
                if (CheckInArea(QuadTreeArea.LeftTop, enemy.center + new Vector2(-enemy.width/2, enemy.height/2)))
                {
                    LTnodes.Add(enemy);
                }
                if (CheckInArea(QuadTreeArea.LeftBottom, enemy.center+ new Vector2(-enemy.width/2, -enemy.height/2)))
                {
                    LBnodes.Add(enemy);
                }
                if (CheckInArea(QuadTreeArea.RightTop, enemy.center+ new Vector2(enemy.width/2, enemy.height/2)))
                {
                    RTnodes.Add(enemy);
                }
                if (CheckInArea(QuadTreeArea.RightBottm, enemy.center+ new Vector2(enemy.width/2, -enemy.height/2)))
                {
                    RBnodes.Add(enemy);
                }
            }

            LT = new QuadTreeNode(new Rect(rect.x, rect.y + rect.height/2, rect.width/2, rect.height/2), LTnodes,this);
            LB = new QuadTreeNode(new Rect(rect.x, rect.y, rect.width / 2, rect.height / 2), LBnodes, this);
            RT = new QuadTreeNode(new Rect(rect.x + rect.width / 2, rect.y + rect.height/2, rect.width / 2, rect.height / 2), RTnodes, this);
            RB = new QuadTreeNode(new Rect(rect.x + rect.width / 2, rect.y, rect.width / 2, rect.height / 2), RBnodes, this);
            isFinal = false;
        }
        else
        {    //该区域节点小于阈值，不用分裂了
             enemyList = _enemyList;
             isFinal = true;
        }
    }

    public void InsertNode(QuadTreeNode node)
    {
        
    }

    public bool CheckInArea(QuadTreeArea area, Vector2 vec)
    {
        if(area == QuadTreeArea.LeftTop)
            return vec.x < rect.center.x && vec.y > rect.center.y;
        if(area == QuadTreeArea.LeftBottom) 
            return vec.x < rect.center.x && vec.y < rect.center.y;
        if(area == QuadTreeArea.RightTop) 
            return vec.x > rect.center.x && vec.y > rect.center.y;
        if(area == QuadTreeArea.RightBottm) 
            return vec.x > rect.center.x && vec.y < rect.center.y;
        return false;
    }
}

public enum QuadTreeArea
{
    LeftTop,
    LeftBottom,
    RightTop,
    RightBottm
}
