using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABTest : MonoBehaviour
{
    public Image image;

    public AssetBundle ab;

    public bool testABManager = true;
    // Start is called before the first frame update
    void Start()
    {
        if (testABManager)
        {
            GameObject obj11 = ABManager.Instance.LoadRes("model", "Cube") as GameObject;
            obj11.transform.position = Vector3.up;
            
            GameObject obj12 = ABManager.Instance.LoadRes("model", "Cube", typeof(GameObject)) as GameObject;
            obj12.transform.position = Vector3.down;
            
            GameObject obj13 = ABManager.Instance.LoadRes<GameObject>("model", "Cube");
            obj13.transform.position = Vector3.right;
            
            ABManager.Instance.LoadResAsync("model", "Cube",(obj) =>
            {
                (obj as GameObject).transform.position = Vector3.left;
            });
            
            ABManager.Instance.LoadResAsync("model", "Cube",typeof(GameObject),(obj) =>
            {
                (obj as GameObject).transform.position = Vector3.forward;
            });
            
            ABManager.Instance.LoadResAsync<GameObject>("model", "Cube",(obj) =>
            {
                obj.transform.position = Vector3.back;
            });


            return;
        }
        //第一步  加载 AB包
        ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        
        
        //关于AB包的依赖——一个资源身上用到了别的AB包中的资源 这个时候 如果只加载自己的AB包
        //通过它创建对象 会出现资源丢失的情况
        //这种时候 需要把依赖包 一起加载了 才能正常
        
        //或者利用主包 获取依赖信息
        
        //加载主包
        AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //加载主包中的固定文件
        AssetBundleManifest abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //从固定文件中 得到依赖信息
        string[] strs = abManifest.GetAllDependencies("model");
        //得到了 依赖包的名字
        for (int i = 0; i < strs.Length; i++)
        {
            Debug.Log(strs[i]);
        }
        
        
        
        //第二步 加载 AB包中的资源
        
        //只用名字加载会出现同名不同类型资源 导致分不清
        //建议用泛型加载或者用Type指定类型
        GameObject obj1 = ab.LoadAsset<GameObject>("Cube");
        GameObject obj2 = ab.LoadAsset("Capsule",typeof(GameObject)) as GameObject;
        Instantiate(obj1);
        //Instantiate(obj2);
        


        //异步加载-->协程
        //StartCoroutine(LoadABRes("head", "bear"));

    }

    IEnumerator LoadABRes(string ABName, string resName)
    {
        //第一步  加载 AB包
        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
        yield return abcr;
        AssetBundleRequest abq = abcr.assetBundle.LoadAssetAsync(resName, typeof(Sprite));
        //第二步 加载 AB包中的资源
        yield return abq;
        image.sprite = abq.asset as Sprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        //AB包不能重复加载 否则会报错
        //AB包通过AssetBundle.UnloadAllAssetBundles卸载所有已经加载的AB包
        //参数为true时会把通过AB包加载的资源一同卸载
        //参数为false只卸载AB包，已加载的资源不管
        if (Input.GetMouseButtonDown(0))
        {
            AssetBundle.UnloadAllAssetBundles(true);
        }
        //单独卸载 参数布尔值和UnloadAllAssetBundles方法参数含义一致
        //ab.Unload(false);
    }
}
