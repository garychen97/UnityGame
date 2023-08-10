using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

/// <summary>
/// Lua管理器
/// 提供 lua解析器
/// 保证解析器的唯一性
/// </summary>
public class LuaMgr : BaseManager<LuaMgr>
{
    //执行Lua语言的函数
    
    //释放垃圾
    
    //销毁
    
    //重定向

    private LuaEnv luaEnv;

    /// <summary>
    /// 得到Lua中的_G
    /// </summary>
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }
    
    public void Init()
    {
        if (luaEnv != null)
            return;
        luaEnv = new LuaEnv();
        //加载lua脚本 重定向
        luaEnv.AddLoader(MyCustomLoader);
        luaEnv.AddLoader(MyCustomABLoader);
    }


    /// <summary>
    /// 传入lua文件名 执行lua脚本
    /// </summary>
    /// <param name="fileName"></param>
    public void DoLuaFile(string fileName)
    {
        string str = "require('" + fileName + "')";
        Debug.Log(str);
        DoString(str);
    }

    /// <summary>
    /// 执行lua语言
    /// </summary>
    /// <param name="str"></param>
    public void DoString(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("解析器未初始化");
            return;
        }
        luaEnv.DoString(str);
    }

    /// <summary>
    /// 释放lua垃圾
    /// </summary>
    public void Tick()
    {
        luaEnv.Tick();
    }

    /// <summary>
    /// 销毁解析器
    /// </summary>
    public void Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
    }
    
    //我们自定义去找我们Lua文件的路径
    //通过AddLoader方法注册后，require lua脚本时 可以自己设计路径重定向去找我们的lua脚本
    //自动执行
    private byte[] MyCustomLoader(ref string filePath)
    {
        //传入的参数时 require执行的lua脚本文件名
        //拼接一个Lua文件所在路径
        string path = Application.dataPath + "/Lua/" + filePath + ".lua.txt";

        //由路径 就去加载文件
        //File知识点 C#提供的文件读写的类
        //判断文件是否存在
        if (File.Exists(path))
        {
            Debug.Log("MyCustomLoader重定向成功！文件名为" + filePath);
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

    
    //Lua脚本会放在AB包中
    //最终我们会通过加载AB包再加载其中的Lua脚本资源 来执行它
    //AB包中如果要加载文本 后缀还是会有一定显示 .lua不能被识别
    //打包时 还是要把lua文件后缀改成.txt
    
    /// <summary>
    /// 重定向加载AB包中的Lua脚本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[] MyCustomABLoader(ref string filePath)
    {
        TextAsset lua = ABManager.Instance.LoadRes<TextAsset>("lua", filePath + ".lua");
        if (lua != null)
        {
            Debug.Log("MyCustomABLoader重定向成功！文件名为" + filePath);
            return lua.bytes;   
        }
        else
            Debug.Log("MyCustomABLoader重定向失败，文件名为" + filePath);
        return null;
    }
}
