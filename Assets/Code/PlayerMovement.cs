using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed = .0005f;
    public bool moving;
    public short direction; //0 for left, 1 for up, 2 for right, 3 for down
    public float blockSize = .125f;
    public float goalX, goalY;
	// Use this for initialization
	void Start () {
        moving = false;
	}
	
	// Update is called once per frame
	internal void Update () {
        if (moving)
        {
            float desiredX = gameObject.transform.position.x;
            float desiredY = gameObject.transform.position.y;
            if (direction == 0)
            {
                desiredX = gameObject.transform.position.x - movementSpeed * Time.deltaTime;
                if (desiredX <= goalX)
                {
                    moving = false;
                }
            }
            else if (direction == 2)
            {
                desiredX = gameObject.transform.position.x + movementSpeed * Time.deltaTime;
                if (desiredX >= goalX)
                {
                    moving = false;
                }
            }
            else if (direction == 1)
            {
                desiredY = gameObject.transform.position.y + movementSpeed * Time.deltaTime;
                if (desiredY >= goalY)
                {
                    moving = false;
                }
            }
            else
            {
                desiredY = gameObject.transform.position.y - movementSpeed * Time.deltaTime;
                if (desiredY <= goalY)
                {
                    moving = false;
                }
            }
            gameObject.transform.position = new Vector3(desiredX, desiredY);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moving = true;
                direction = 2;
                Debug.Log("OG x goal:");
                Debug.Log(goalX);
                goalX = transform.position.x + .25f * blockSize;
                Debug.Log("NEW x goal");
                Debug.Log(goalX);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moving = true;
                direction = 0;
                goalX = transform.position.x - .25f * blockSize;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moving = true;
                direction = 1;
                goalY = transform.position.y + .25f * blockSize;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moving = true;
                direction = 3;
                goalY = transform.position.y - .25f * blockSize;
            }
        }
    }
}
