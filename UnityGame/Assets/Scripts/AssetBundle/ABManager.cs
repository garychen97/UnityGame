using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1、AB包相关API
/// 2、单例模式
/// 3、委托——lambda表达式
/// 4、协程
/// 5、字典
/// </summary>
public class ABManager : SingletonMonoBehaviour<ABManager>
{
    //目的：
    //让外部更方便地进行资源加载

    //主包
    private AssetBundle mainAB = null;
    //依赖包用的配置文件
    private AssetBundleManifest manifest = null;
    
    //AB包不能重复加载 重复加载会报错
    //所以需要用字典存储加载过的AB包
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包存放路径 方便修改
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// 主包名 方便修改
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName)
    {
        //加载主包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab = null;
        //加载主包中关键配置信息 获取依赖包
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //判断包是否加载过
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i],ab);
            }
        }
        //加载主AB包
        //如果没有加载过，再加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName,ab);
        }
    }

    /// <summary>
    /// 同步加载指定AB包中的指定资源(重载一：不指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public Object LoadRes(string abName, string resName)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //如果是GameObject直接实例化后再返回
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate((obj));
        else
            return obj;
    }
    
    /// <summary>
    /// 同步加载指定AB包中的指定资源(重载二：根据type指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public Object LoadRes(string abName, string resName,System.Type type)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //如果是GameObject直接实例化后再返回
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return Instantiate((obj));
        else
            return obj;
    }
    
    /// <summary>
    /// 同步加载指定AB包中的指定资源(重载二：泛型指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadRes<T>(string abName, string resName) where  T : Object
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //如果是GameObject直接实例化后再返回
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate((obj));
        else
            return obj;
    }
    
    
    //异步加载指定AB包中的指定资源的方法
    //这里的异步加载 AB包并没有使用异步加载
    //只是从AB包中加载资源时 使用异步加载
    
    /// <summary>
    /// 异步加载外部接口(重载一：不指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        StartCoroutine(RealLoadResAsync(abName, resName, callBack));
    }
    /// <summary>
    /// 异步加载(重载一：不指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    private IEnumerator RealLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //如果是GameObject直接实例化后再返回
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        if (abr.asset is GameObject)
            callBack(Instantiate((abr.asset)));
        else
            callBack(abr.asset);
    }
    
    /// <summary>
    /// 异步加载外部接口(重载二：根据type指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public void LoadResAsync(string abName, string resName,System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(RealLoadResAsync(abName, resName, type, callBack));
    }
    /// <summary>
    /// 异步加载(重载二：根据type指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    private IEnumerator RealLoadResAsync(string abName, string resName,System.Type type, UnityAction<Object> callBack)
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //如果是GameObject直接实例化后再返回
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        yield return abr;
        if (abr.asset is GameObject)
            callBack(Instantiate((abr.asset)));
        else
            callBack(abr.asset);
    }
    
    
    /// <summary>
    /// 异步加载外部接口(重载三：根据泛型指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(RealLoadResAsync<T>(abName, resName, callBack));
    }
    /// <summary>
    /// 异步加载(重载三：根据泛型指定类型)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    private IEnumerator RealLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //如果是GameObject直接实例化后再返回
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        if (abr.asset is GameObject)
            callBack(Instantiate((abr.asset) as T));
        else
            callBack(abr.asset as T);
    }
    
    //单个包卸载
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload((false));
            abDic.Remove(abName);
        }
    }
    
    
    //所有包卸载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
