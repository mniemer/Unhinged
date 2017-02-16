using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GridController : MonoBehaviour {

    public Transform[,] gridMatrix = new Transform[20,20];
    private bool gameOver;

	// Use this for initialization
	void Start ()
    {
	    updateGrid();
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
            else if (child.tag.Equals("Block") || child.tag.Equals("Wall"))
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

    void OnGUI()
    {
        if (gameOver)
        {
            GUI.Label(new Rect(1000, 450, 1000, 500), "You Win!");
        }
    }

    // Update is called once per frame
    void Update () {
        //updateGrid();
	}
}
