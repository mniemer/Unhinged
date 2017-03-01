using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public bool moving;
    public float oldRotation;
    private GameObject[] originalHinge;
    public short rotationDirection = 1; // counter clockwise == -1, clockwise == 1;
    private float rotationSpeed = 3f;
    public Transform[,] originalGrid;

    // Use this for initialization
    void Start()
    {
        moving = false;
        originalHinge = GameObject.FindGameObjectsWithTag("Hinge");
        oldRotation = transform.eulerAngles.z;
    }

    void Update()
    {
        float currRotation = transform.eulerAngles.z;
        if (moving && Math.Abs(currRotation - oldRotation) >= 85 && Math.Abs(currRotation - oldRotation) <=275)
        {
            if (currRotation <= 5 || (currRotation >= 355 && currRotation <= 361))
                currRotation = 0;
            else if (currRotation >= 85 && currRotation <= 95)
                currRotation = 90;
            else if (currRotation >= 175 && currRotation <= 185)
                currRotation = 180;
            else if (currRotation >= 265 && currRotation <= 275)
                currRotation = 270;
            else
            {
                Debug.Log("Something is wrong with rotation.");
            }
            oldRotation = currRotation;
            snapToRotation(currRotation);
            transform.parent.GetComponent<GridController>().updateGrid();
            moving = false;
        }
        else if (!moving)
        {
            snapToRotation(oldRotation);
        }
        else
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x, 
                transform.eulerAngles.y, 
                transform.eulerAngles.z - (rotationDirection * rotationSpeed));
        }
        int gridXCoord, gridYCoord;
        if (moving)
        {
            foreach (SquareController child in GetComponentsInChildren<SquareController>())
            {
                gridXCoord = GameUtility.gameToGridCoord(child.transform.position.x);
                gridYCoord = GameUtility.gameToGridCoord(child.transform.position.y);
                 
                if ((originalGrid[gridXCoord, gridYCoord] != null) && !inOriginalBlock(gridXCoord, gridYCoord))
                {
                    transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    transform.eulerAngles.y,
                    oldRotation);
                    moving = false;
                    GameObject p = GameObject.FindGameObjectWithTag("Player");
                    p.GetComponent<PlayerMovement>().moving = false;
                    transform.parent.GetComponent<GridController>().updateGrid();
                    //p.GetComponent<PlayerMovement>().pushes -= 1;
                    break;
                }
            }
        }
    }
    private bool inOriginalBlock(int gridXCoord, int gridYCoord)
    {
        
        foreach (SquareController child in GetComponentsInChildren<SquareController>())
        {
            if ((child.lastXCoord == gridXCoord) && (gridYCoord == child.lastYCoord))
            {
                return true;
            }
        }
        return false;
    }
    internal void snapToRotation(float desiredRotation)
    {

        Vector3 snap = new Vector3(0, 0, desiredRotation);
        //transform.eulerAngles = snap;
        transform.localEulerAngles = snap;
    }

}