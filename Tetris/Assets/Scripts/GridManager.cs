using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Public variables
    public int gridHeight = 20;
    public int gridWidth = 10;
    
    //Private variables
    [SerializeField]
    private Transform[,] grid;

    void Start()
    {
        //Initialize grid
        grid = new Transform[gridWidth, gridHeight];
    }

    public void AddBlockToGrid(Transform block)
    {
        foreach (Transform square in block)
        {
            //Get the x,y positions as int
            int xPos = Mathf.RoundToInt(square.transform.position.x);
            int yPos = Mathf.RoundToInt(square.transform.position.y);
            
            //Add the block to the corresponding location in the grid array
            grid[xPos, yPos] = block;
        }
    }

    public bool CheckIfValidMove(Transform block)
    {
        //Check if each square in the Tetromino block is moving outside the bounds of the grid
        foreach (Transform square in block)
        {
            //Get the x,y positions as int
            int xPos = Mathf.RoundToInt(square.transform.position.x);
            int yPos = Mathf.RoundToInt(square.transform.position.y);

            //Check if the position of the square is outside the grid - return false
            if (xPos < 0 || xPos >= gridWidth || yPos < 0 || yPos >= gridHeight)
            {
                return false;
            }

            //Check if each square in the Tetromino block is moving into an occupied grid location - return false
            if (grid[xPos, yPos] != null)
            {
                return false;
            }
        }

        //The move is valid - return true
        return true;
    }
    
}
