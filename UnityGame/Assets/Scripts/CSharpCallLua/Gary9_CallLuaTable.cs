using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Gary9_CallLuaTable : MonoBehaviour
{

    void Start()
    {
        LuaMgr.Instance.Init();
        LuaMgr.Instance.DoLuaFile("Main");

        //不建议使用LuaTable和LuaFunction 效率低 产生垃圾
        //是引用对象
        LuaTable table = LuaMgr.Instance.Global.Get<LuaTable>("testClass");
        Debug.Log(table.Get<int>("testInt"));
        Debug.Log(table.Get<int>("testBool"));
        Debug.Log(table.Get<int>("testFloat"));
        Debug.Log(table.Get<int>("testString"));
        table.Get<LuaFunction>("testFun").Call();
        
        //改 引用
        table.Set("testInt",10000);
        //值会一起改
        LuaTable table2 = LuaMgr.Instance.Global.Get<LuaTable>("testClass");
        Debug.Log(table2.Get<int>("testInt"));
        
        
        //这种方法 如果table不用了的话
        table.Dispose();
        table2.Dispose();
    }
    
}
