using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 PlayerPrefs是什么
        //是Unity提供的可以用于存储读取玩家数据的公共类
        #endregion
        
        #region 知识点二 存储相关
        //PlayerPrefs的数据存储 类似于键值对存储 一个键对应一个值
        //提供了存储三种数据的方法 int float string
        //键：string类型
        //值：int、float、string 对应三种API
        PlayerPrefs.SetInt("myAge",25);
        PlayerPrefs.SetFloat("myHeight", 177.5f);
        PlayerPrefs.SetString("myName","garyxunchen");
        
        //直接调用set相关方法 只会把数据存到内存里
        //当游戏结束时 Unity会自动把数据存到硬盘中
        //如果游戏不是正常结束的 而是崩溃 数据是不会存到硬盘中的
        
        //只要调用该方法 就会马上存储到硬盘中
        PlayerPrefs.Save();
        
        //PlayerPrefs是有局限性的 它只能存三种类型的数据
        //如果你想要存储别的类型的数据 只能降低精度 或者上升精度来进行存储

        bool sex = true;
        PlayerPrefs.SetInt("sex", sex ? 1 : 0);
        
        //如果不同类型用同一键名进行存储 会进行覆盖
        PlayerPrefs.SetFloat("myAge", 25.2f);
        #endregion

        #region 知识点三 读取相关

        //注意 运行时 只要你Set了对应键值对
        //即使你没有马上存储Save在本地
        //也能读出信息

        //int
        //这里myAge已经被float值覆盖 这里取出来是默认值0
        int age = PlayerPrefs.GetInt("myAge");
        print(age);
        //如果找不到myAge的值 就会返回函数的第二个参数 默认值
        age = PlayerPrefs.GetInt("myAge", 100);
        print(age);
        
        //float
        float height = PlayerPrefs.GetFloat("myHeight", 1000f);
        print(height);

        //string
        string name = PlayerPrefs.GetString("myName");
        print(name);

        //第二个参数 默认值 对于我们的作用
        //就是 在得到没有的数据的时候 就可以用它来进行基础数据的初始化
        
        //判断数据是否存在
        //主要作用用来 避免设置相同键
        if (PlayerPrefs.HasKey("myName"))
        {
            print("存在myName对应的键值对数据");
        }
        #endregion

        #region 知识点四 删除数据

        //删除指定键值对
        PlayerPrefs.DeleteKey("myAge");
        
        //删除所有存储的数据
        PlayerPrefs.DeleteAll();

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //总结
    //Set
    //Get
    //HasKey
    //Delete
}
