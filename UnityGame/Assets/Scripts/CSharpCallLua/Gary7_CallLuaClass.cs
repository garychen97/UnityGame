using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLuaClass
{
    //在这个类去声明成员变量
    //名字一定要和Lua那边的一样
    //公共 私有和保护 没办法赋值
    public int testInt;
    public bool testBool;
    public float testFloat;
    public string testString;
    public Action testFun;
    public int i;
    public CallLuaInClass testInClass;

    //这个自定义中的变量 可以更多 也可以更少
    //如果变量比lua中的少 就会忽略它
    //如果变量比lua中的多 不会赋值 也不会忽略
}

public class CallLuaInClass
{
    public int testInInt;
}

public class Gary7_CallLuaClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.Instance.Init();
        LuaMgr.Instance.DoLuaFile("Main");

        CallLuaClass obj = LuaMgr.Instance.Global.Get<CallLuaClass>("testClass");
        Debug.Log(obj.testInt);
        Debug.Log(obj.testBool);
        Debug.Log(obj.testFloat);
        Debug.Log(obj.testString);
        Debug.Log(obj.i);
        Debug.Log("嵌套：" + obj.testInClass.testInInt);
        obj.testFun();

        //值拷贝 浅拷贝 不会改变Lua表里的内容
        obj.testInt = 100;
        CallLuaClass obj2 = LuaMgr.Instance.Global.Get<CallLuaClass>("testClass");
        Debug.Log(obj2.testInt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
