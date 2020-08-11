using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Public variables
    public float fallSpeed = 0.9f;

    //Private variables
    private float prevTime;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        //Check for player inputs to move Tetromino Left/Right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }

        //Make the block fall based on the fall speed
        //Block falls faster when the down arrow key is pressed
        if (Time.time - prevTime >= (Input.GetKey(KeyCode.DownArrow)? fallSpeed/10 : fallSpeed))
        {
            transform.position += Vector3.down;
            prevTime = Time.time;
        }
        
    }
}
