using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用xLua命名空间
using XLua;

public class Gary1_LuaEnv : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Lua解析器 能够让我们在Unity里执行Lua
        //一般情况下，保持它的唯一性
        LuaEnv env = new LuaEnv();
        
        //执行Lua语言
        //C#中的lua字符串用双引号的话前面要带转义字符，一般用单引号好
        env.DoString("print('你好世界')");
        
        //执行一个Lua脚本 Lua知识点：多脚本执行 require
        //默认寻找脚本的路径 是在Resources下
        //因为再Resources下，估计时用Resources.Load去加载的Lua脚本
        //所以Lua脚本后缀需要加一个txt
        //Main.lua.txt
        env.DoString("require('Main')");
        
        //重载一，第二个参数是第一个参数lua语句执行错误时打印的内容
        env.DoString("print('你好世界')","Gary_LuaEnv");
        
        //帮助我们清除Lua中我们没有手动释放的对象 垃圾回收
        //帧更新中定时执行 或者切场景时执行
        env.Tick();
        
        //销毁Lua解析器，一般很少用到 因为一般只有一个lua解析器
        env.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
