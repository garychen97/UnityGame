using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gary4_CallVariable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.Instance.Init();
        
        LuaMgr.Instance.DoLuaFile("Main");
        
        //使用lua解析器luaenv中的Global属性
        int i = LuaMgr.Instance.Global.Get<int>("testNumber");
        Debug.Log("testNumber:" + i);
        i = 10;
        //值拷贝 不会影响原来Lua中的值
        int i2 = LuaMgr.Instance.Global.Get<int>("testNumber");
        Debug.Log("testNumber2:" + i2);
        
        //要改lua变量值
        LuaMgr.Instance.Global.Set("testNumber",55);
        int i3 = LuaMgr.Instance.Global.Get<int>("testNumber");
        Debug.Log("testNumber3:" + i3);
        
        
        
        bool b = LuaMgr.Instance.Global.Get<bool>("testBool");
        Debug.Log("testBool:" + b);
        
        float f = LuaMgr.Instance.Global.Get<float>("testFloat");
        Debug.Log("testFloat:" + f);
        
        double d = LuaMgr.Instance.Global.Get<double>("testFloat");
        Debug.Log("testFloat_double:" + d);
        
        string s = LuaMgr.Instance.Global.Get<string>("testString");
        Debug.Log("testString:" + s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
