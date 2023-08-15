using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using Object = UnityEngine.Object;


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

#region 第四次课 拓展方法

//想要在Lua中使用拓展方法，一定要在工具类前面加上特性
//建议 在Lua中要使用的类 都加上该特性 可以提升性能
//如果不加该特性 除了拓展方法对应得类 其他类得使用 都不会报错
//但是lua是通过反射得机制去调用C#得类 效率较低
[LuaCallCSharp]
public static class Tools
{
    public static void Move(this Lesson4 obj)
    {
        Debug.Log(obj.name + "移动");
    }
}

public class Lesson4
{
    public string name = "陈洵";

    public void Speak(string str)
    {
        Debug.Log(str);
    }

    public static void Eat()
    {
        Debug.Log("吃东西");
    }
}

#endregion

#region 第五次课 ref和out

public class Lesson5
{
    public int RefFun(int a, ref int b, ref int c, int d)
    {
        b = a + d;
        c = a - d;
        return 100;
    }

    public int OutFun(int a, out int b, out int c, int d)
    {
        b = a;
        c = d;
        return 200;
    }

    public int RefOutFun(int a, out int b, ref int c)
    {
        b = a * 10;
        c = a * 20;
        return 300;
    }
    
}

#endregion

#region 第六次课 函数重载

public class Lesson6
{
    public int Calc()
    {
        return 100;
    }

    public int Calc(int a, int b)
    {
        return a + b;
    }

    public int Calc(int a)
    {
        return a;
    }

    public float Calc(float a)
    {
        return a;
    }
    
}

#endregion

#region 第七次课 委托和事件

public class Lesson7
{
    //申明委托和事件
    public UnityAction del;

    public event UnityAction eventAction;

    public void DoEvent()
    {
        if (eventAction != null)
            eventAction();
    }

    public void ClearEvent()
    {
        eventAction = null;
    }
}

#endregion

#region 第八次课 二维数组遍历

public class Lesson8
{
    public int[,] array = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

    public Lesson8()
    {
        int length = array.GetLength(1);
        
    }
}

#endregion

#region 第九次课 判空
    
//为object拓展一个方法
[LuaCallCSharp]
public static class Lesson9
{
    //拓展一个为object判空的方法 主要给lua使用 lua没法用null和nil比较
    public static bool IsNull(this Object obj)
    {
        return obj == null;
    }
}

#endregion

#region 第十次课 系统类型加特性

//解决系统类或者第三方库代码无法加特性的问题，当然也可以把所有特性汇总在此，静态类中，要重新生成代码
public static class Lesson10
{
    [CSharpCallLua]
    public static List<Type> csharpCallLuaList = new List<Type>()
    {
        typeof(UnityAction<float>),
        typeof(CustomCall)
    };

    [LuaCallCSharp] public static List<Type> luaCallCsharpList = new List<Type>()
    {
        typeof(GameObject),
        typeof(Rigidbody)
    };
}


#endregion

#region 第十二次课 调用泛型方法

public class Lesson12
{
    public interface ITest
    {
        
    }
    public class TestFather
    {
        
    }

    public class TestChild : TestFather,ITest
    {
        
    }
    public void TestFun1<T>(T a, T b) where T:TestFather
    {
        Debug.Log("有参数 有约束的泛型方法");
    }
    
    public void TestFun2<T>(T a)
    {
        Debug.Log("有参数 没有约束的泛型方法");
    }
    
    public void TestFun3<T>() where T:TestFather
    {
        Debug.Log("没有参数 有约束的泛型方法");
    }
    
    public void TestFun4<T>() where T:ITest
    {
        Debug.Log("有约束 有参数 但约束不是类 的泛型方法");
    }
    
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
