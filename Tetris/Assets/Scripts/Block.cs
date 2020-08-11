using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Public variables
    public float fallSpeed = 0.9f;

    //Private variables
    private float prevTime;
    
    //References
    private GridManager _gm;
    
    void Start()
    {
        //Find the reference to the GridManager
        _gm = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
    }
    
    void Update()
    {
        //Check for player inputs to move Tetromino Left/Right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            //Reverse the movement if it is not a valid move
            if (!_gm.ValidMove(transform))
            {
                transform.position += Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            //Reverse the movement if it is not a valid move
            if (!_gm.ValidMove(transform))
            {
                transform.position += Vector3.left;
            }
        }

        //Make the block fall based on the fall speed
        //Block falls faster when the down arrow key is pressed
        if (Time.time - prevTime >= (Input.GetKey(KeyCode.DownArrow)? fallSpeed/10 : fallSpeed))
        {
            transform.position += Vector3.down;
            //Reverse the movement if it is not a valid move
            if (!_gm.ValidMove(transform))
            {
                transform.position += Vector3.up;
            }
            prevTime = Time.time;
        }
        
    }
}
