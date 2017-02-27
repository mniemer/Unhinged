using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour {
    public bool moving;
    public float goalX;
    public float goalY;
    public int direction; // 0 for left, 1 for up, 2 for right, 3 for down
    float movementSpeed = 3f;
	// Use this for initialization
	void Start () {
        transform.tag = "PushableBlock";
        moving = false;
    }
	
	// Update is called once per frame

    void moveBlock()
    {
        if(direction == 0)
        {
            if (transform.position.x <= goalX)
            {
                moving = false;
                transform.position = new Vector3(
                    goalX,
                    transform.position.y,
                    transform.position.z
                    );
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x - movementSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                    );
            }
        }
        else if (direction == 1)
        {
            if (transform.position.y >= goalY)
            {
                moving = false;
                transform.position = new Vector3(
                    transform.position.x,
                    goalY,
                    transform.position.z
                    );
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + movementSpeed * Time.deltaTime,
                    transform.position.z
                    );
            }
        }
        else if (direction == 2)
        {
            if (transform.position.x >= goalX)
            {
                moving = false;
                transform.position = new Vector3(
                    goalX,
                    transform.position.y,
                    transform.position.z
                    );
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x + movementSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                    );
            }
        }
        else if (direction == 3)
        {
            if (transform.position.y <= goalY)
            {
                moving = false;
                transform.position = new Vector3(
                    transform.position.x,
                    goalY,
                    transform.position.z
                    );
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - movementSpeed * Time.deltaTime,
                    transform.position.z
                    );
            }
        }
    }
    
	void Update () {
		if (moving)
        {
            moveBlock();
        }
	}

}
