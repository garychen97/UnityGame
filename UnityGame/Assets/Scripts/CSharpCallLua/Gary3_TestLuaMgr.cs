using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gary3_TestLuaMgr : MonoBehaviour
{
    private void Start()
    {
        LuaMgr.Instance.Init();
        //LuaMgr.Instance.DoString("require('Main')");
        LuaMgr.Instance.DoLuaFile("Main");
    }
}
