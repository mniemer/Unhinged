﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public const float movementSpeed = 2f;
    public const float blockSize = .49f;
    public const float boundaryUp = .5f;
    public const float boundaryDown = -.5f;

    public bool moving;
    public short direction; //0 for left, 1 for up, 2 for right, 3 for down
    public float goalX, goalY;
    public bool gameOver = false;

	// Use this for initialization

	void Start ()
	{
	    moving = false;
	}

    void handleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moving = true;
            direction = 2;
            goalX = transform.position.x + blockSize;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moving = true;
            direction = 0;
            goalX = transform.position.x - blockSize;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moving = true;
            direction = 1;
            goalY = transform.position.y + blockSize;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moving = true;
            direction = 3;
            goalY = transform.position.y - blockSize;
        }

        if (goalY > boundaryUp || goalY < boundaryDown)
        {
            moving = false;
        }
    }

    void movePlayer()
    {
        float desiredX = transform.position.x;
        float desiredY = transform.position.y;
        if (direction == 0) //left
        {
            desiredX = transform.position.x - movementSpeed * Time.deltaTime;
            if (desiredX <= goalX)
            {
                moving = false;
            }
        }
        else if (direction == 2) //right
        {
            desiredX = transform.position.x + movementSpeed * Time.deltaTime;
            if (desiredX >= goalX)
            {
                moving = false;
            }
        }
        else if (direction == 1) //up
        {
            desiredY = transform.position.y + movementSpeed * Time.deltaTime;
            if (desiredY >= goalY)
            {
                moving = false;
            }
        }
        else //down
        {
            desiredY = transform.position.y - movementSpeed * Time.deltaTime;
            if (desiredY <= goalY)
            {
                moving = false;
            }
        }
        transform.position = new Vector3(desiredX, desiredY);
    }

	// Update is called once per frame
	void Update () {
        if (moving)
        {
            movePlayer();
        }
        else
        {
            handleKeyInput();
        }
        if (transform.position.x>(10*blockSize))
        {
            gameOver = true;
        }
    }

    void OnGUI()
    {
        if (gameOver)
        {
            GUI.Label(new Rect(1000, 450, 1000, 500), "You Win!");
        }
    }
}
