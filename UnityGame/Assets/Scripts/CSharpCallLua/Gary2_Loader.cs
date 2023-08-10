using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using XLua;
using File = System.IO.File;

public class Gary2_Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaEnv env = new LuaEnv();
        
        //Xlua提供的一个路径重定向的方法
        //允许我们自定义加载Lua文件的规则
        //当我们执行Lua语言 require时 相当于执行一个lua脚本
        //他就会执行 我们自定义传入的这个函数
        env.AddLoader(MyCustomLoader);

        //最终我们其实会去AB包中加载 lua文件
        
        env.DoString("require('Main')");
    }

    //我们自定义去找我们Lua文件的路径
    //通过AddLoader方法注册后，require lua脚本时 可以自己设计路径重定向去找我们的lua脚本
    //自动执行
    private byte[] MyCustomLoader(ref string filePath)
    {
        //传入的参数时 require执行的lua脚本文件名
        //拼接一个Lua文件所在路径
        string path = Application.dataPath + "/Lua/" + filePath + ".lua";
        Debug.Log(path);
        
        //由路径 就去加载文件
        //File知识点 C#提供的文件读写的类
        //判断文件是否存在
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("MyCustomLoader重定向失败，文件名为" + filePath);
        }
        Debug.Log(filePath);
        //通过函数中的逻辑 去加载Lua 文件
        return null;
    }
}
