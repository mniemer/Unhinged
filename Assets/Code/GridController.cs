using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
public class GridController : MonoBehaviour {

    public Transform[,] gridMatrix = new Transform[20,20];
    public int arenaLength;
    public int arenaHeight;
    public int gridBottomPos;
    public int gridTopPos;
    public int gridLeftPos;
    public int gridRightPos;
    float startTime = 0.0f;
    float goalTime =  0.0f;
    List<GameObject> fades = new List<GameObject>();
    List<GameObject> newWalls = new List<GameObject>(); //Initial color, gameobject
    List<Vector4> oldColors = new List<Vector4>();
	// Use this for initialization
	void Start ()
    {
	    updateGrid();
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

    internal void fadeAndDestroy()
    {

        float currTime = Time.time;
        float percentage = 1.0f - (goalTime - currTime) / (goalTime - startTime);
        foreach (GameObject fade in fades)
        {
            SpriteRenderer sr = fade.transform.GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - percentage);
        } 


    }

    internal void turnBlack()
    {
        float currTime = Time.time;
        float percentage = 1.0f - (goalTime - currTime) / (goalTime - startTime);
        for(int i = 0; i <newWalls.Count; ++i)
        {
            SpriteRenderer sr = newWalls[i].transform.GetComponent<SpriteRenderer>();
            sr.color = Vector4.Lerp(oldColors[i], new Vector4(0, 0, 0, 1f), percentage);
        }

    }

    internal void allTheWayBlack()
    {
        foreach(GameObject newWall in newWalls)
        {
            SpriteRenderer sr = newWall.transform.GetComponent<SpriteRenderer>();
            sr.color = new Vector4(0, 0, 0, 1f);
        }
        
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
                    if (gridMatrix[i, j].tag.Equals("Square") || gridMatrix[i, j].tag.Equals("Block") || gridMatrix[i, j].tag.Equals("Hinge") || gridMatrix[i,j].tag.Equals("Wall"))
                    {
                        blockCounter++;
                    }
                    /*else if (gridMatrix[i, j].tag.Equals("Wall"))
                    {
                        for (int looper = 0; looper < gridMatrix[i, j].childCount; ++looper)
                        {
                            Transform wallsCurrChild = gridMatrix[i, j].GetChild(looper);
                            in
                        }
                    }*/
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
                        fades.Add(square);
                        //Destroy(square);
                        Transform oldParent = square.transform.parent;
                        square.transform.parent = null;
                        SquareController[] parentChildren = oldParent.GetComponentsInChildren<SquareController>();
                        foreach(SquareController child in parentChildren)
                        {
                            GameObject newParent = new GameObject();
                            newParent.transform.tag = "Wall";
                            newParent.transform.SetParent(transform, true);
                            newParent.transform.position = child.gameObject.transform.position;
                            child.gameObject.transform.SetParent(newParent.transform, true);
                            int myY = GameUtility.gameToGridCoord(child.transform.position.y);
                            if (!destructions.Contains(myY))
                            {
                                oldColors.Add(child.transform.GetComponent<SpriteRenderer>().color);
                                newWalls.Add(child.gameObject);
                            }
                        }

                    }
                }
            }
            startTime = Time.time;
            goalTime = startTime + 1.5f;
            foreach (GameObject hinge in GameObject.FindGameObjectsWithTag("Hinge"))
            {
                int hingeYCoord = GameUtility.gameToGridCoord(hinge.transform.position.y);
                foreach (int j in destructions)
                {
                    if (j == hingeYCoord)
                    {
                        // If we have other children besides the hinge, turn their parent into a Wall
                        SquareController[] leftoverSquares = hinge.transform.parent.GetComponentsInChildren<SquareController>();
                        Destroy(hinge.transform.parent.gameObject);
                        fades.Add(hinge);
                        //Destroy(hinge);
                        foreach (var leftoverSquare in leftoverSquares)
                        {
                            GameObject newParent = new GameObject();
                            newParent.transform.tag = "Wall";
                            newParent.transform.SetParent(transform, true);
                            newParent.transform.position = leftoverSquare.gameObject.transform.position;
                            leftoverSquare.gameObject.transform.SetParent(newParent.transform, true);
                            int yCheck = GameUtility.gameToGridCoord(leftoverSquare.transform.position.y);
                            if (!destructions.Contains(yCheck))
                            {
                                //leftoverSquare.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
                                oldColors.Add(leftoverSquare.transform.GetComponent<SpriteRenderer>().color);
                                newWalls.Add(leftoverSquare.gameObject);
                            }
                           
                        }
                    }
                }
            }
        }
    }

    void deleteEmptyParents()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.childCount == 0 && t.tag == "Wall")
            {
                Destroy(t.gameObject);
            }
        }
    }
    internal void clearFades()
    {
        foreach (GameObject fade in fades)
        {
            Destroy(fade);
        }
        fades.Clear();
    }
    // Update is called once per frame
    internal void Update () {
        GameUtility.HandleSceneInput();

        if (goalTime > Time.time)
        {
            fadeAndDestroy();
            turnBlack();
        }
        else
        {
            if (fades.Count > 0)
            {
                clearFades();
                allTheWayBlack();
            }
            if (GameUtility.areBlocksAllStill())
            {
                clearRow();
            }
            
            updateGrid();
            deleteEmptyParents();
        }
        
	}
}
