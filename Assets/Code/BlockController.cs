using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
    public const float movementSpeed = .0005f;
    public bool moving;
    public float goalRotation;
    public float rotationDirection; //11 for clockwise, 12 for counterclockwise

    // Use this for initialization
    void Start ()
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
            if (direction ==  0 || direction == 3) //player pushing left or down
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
        transform.Rotate(0, 0, rotationDirection*movementSpeed*Time.deltaTime);
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            rotateBlock();
        }
	}
}
