using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GridController : MonoBehaviour {

    public Transform[,] gridMatrix = new Transform[20,20];
    private bool gameOver;
    private bool clearTest = false;
    public int arenaLength;
    public int arenaHeight;
    public int gridBottomPos;
    public int gridTopPos;
    public int gridLeftPos;
    public int gridRightPos;
	// Use this for initialization
	void Start ()
    {
	    updateGrid();
        float startPos = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        int playerStartPos = GameUtility.gameToGridCoord(startPos);
        float rightPos = GameObject.FindGameObjectWithTag("WallRight").transform.position.x;
        gridRightPos = GameUtility.gameToGridCoord(rightPos);
        float leftPos = GameObject.FindGameObjectWithTag("WallLeft").transform.position.x;
        gridLeftPos = GameUtility.gameToGridCoord(leftPos);
        arenaLength = gridRightPos - gridLeftPos - 1;

        float topPos = GameObject.FindGameObjectWithTag("WallTop").transform.position.y;
        gridTopPos = GameUtility.gameToGridCoord(topPos);
        float bottomPos = GameObject.FindGameObjectWithTag("WallBottom").transform.position.y;
        gridBottomPos = GameUtility.gameToGridCoord(bottomPos);
        arenaHeight = gridTopPos - gridBottomPos;
       
    }

    internal void updateGrid()
    {
        // start by assigning all indeces to null
        for (int i = 0; i < gridMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < gridMatrix.GetLength(1); j++)
                gridMatrix[i, j] = null;
        }

        // assign all of the gameObjects in the grid to their appropriate spots
        
        foreach (Transform child in transform)
        {
            if (child.tag.Equals("Player") || child.tag.Equals("Wall")) // add Goal later.
            {
                //Debug.Log("Found a player tag.");
                int gridCoordX = GameUtility.gameToGridCoord(child.position.x);
                int gridCoordY = GameUtility.gameToGridCoord(child.position.y);
                gridMatrix[gridCoordX, gridCoordY] = child;
            }
            else if (
                child.tag.Equals("Block") || 
                child.tag.Equals("WallTop") || 
                child.tag.Equals("WallBottom") || 
                child.tag.Equals("WallRight") || 
                child.tag.Equals("WallLeft") ||
                child.tag.Equals("PushableBlock")
                )
            {
                foreach (Transform blockChild in child.transform)
                {
                    
                    int gridCoordX = GameUtility.gameToGridCoord(blockChild.position.x);
                    int gridCoordY = GameUtility.gameToGridCoord(blockChild.position.y);
                    gridMatrix[gridCoordX, gridCoordY] = child;
                }
            }
        }

        
       
        /*
        //make sure this shit is right
        for (int i = 0; i < gridMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < gridMatrix.GetLength(1); j++)
            {
                if (gridMatrix[i, j] == null)
                    Debug.Log("The cell at grid coordinate " + i + ", " + j + " is null.");
                else
                    Debug.Log("The cell at grid coordinate " + i + ", " + j + " is " + gridMatrix[i, j].tag + ".");
            }
        }
        */
        
    }


    internal void clearRow()
    {

        // start by assigning all indeces to null
        int blockCounter = 0;
        List<int> destructions = new List<int>();
        for (int j = gridBottomPos; j <= gridTopPos; ++j)
        {
            blockCounter = 0;
            for (int i = gridLeftPos; i <= gridRightPos; ++i)
            {
                if (gridMatrix[i, j] != null)
                {
                    if (gridMatrix[i, j].tag.Equals("Square") || gridMatrix[i, j].tag.Equals("Block") || gridMatrix[i, j].tag.Equals("Hinge"))
                    {
                        blockCounter++;
                    }
                }
            }
            if (blockCounter == arenaLength && !destructions.Contains(j))
            {
                destructions.Add(j);
            }
        }
        if (destructions.Count > 0)
        {
            Debug.Log("CLEAR");
            // Code for deleting squares and hinges
            foreach (GameObject square in GameObject.FindGameObjectsWithTag("Square"))
            {
                int squareYCoord = GameUtility.gameToGridCoord(square.transform.position.y);
                foreach (int j in destructions)
                {
                    if (j == squareYCoord)
                    {
                        BlockController parent = square.GetComponentInParent<BlockController>();
                        Destroy(square);

                    }
                }
            }
            foreach (GameObject hinge in GameObject.FindGameObjectsWithTag("Hinge"))
            {
                int hingeYCoord = GameUtility.gameToGridCoord(hinge.transform.position.y);
                foreach (int j in destructions)
                {
                    if (j == hingeYCoord)
                    {
                        // If we have other children besides the hinge, turn their parent into a PushableBlock

                        GameObject newParent = new GameObject();
                        if (hinge.transform.parent.childCount > 1)
                        {
                            newParent.AddComponent<PushableBlock>();
                            newParent.transform.SetParent(transform, true);
                            newParent.transform.position = hinge.transform.parent.transform.position;
                            SquareController[] leftoverSquares = hinge.transform.parent.GetComponentsInChildren<SquareController>();
                            // basically make all of the squares part of a pushable block.
                            Destroy(hinge.transform.parent.gameObject);
                            foreach (SquareController square in leftoverSquares)
                            {
                                if (square.tag == "Hinge")
                                {
                                    continue;
                                }
                                square.transform.SetParent(newParent.transform, true);
                            }
                        }
                        Destroy(hinge);
                    }
                }
            }
        }
    }
            



        
        
    

    void OnGUI()
    {
        if (gameOver)
        {
            GUI.Label(new Rect(1000, 450, 1000, 500), "You Win!");
        }
    }

    void deleteEmptyParents()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.childCount == 0 && t.tag == "PushableBlock")
            {
                Destroy(t.gameObject);
            }
        }
    }

    // Update is called once per frame
    internal void Update () {
        clearRow();
        updateGrid();
        if (!clearTest)
        {
            Debug.Log(arenaLength);
            clearTest = true;
        }
        deleteEmptyParents();
        
	}
}
