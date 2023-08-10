using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lua没有办法直接访问C# 一定是先从C#调用Lua脚本后
/// 才把核心逻辑 交给Lua来编写
/// </summary>
public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.Instance.Init();
        LuaMgr.Instance.DoLuaFile("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
