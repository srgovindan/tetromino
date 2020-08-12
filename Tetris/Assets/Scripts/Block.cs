using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Public variables
    public Vector3 rotationPoint;

    //Private variables
    private GameManager _gam;
    private GridManager _grm;
    private SpawnManager _spm;
    private AudioManager _am;
    private float fallSpeed;
    private float prevTime;
    
    void Start()
    {
        //Find references to Managers
        _gam = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _grm = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        _spm = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        
        //Get the fall speed from the Game Manager
        fallSpeed = _gam.BlockFallSpeed;
    }
    
    void Update()
    {
        //Check for player input 
        
        //Move block to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            //Reverse the movement if it is not a valid move
            if (!_grm.CheckIfValidMove(transform))
            {
                transform.position += Vector3.right;
            }
        }
        //Move block to the right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            //Reverse the movement if it is not a valid move
            if (!_grm.CheckIfValidMove(transform))
            {
                transform.position += Vector3.left;
            }
        }
        //Rotate block 90deg clockwise 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), -Vector3.forward, 90);
            //Reverse the movement if it is not a valid move
            if (!_grm.CheckIfValidMove(transform))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), -Vector3.forward, -90);
            }
            
            //Play SFX
            _am.PlayAudioClip(4);
        }
        //Hard drop 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fallSpeed = 0f;
        }

        //Make the block fall based on the fall speed
        //Block falls faster when the down arrow key is pressed
        if (Time.time - prevTime >= (Input.GetKey(KeyCode.DownArrow)? fallSpeed/10 : fallSpeed))
        {
            transform.position += Vector3.down;
            //The block has hit an occupied space in the grid below it
            if (!_grm.CheckIfValidMove(transform))
            {
                //Reverse the movement & add the block to the grid
                transform.position += Vector3.up;
                _grm.AddBlockToGrid(transform);
                _grm.CheckForLineClear();
                //Play SFX
                _am.PlayAudioClip(0);
                
                this.enabled = false;
                
                //If the game is not over, spawn next block
                if (!_gam.IsGameOver(transform))
                {
                    _spm.SpawnBlockFromBag();
                }
            }
            prevTime = Time.time;
        }
    }

}
