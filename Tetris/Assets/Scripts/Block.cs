using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Public variables
    public float fallSpeed = 0.9f;

    //Private variables
    private GridManager _gm;
    private float prevTime;
    
    void Start()
    {
        //Find the reference to the GridManager
        _gm = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
    }
    
    void Update()
    {
        //Check for player input 
        
        //Move block to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            //Reverse the movement if it is not a valid move
            if (!_gm.ValidMove(transform))
            {
                transform.position += Vector3.right;
            }
        }
        //Move block to the right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            //Reverse the movement if it is not a valid move
            if (!_gm.ValidMove(transform))
            {
                transform.position += Vector3.left;
            }
        }
        //Rotate block 90deg clockwise 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            //Reverse the movement if it is not a valid move
            if (!_gm.ValidMove(transform))
            {
                transform.Rotate(new Vector3(0, 0, -90), Space.Self);
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
