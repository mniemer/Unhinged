using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public const float movementSpeed = 4.5f;
    public bool moving;
    public bool updateGrid;
    public short direction; //0 for left, 1 for up, 2 for right, 3 for down
    public float goalX, goalY;
    public bool gameOver = false;
    public float endX;
    public int steps, pushes=0;
    public BlockController lastPushedBlock = null;

	// Use this for initialization

	void Start ()
	{
        endX = GameUtility.gameToGridCoord(GameObject.FindGameObjectsWithTag("Goal")[0].transform.position.x);
	    moving = false;
	}

    internal void handleArrowKeyInput()
    {
        int currGridX = GameUtility.gameToGridCoord(transform.position.x);
        int currGridY = GameUtility.gameToGridCoord(transform.position.y);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currGridX >= 19 ||
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY] != null)
            {
                return;
            }

            steps++;
            moving = true;
            direction = 2;
            goalX = GameUtility.gridToGameCoord(currGridX + 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currGridX <= 0 ||
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY] != null
                
                )
            {
                return;
            }

            steps++;
            moving = true;
            direction = 0;
            goalX = GameUtility.gridToGameCoord(currGridX - 1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currGridY >= 19 ||
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1] != null)
            {
                return;
            }

            steps++;
            moving = true;
            direction = 1;
            goalY = GameUtility.gridToGameCoord(currGridY + 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currGridY <= 0 ||
               transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1] != null)
            {
                return;
            }

            steps++;
            moving = true;
            direction = 3;
            goalY = GameUtility.gridToGameCoord(currGridY - 1);
        }
    }

    internal void handleWASDKeyInput()
    {
        int currGridX = GameUtility.gameToGridCoord(transform.position.x);
        int currGridY = GameUtility.gameToGridCoord(transform.position.y);
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY].tag.Equals("Block"))
            {
                lastPushedBlock = transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY]
                    .GetComponent<BlockController>();
                SquareController[] children = lastPushedBlock.GetComponentsInChildren<SquareController>();
                Vector3 hingePos;
                hingePos.x = 0;
                hingePos.y = 0;
                foreach (SquareController child in children)
                {
                    child.lastXCoord = GameUtility.gameToGridCoord(child.transform.position.x);
                    child.lastYCoord = GameUtility.gameToGridCoord(child.transform.position.y);
                    if (child.tag == "Hinge")
                    {
                        hingePos.x = child.transform.position.x;
                        hingePos.y = child.transform.position.y;
                    }
                }
                int hingeGridY = GameUtility.gameToGridCoord(hingePos.y);
                if(hingeGridY != currGridY)
                {
                    pushes++;
                    lastPushedBlock.moving = true;
                    if (hingeGridY > currGridY)
                    {
                        lastPushedBlock.rotationDirection = -1;
                    }
                    else
                    {
                        lastPushedBlock.rotationDirection = 1;
                    }
                    lastPushedBlock.oldRotation = lastPushedBlock.transform.eulerAngles.z;
                    lastPushedBlock.originalGrid = (Transform[,])transform.parent.GetComponent<GridController>().gridMatrix.Clone();
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY].tag.Equals("Block"))
            {
                lastPushedBlock = transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY]
                    .GetComponent<BlockController>();
                SquareController[] children = lastPushedBlock.GetComponentsInChildren<SquareController>();
                Vector3 hingePos;
                hingePos.x = 0;
                hingePos.y = 0;
                foreach (SquareController child in children)
                {
                    child.lastXCoord = GameUtility.gameToGridCoord(child.transform.position.x);
                    child.lastYCoord = GameUtility.gameToGridCoord(child.transform.position.y);
                    if (child.tag == "Hinge")
                    {
                        hingePos.x = child.transform.position.x;
                        hingePos.y = child.transform.position.y;
                    }
                }
                int hingeGridY = GameUtility.gameToGridCoord(hingePos.y);
                if (hingeGridY != currGridY)
                {
                    pushes++;
                    lastPushedBlock.moving = true;
                    if (hingeGridY > currGridY)
                    {
                        lastPushedBlock.rotationDirection = 1;
                    }
                    else
                    {
                        lastPushedBlock.rotationDirection = -1;
                    }
                    lastPushedBlock.oldRotation = lastPushedBlock.transform.eulerAngles.z;
                    lastPushedBlock.originalGrid = (Transform[,])transform.parent.GetComponent<GridController>().gridMatrix.Clone();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            pushes++;
            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1].tag.Equals("Block"))
            {

                lastPushedBlock = transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1]
                    .GetComponent<BlockController>();
                SquareController[] children = lastPushedBlock.GetComponentsInChildren<SquareController>();
                Vector3 hingePos;
                hingePos.x = 0;
                hingePos.y = 0;
                foreach (SquareController child in children)
                {
                    child.lastXCoord = GameUtility.gameToGridCoord(child.transform.position.x);
                    child.lastYCoord = GameUtility.gameToGridCoord(child.transform.position.y);
                    if (child.tag == "Hinge")
                    {
                        hingePos.x = child.transform.position.x;
                        hingePos.y = child.transform.position.y;
                    }
                }
                int hingeGridX = GameUtility.gameToGridCoord(hingePos.x);
                if (hingeGridX != currGridX)
                {
                    lastPushedBlock.moving = true;
                    lastPushedBlock.oldRotation = lastPushedBlock.transform.eulerAngles.z;
                    if (hingeGridX > currGridX)
                    {
                        lastPushedBlock.rotationDirection = 1;
                    }
                    else
                    {
                        lastPushedBlock.rotationDirection = -1;
                    }
                    lastPushedBlock.originalGrid = (Transform[,])transform.parent.GetComponent<GridController>().gridMatrix.Clone();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1].tag.Equals("Block"))
            {

                lastPushedBlock = transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1]
                    .GetComponent<BlockController>();

                SquareController[] children = lastPushedBlock.GetComponentsInChildren<SquareController>();
                Vector3 hingePos;
                hingePos.x = 0;
                hingePos.y = 0;
                foreach (SquareController child in children)
                {
                    child.lastXCoord = GameUtility.gameToGridCoord(child.transform.position.x);
                    child.lastYCoord = GameUtility.gameToGridCoord(child.transform.position.y);
                    if (child.tag == "Hinge")
                    {
                        hingePos.x = child.transform.position.x;
                        hingePos.y = child.transform.position.y;
                    }
                }

                int hingeGridX = GameUtility.gameToGridCoord(hingePos.x);
                if (hingeGridX != currGridX)
                {
                    pushes++;
                    lastPushedBlock.moving = true;
                    lastPushedBlock.oldRotation = lastPushedBlock.transform.eulerAngles.z;
                  
                    if (hingeGridX > currGridX)
                    {
                        lastPushedBlock.rotationDirection = -1;
                    }
                    else
                    {
                        lastPushedBlock.rotationDirection = 1;
                    }
                    lastPushedBlock.originalGrid = (Transform[,])transform.parent.GetComponent<GridController>().gridMatrix.Clone();
                }
            }
        
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
                desiredX = goalX;
                moving = false;
                updateGrid = true;
            }
        }
        else if (direction == 2) //right
        {
            desiredX = transform.position.x + movementSpeed * Time.deltaTime;
            if (desiredX >= goalX)
            {
                desiredX = goalX;
                moving = false;
                updateGrid = true;
            }
        }
        else if (direction == 1) //up
        {
            desiredY = transform.position.y + movementSpeed * Time.deltaTime;
            if (desiredY >= goalY)
            {
                desiredY = goalY;
                moving = false;
                updateGrid = true;
            }
        }
        else //down
        {
            desiredY = transform.position.y - movementSpeed * Time.deltaTime;
            if (desiredY <= goalY)
            {
                desiredY = goalY;
                moving = false;
                updateGrid = true;
            }
        }
        transform.position = new Vector3(desiredX, desiredY);
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(10, 10, 1000, 500), "Steps:"+steps);
        GUI.Label(new Rect(10, 25, 1000, 500), "Pushes:"+pushes);
        if (gameOver)
        {
            
            GUI.Label(new Rect(400, 10, 1000, 500), "You Win!");
        }
    }


    // Update is called once per frame
    void Update () {
        bool blockWait = false;
        if (lastPushedBlock != null)
        {
            if (lastPushedBlock.moving)
            {
                blockWait = true;
            }
        }
        if (moving && !blockWait)
	        movePlayer();
	    else
	    {
	        handleArrowKeyInput();
            handleWASDKeyInput();
	    }
        
	    if (updateGrid)
	    {
            transform.parent.GetComponent<GridController>().updateGrid();
            updateGrid = false;
	    }
         if(GameUtility.gameToGridCoord(transform.position.x)==endX)
        {
            gameOver = true;
           
        }
        //if (transform.position.x>(10*blockSize))
        //{
        //  gameOver = true;
        //}
    }

    
}
