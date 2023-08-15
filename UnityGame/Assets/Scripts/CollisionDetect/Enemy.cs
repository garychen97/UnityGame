using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class Enemy
{
    public GameObject go;
    public float width;
    public float height;
    public Vector2 center;
    public SpriteRenderer spriteRenderer;
    public Color originalColor;
    public Color detectedColor;
    public Enemy()
    {
        go = GameObject.Instantiate(GameManager.Instance.enemyPrefab);
        go.transform.parent = EnemyPool.Instance.gameObject.transform;
        spriteRenderer = go.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        detectedColor = Color.magenta;
        width = 0.5f;
        height = 0.5f;
        center = new Vector2(0, 0);
    }

    public void ChangeColor()
    {
        spriteRenderer.color = detectedColor;
    }

    public void RevertColor()
    {
        spriteRenderer.color = originalColor;
    }

    public void SetEnemyPos(Vector2 vec2)
    {
        go.transform.position = vec2;
        center = vec2;
    }

    public void HideThis()
    {
        go.SetActive(false);
    }
    
    public void ShowThis()
    {
        go.SetActive(true);
    }

    public GameObject EnemyGo()
    {
        return go;
    }
    
    public void DestorySelf()
    {
        EnemyPool.Instance.DestroyEnemy(this);
    }
}


