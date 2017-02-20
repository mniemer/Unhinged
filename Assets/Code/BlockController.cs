using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public bool moving;
    private float oldRotation;
    private GameObject[] originalHinge;
    public short rotationDirection = 1; // counter clockwise == -1, clockwise == 1;
    private float rotationSpeed = .8f;
    // Use this for initialization
    void Start()
    {
        moving = false;
        originalHinge = GameObject.FindGameObjectsWithTag("Hinge");
        oldRotation = transform.eulerAngles.z;
    }

     void  getHingePos(GameObject[] test)
    {
        
       test=GameObject.FindGameObjectsWithTag("Hinge");
        

        }

    void Update()
    {
        GameObject[] HingeLoc= GameObject.FindGameObjectsWithTag("Hinge");
        getHingePos(HingeLoc);
        for(int i =0; i<HingeLoc.Length;i++)
        {
            HingeLoc[i].transform.position = originalHinge[i].transform.position;
        }
        float currRotation = transform.eulerAngles.z;
        if (moving && Math.Abs(currRotation - oldRotation) >= 85)
        {
            Debug.Log("The block has moved.");
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
            moving = false;
            oldRotation = currRotation;
            snapToRotation(currRotation);
            transform.parent.GetComponent<GridController>().updateGrid();
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
    }

    internal void snapToRotation(float desiredRotation)
    {

        Vector3 snap = new Vector3(0, 0, desiredRotation);
        //transform.eulerAngles = snap;
        transform.localEulerAngles = snap;
    }

    
    internal void OnColliderEnter2D(Collider2D other)
    {
        Debug.Log("calling Blocks's onTriggerEnter");
        /*
        if (other.tag.Equals("Player"))
        {
            Debug.Log("inside if statement");
            int direction = other.gameObject.GetComponent<PlayerMovement>().direction;
            if (direction == 0 || direction == 3) //player pushing left or down
            {
                rotationDirection = -1;
                goalRotation = transform.rotation.z - 90;
            }
            else if (direction == 1 || direction == 2) //player pushing right or up
            {
                rotationDirection = 1;
                goalRotation = transform.rotation.z + 90;
            }
            else
                Debug.Log("Something has gone terribly wrong.");
            moving = true;


        }
        */
    }

    /*
    internal void rotateBlock()
    {
        transform.Rotate(0, 0, rotationDirection * movementSpeed * Time.deltaTime);
    }

    void Update()
    {
        rotateBlock();
    }
    
    //Clear row that is filled coordinate is 0 if it is a horizontal row
    //1 if it is a vertical row
    void clearRow(int coordinate, int number)
    {
        List lst1 = Component.getComponent<block1>();
        List lst2 = Component.getComponent<block2>();
        List lst3 = Component.getComponent<block3>();
        List lst4 = Component.getComponent<block4>();
        int loc = number;
        int height = 0;
        int width = 0;
        if (coordinate == 0)
        {
            foreach (gameComponent blk in lst1)
            {

                width = blk.rect.width;
                if (blk.transform.rotation.x == loc ||
                    blk.transform.rotation.x - width == loc || blk.transform.rotation.x + width == loc)
                {
                    Destroy(blk);
                }
            }
            foreach (gameComponent blk in lst2)
            {

                width = blk.rect.width;
                if (blk.transform.rotation.x == loc ||
                    blk.transform.rotation.x - width == loc || blk.transform.rotation.x + width == loc)
                {
                    Destroy(blk);
                }
            }
            foreach (gameComponent blk in lst3)
            {

                width = blk.rect.width;
                if (blk.transform.rotation.x == loc ||
                    blk.transform.rotation.x - width == loc || blk.transform.rotation.x + width == loc)
                {
                    Destroy(blk);
                }
            }
            foreach (gameComponent blk in lst4)
            {

                width = blk.rect.width;
                if (blk.transform.rotation.x == loc ||
                    blk.transform.rotation.x - width == loc || blk.transform.rotation.x + width == loc)
                {
                    Destroy(blk);
                }
            }
        }
        if (coordinate == 1)
        {
            foreach (gameComponent blk in lst1)
            {
                height = blk.rect.height;

                if (blk.transform.rotation.y == loc ||
                    blk.transform.rotation.y - height == loc || blk.transform.rotation.y + height == loc)
                {
                    Destroy(blk);
                }
            }
            foreach (gameComponent blk in lst2)
            {
                height = blk.rect.height;

                if (blk.transform.rotation.y == loc ||
                    blk.transform.rotation.y - height == loc || blk.transform.rotation.y + height == loc)
                {
                    Destroy(blk);
                }
            }
            foreach (gameComponent blk in lst3)
            {
                height = blk.rect.height;

                if (blk.transform.rotation.y == loc ||
                    blk.transform.rotation.y - height == loc || blk.transform.rotation.y + height == loc)
                {
                    Destroy(blk);
                }
            }
            foreach (gameComponent blk in lst4)
            {
                height = blk.rect.height;

                if (blk.transform.rotation.y == loc ||
                    blk.transform.rotation.y - height == loc || blk.transform.rotation.y + height == loc)
                {
                    Destroy(blk);
                }
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            rotateBlock();
        }
        int xCount = 0;
        int yCount = 0;
        int xLoc = transform.rotation.x;
        int yLoc = transform.rotation.y;
        int height = 0;
        int width = 0;
        List lst1 = Component.getComponent<block1>();
        List lst2 = Component.getComponent<block2>();
        List lst3 = Component.getComponent<block3>();
        List lst4 = Component.getComponent<block4>();
        foreach (gameComponent blk in lst1)
        {
            height = blk.rect.height;
            width = blk.rect.width;
            if (blk.transform.rotation.x == xLoc)
            {
                xCount += width / blockSize;

            }
            if (blk.transform.rotation.x + width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.y == yLoc)
            {
                yCount += height / blockSize;
            }
            if (blk.transform.rotation.y + hieght == yLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - helgiht == yLoc)
            {
                xCount++;

            }
        }
        foreach (gameComponent blk in lst2)
        {
            height = blk.rect.height;
            width = blk.rect.width;
            if (blk.transform.rotation.x == xLoc)
            {
                xCount += width / blockSize;

            }
            if (blk.transform.rotation.x + width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.y == yLoc)
            {
                yCount += height / blockSize;
            }
            if (blk.transform.rotation.y + hieght == yLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - helgiht == yLoc)
            {
                xCount++;

            }
        }
        foreach (gameComponent blk in lst3)
        {
            height = blk.rect.height;
            width = blk.rect.width;
            if (blk.transform.rotation.x == xLoc)
            {
                xCount += width / blockSize;

            }
            if (blk.transform.rotation.x + width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.y == yLoc)
            {
                yCount += height / blockSize;
            }
            if (blk.transform.rotation.y + hieght == yLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - helgiht == yLoc)
            {
                xCount++;

            }
        }
        foreach (gameComponent blk in lst4)
        {
            height = blk.rect.height;
            width = blk.rect.width;
            if (blk.transform.rotation.x == xLoc)
            {
                xCount += width / blockSize;

            }
            if (blk.transform.rotation.x + width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - width == xLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.y == yLoc)
            {
                yCount += height / blockSize;
            }
            if (blk.transform.rotation.y + hieght == yLoc)
            {
                xCount++;

            }
            if (blk.transform.rotation.x - helgiht == yLoc)
            {
                xCount++;

            }
        }
        if (xCount == 10)
        {
            clearRow(0, xLoc);
        }
        if (yCount == 10)
        {
            clearRow(0, yLoc);
        }
        */
}