using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using Object = UnityEngine.Object;

//无参数无返回值的委托
public delegate void CustomCall();

//有参数有返回值的委托
//该特性在XLua命名空间中的
//每次加标签 都需要点XLua/Generate Code来生成代码
[CSharpCallLua]
public delegate int CustomCall2(int a);

[CSharpCallLua]
public delegate int CustomCall3(int a, out int b, out bool c, out string d, out int e);

[CSharpCallLua]
public delegate int CustomCall4(int a, ref int b, ref bool c, ref string d, ref int e);

[CSharpCallLua]
public delegate void CustomCall5(string a, params int[] args);//变长参数的类型是根据实际清空来定的 不确定的话就是object[]

public class Gary5_CallFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.Instance.Init();
        
        LuaMgr.Instance.DoLuaFile("Main");
        
        //无参无返回值的函数获取
        //自定义委托
        CustomCall call = LuaMgr.Instance.Global.Get<CustomCall>("testFun");
        call();
        //Unity自带委托
        UnityAction ua = LuaMgr.Instance.Global.Get<UnityAction>("testFun");
        ua();
        //C#提供的委托
        Action ac = LuaMgr.Instance.Global.Get<Action>("testFun");
        ac();
        //Xlua提供的一种 获取函数的方式 少用
        LuaFunction lf = LuaMgr.Instance.Global.Get<LuaFunction>("testFun");
        lf.Call();
         
        //有参数有返回值
        CustomCall2 call2 = LuaMgr.Instance.Global.Get<CustomCall2>("testFun2");
        Debug.Log("有参有返回：" + call2(2));
        //C#自带的泛型委托 方便我们使用
        Func<int,int> sFunc = LuaMgr.Instance.Global.Get<Func<int,int>>("testFun2");
        Debug.Log("有参有返回：" + sFunc(2));
        //XLua提供的
        LuaFunction lf2 = LuaMgr.Instance.Global.Get<LuaFunction>("testFun2");
        Debug.Log("有参有返回：" + lf2.Call(30)[0]);
        
        //多返回值
        //使用 out 和 ref 来接收
        CustomCall3 call3 = LuaMgr.Instance.Global.Get<CustomCall3>("testFun3");
        int b;
        bool c;
        string d;
        int e;
        Debug.Log("第一个返回值" + call3(100,out b,out c,out d,out e));
        Debug.Log(b + "_" + c + "_" + d + "_" + e);
        
        CustomCall4 call4 = LuaMgr.Instance.Global.Get<CustomCall4>("testFun3");
        int b1 = 0;
        bool c1 = false;
        string d1 = "";
        int e1 = 0;
        Debug.Log("第一个返回值" + call4(200, ref b1,ref c1,ref d1,ref e1));
        Debug.Log(b1 + "_" + c1 + "_" + d1 + "_" + e1);
        //Xlua
        LuaFunction lf3 = LuaMgr.Instance.Global.Get<LuaFunction>("testFun3");
        object[] objs = lf3.Call(1000);
        for (int i = 0; i < objs.Length; ++i)
        {
            Debug.Log("第" + i + "个返回值是：" + objs[i]);
        }
        
        //变长参数
        CustomCall5 call5 = LuaMgr.Instance.Global.Get<CustomCall5>("testFun4");
        call5("123", 1, 2, 3, 4, 5, 6, 9, 99);

        LuaFunction lf4 = LuaMgr.Instance.Global.Get<LuaFunction>("testFun4");
        lf4.Call("456", 6, 7, 8, 9);
    }


}
