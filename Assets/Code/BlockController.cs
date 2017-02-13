using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public const float movementSpeed = .0005f;
    public bool moving;
    public float goalRotation;
    public float rotationDirection; //11 for clockwise, 12 for counterclockwise

    // Use this for initialization
    void Start()
    {
        moving = false;
    }

    internal void OnColliderEnter2D(Collider2D other)
    {
        Debug.Log("calling onTriggerEnter");
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
    }


    internal void rotateBlock()
    {
        transform.Rotate(0, 0, rotationDirection * movementSpeed * Time.deltaTime);
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

    }
}