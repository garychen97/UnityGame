using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;

//接口中是不允许有成员变量的
//我们用属性来接收
//接口也需要加CSharpCallLua特性
//接口和类规则一样，其中的属性多了少了 不影响结果 无非是忽略他们
//嵌套几乎和类一样 无非是要遵循接口的规则 加特性
[CSharpCallLua]
public interface ICSharpCallInterface
{
    int testInt
    {
        get;
        set;
    }

    bool testBool
    {
        get;
        set;
    }
    
    float testFloat
    {
        get;
        set;
    }
    
    string testString
    {
        get;
        set;
    }
    
    Action testFun
    {
        get;
        set;
    }
}

public class Gary8_CallInterface : MonoBehaviour
{

    
    void Start()
    {
        LuaMgr.Instance.Init();
        LuaMgr.Instance.DoLuaFile("Main");

        ICSharpCallInterface obj = LuaMgr.Instance.Global.Get<ICSharpCallInterface>("testClass2");
        Debug.Log(obj.testInt);
        Debug.Log(obj.testBool);
        Debug.Log(obj.testFloat);
        Debug.Log(obj.testString);
        obj.testFun();

        //接口拷贝！是引用拷贝 改了值 Lua表中的值也变了
        obj.testInt = 100000;
        ICSharpCallInterface obj2 = LuaMgr.Instance.Global.Get<ICSharpCallInterface>("testClass2");
        Debug.Log(obj2.testInt);
    }

}
