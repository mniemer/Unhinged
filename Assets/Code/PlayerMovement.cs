using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public const float movementSpeed = 6f;
    public bool moving;
    public bool updateGrid;
    public short direction; //0 for left, 1 for up, 2 for right, 3 for down
    public float goalX, goalY;
    public bool gameOver = false;
    public float endX;
    public int steps, pushes=0;
    public BlockController lastPushedBlock = null;
    public float transitionTime;



	// Use this for initialization

	void Start ()
	{
        endX = GameUtility.gameToGridCoord(GameObject.FindGameObjectsWithTag("Goal")[0].transform.position.x);
	    moving = false;
        gameOver = false;
	}

    internal void handleArrowKeyInput()
    {
        int currGridX = GameUtility.gameToGridCoord(transform.position.x);
        int currGridY = GameUtility.gameToGridCoord(transform.position.y);
        if (Input.GetKey(KeyCode.RightArrow))
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
        else if (Input.GetKey(KeyCode.LeftArrow))
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
        else if (Input.GetKey(KeyCode.UpArrow))
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
        else if (Input.GetKey(KeyCode.DownArrow))
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

    internal void WASDRotateBlockHandling()
    {
        int currGridX = GameUtility.gameToGridCoord(transform.position.x);
        int currGridY = GameUtility.gameToGridCoord(transform.position.y);
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.GetChild(0).GetComponent<Animation>().Play("pushRight");
            

            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY].tag.Equals("Block")
                 && !transform.parent.GetComponent<GridController>().gridMatrix[currGridX + 1, currGridY].GetComponent<BlockController>().moving
                )
            {
                pushes++;
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
            transform.GetChild(0).GetComponent<Animation>().Play("pushLeft");
           

            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY].tag.Equals("Block")
                 && !transform.parent.GetComponent<GridController>().gridMatrix[currGridX - 1, currGridY].GetComponent<BlockController>().moving
                )
            {
                pushes++;
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
            transform.GetChild(0).GetComponent<Animation>().Play("pushUp");
      

            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1].tag.Equals("Block")
                && !transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY + 1].GetComponent<BlockController>().moving
                )
            {
                pushes++;

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
            transform.GetChild(0).GetComponent<Animation>().Play("pushDown");
           

            if (transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1] != null &&
                transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1].tag.Equals("Block")
                 && !transform.parent.GetComponent<GridController>().gridMatrix[currGridX, currGridY - 1].GetComponent<BlockController>().moving
                 )
            {
                pushes++;

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

    bool isGameOver()
    {
        int playerX = GameUtility.gameToGridCoord(transform.position.x);
        int playerY = GameUtility.gameToGridCoord(transform.position.y);
        int finishedX = GameUtility.gameToGridCoord(GameObject.FindGameObjectsWithTag("Goal")[0].transform.position.x);
        int finishedY = GameUtility.gameToGridCoord(GameObject.FindGameObjectsWithTag("Goal")[0].transform.position.y);
        return (playerX == finishedX && playerY == finishedY);
    }

    int calculateScore()
    {
        return (int)(.25 * steps + pushes);
    }

    void OnGUI()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(1, 1, Color.white);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();
        GUIStyle style= new GUIStyle();
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.black;
        style.normal.background = texture;

        Texture2D endGameTexture = new Texture2D(1, 1);
        endGameTexture.SetPixel(1, 1, Color.black);
        endGameTexture.wrapMode = TextureWrapMode.Repeat;
        endGameTexture.Apply();
        GUIStyle endGame = new GUIStyle();
        endGame.fontSize = 50;
        endGame.alignment = TextAnchor.MiddleCenter;
        endGame.normal.textColor = Color.cyan;
        endGame.normal.background = endGameTexture;

        string currentScene = SceneManager.GetActiveScene().name;
        string num = currentScene.Substring(5);
        int i = Int32.Parse(num);
        if (i != 0 && i != 21)
        {
            GUI.Label(new Rect(200, 20, 90, 35), "Steps: " + steps, style);
            GUI.Label(new Rect(300, 20, 100, 35), "Pushes: " + pushes, style);
            GUI.Label(new Rect(410, 20, 90, 35), "Score: " + calculateScore(), style);
            GUI.Label(new Rect(510, 20, 70, 35), "Par: " + GameUtility.getLevelPar(), style);
        }
        if (gameOver && (i!=0 && i!=21))
        {
            string endGameString = "You Win!\nYour score was: " + calculateScore() + "\nPar was: " +
                                   GameUtility.getLevelPar();
           GUI.Label(new Rect(600, 200, 500, 250), endGameString, endGame);
        }
    }


    // Update is called once per frame
    void Update () {

        GameUtility.HandleSceneInput();

        if (gameOver)
        {
            if (moving)
            {
                movePlayer();
            }
            if (Time.time >= transitionTime)
            {
                GameUtility.loadNextLevel();
            }
            else
            {
                return;
            }
        }

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
	    else if (!blockWait)
	    {
	        handleArrowKeyInput();
            WASDRotateBlockHandling();
        }
        
	    if (updateGrid)
	    {
            transform.parent.GetComponent<GridController>().updateGrid();
            updateGrid = false;
	    }

        if(isGameOver())
        {
            gameOver = true;
            transitionTime = 1.5f + Time.time;
            string currentScene = SceneManager.GetActiveScene().name;
            string num = currentScene.Substring(5);
            int i = Int32.Parse(num);
            if (i == 0)
            {

                GameUtility.loadNextLevel();
            }
        }
    }

    
}
