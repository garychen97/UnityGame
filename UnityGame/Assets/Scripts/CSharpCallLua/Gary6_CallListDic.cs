using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gary6_CallListDic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.Instance.Init();
        LuaMgr.Instance.DoLuaFile("Main");
        
        //同一类型List
        List<int> list = LuaMgr.Instance.Global.Get<List<int>>("testList");
        Debug.Log("****************************List***************************");
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
        //证明是值拷贝 浅拷贝 不会改变lua 中的内容
        list[0] = 100;
        List<int> list2 = LuaMgr.Instance.Global.Get<List<int>>("testList");
        Debug.Log("****************************List***************************");
        for (int i = 0; i < list2.Count; i++)
        {
            Debug.Log(list2[i]);
            //值不会变 
        }
        
        //不指定类型 object
        List<object> list3 = LuaMgr.Instance.Global.Get<List<object>>("testList2");
        Debug.Log("****************************List object***************************");
        for (int i = 0; i < list3.Count; i++)
        {
            Debug.Log(list3[i]);
        }
        
        //Dictionary
        Debug.Log("****************************Dictionary***************************");
        Dictionary<string,int> dic = LuaMgr.Instance.Global.Get<Dictionary<string,int>>("testDic");
        foreach (string item in dic.Keys)
        {
            Debug.Log(item + "_" + dic[item]);
        }
        //证明是值拷贝 浅拷贝 不会改变lua 中的内容
        dic["1"] = 1000000;
        Dictionary<string,int> dic2= LuaMgr.Instance.Global.Get<Dictionary<string,int>>("testDic");
        foreach (string item in dic2.Keys)
        {
            Debug.Log(item + "_" + dic2[item]);
        }
        
        Debug.Log("****************************Dictionary object***************************");
        Dictionary<object,object> dic3 = LuaMgr.Instance.Global.Get<Dictionary<object,object>>("testDic2");
        foreach (object item in dic3.Keys)
        {
            Debug.Log(item + "_" + dic3[item]);
        }

    }
    
}
