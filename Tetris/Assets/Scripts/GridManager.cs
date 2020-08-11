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
            grid[xPos, yPos] = square;
        }
    }

    public void CheckForLineClear()
    {
        //For each row in the grid, starting from the bottom
        for (int y = 0; y < gridHeight; y++)
        {
            if (HasLine(y))
            {
                ClearLine(y);
                MoveRowsDown(y);
            }
        }
    }


    bool HasLine(int y)
    {
        //Check if each cell is occupied by a square
        for (int x = 0; x < gridWidth; x++)
        {
            //If any cell is unoccupied - return false
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        //The line is full - return true
        return true;
    }
    
    void ClearLine(int y)
    {
        //Delete each square GameObject in the line & clear the corresponding grid reference 
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x,y].gameObject);
            grid[x, y] = null;
        }
    }
    
    void MoveRowsDown(int clearedRow)
    {
        //Move each row above the cleared row down by one 
        for (int y = clearedRow; y < gridHeight; y++)
        {
            //Check across each square in the row
            for (int x = 0; x < gridWidth; x++)
            {
                //If there is a square, move it down one row & update the grid reference
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].gameObject.transform.position += Vector3.down;
                }
            }
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