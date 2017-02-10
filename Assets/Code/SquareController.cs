using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    internal void OnColliderEnter2D(Collider2D other)
    {
        Debug.Log("Square collider called");
    }
}
