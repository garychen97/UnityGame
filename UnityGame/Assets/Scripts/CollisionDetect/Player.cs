using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool posChange = false;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey((KeyCode.A)))
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            posChange = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey((KeyCode.D)))
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            posChange = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey((KeyCode.S)))
        {
            this.transform.position += new Vector3(0,-speed * Time.deltaTime, 0);
            posChange = true;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey((KeyCode.W)))
        {
            this.transform.position += new Vector3(0,speed * Time.deltaTime, 0);
            posChange = true;
        }
        GameManager.Instance.playerRect.center = this.transform.position;
        if (GameManager.Instance.ShowDetectedArea)
        {
            if (posChange)
            {
                GameManager.Instance.DrawDetectedTile();
            }
        }

        if (GameManager.Instance.ShowDetectedEnemy)
        {
            GameManager.Instance.DrawDetectedEnemy();   
        }
    }
}
