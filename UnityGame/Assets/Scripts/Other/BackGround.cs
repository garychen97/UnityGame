using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Vector2 screenSize;
    public RectTransform rect;
    
    void Start()
    {
        screenSize = GetScreenSize();
        rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = screenSize;
    }

    private Vector2 GetScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }
}
