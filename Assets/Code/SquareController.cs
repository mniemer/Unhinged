using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour {
    public int lastXCoord;
    public int lastYCoord;
    public Vector4 startColor;
    public Vector4 redColor = new Vector4(1f, 0f, 0f, 1f);
    public bool changeColors;
    public float startTime;
    public float halfDuration = 1f;
	// Use this for initialization
	void Start () {
        startColor = transform.GetComponent<SpriteRenderer>().color;
        changeColors = false;
        Debug.Log(startColor);
	}
	
	// Update is called once per frame
	void Update () {
	    if (changeColors)
        {
            if ((Time.time - startTime ) < halfDuration)
            {
                transform.GetComponent<SpriteRenderer>().color = Vector4.Lerp(startColor, redColor, (Time.time - startTime) / halfDuration);
            }
            else if ((Time.time - startTime) < (2 * halfDuration))
            {
                transform.GetComponent<SpriteRenderer>().color = Vector4.Lerp(startColor, redColor,  1 - ((Time.time - halfDuration - startTime) / halfDuration));
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().color = startColor;
                changeColors = false;
            }
        }
        else
        {
            if (startTime > 1f)
            {
             //   Debug.Log("INITIAL COLOR: " + startColor.ToString());
               // Debug.Log("Curr Color: " + transform.GetComponent<SpriteRenderer>().color.ToString());
            }
        }
	}
}
