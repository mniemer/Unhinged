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
	// Use this for initialization
	void Start ()
    {
	    updateGrid();
        float startPos = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        int gridStartPos = GameUtility.gameToGridCoord(startPos);
        float endPos = GameObject.FindGameObjectWithTag("Goal").transform.position.x;
        int gridEndPos = GameUtility.gameToGridCoord(endPos);
        arenaLength = gridEndPos - gridStartPos;
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
            //Debug.Log("In the child loop");
            if (child.tag.Equals("Player"))
            {
                //Debug.Log("Found a player tag.");
                int gridCoordX = GameUtility.gameToGridCoord(child.position.x);
                int gridCoordY = GameUtility.gameToGridCoord(child.position.y);
                gridMatrix[gridCoordX, gridCoordY] = child;
            }
            else if (child.tag.Equals("Block") || child.tag.Equals("WallTop") || child.tag.Equals("WallBottom") || child.tag.Equals("Wall"))
            {
                //Debug.Log("Found a block/wall/goal tag");
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
            for (int i = 0; i < arenaLength; ++i)
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
            foreach (GameObject square in GameObject.FindGameObjectsWithTag("Square")){
                int squareYCoord = GameUtility.gameToGridCoord(square.transform.position.y);
                foreach (int j in destructions)
                {
                    if (j == squareYCoord)
                    {
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

    // Update is called once per frame
    internal void Update () {
        clearRow();
        updateGrid();
        if (!clearTest)
        {
            Debug.Log(arenaLength);
            clearTest = true;
        }
        
	}
}
