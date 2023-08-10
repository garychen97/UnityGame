using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 第一次课 类的使用

//自定义类
public class Test
{
    public void Speak(string str)
    {
        Debug.Log("test1" + str);
    }
}

namespace GaryXunChen
{
    public class Test2
    {
        public void Speak(string str)
        {
            Debug.Log("test2" + str);
        }
    }
}

#endregion

#region 第二次课 枚举

/// <summary>
/// 自定义测试枚举
/// </summary>
public enum E_MyEnum
{
    Idle,
    Move,
    Atk,
}


#endregion

#region 第三次课 数组 List 字典

public class Lesson3
{
    public int[] array = new int[5] { 1, 2, 3, 4, 5 };

    public List<int> list = new List<int>();

    public Dictionary<int, string> dic = new Dictionary<int, string>();
}

#endregion



public class LuaCallCSharp : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
