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
    private ScoreManager _scm;
    private AudioManager _am;
    private Transform[,] grid;

    void Start()
    {
        //Find references to Managers
        _scm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        _am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        
        //Initialize grid
        grid = new Transform[gridWidth, gridHeight];
    }

    /// <summary>
    /// Adds a block to the grid array once it has landed to keep track of the occupied grid spaces.
    /// </summary>
    /// <param name="block"></param>
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

    /// <summary>
    /// Checks if any lines have been cleared after a block has landed.
    /// </summary>
    public void CheckForLineClear()
    {
        //Keep track of the number of lines 
        int numLinesCleared = 0;
        
        //For each row in the grid, starting from the top
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            if (HasLine(y))
            {
                numLinesCleared++;
                ClearLine(y);
                MoveRowsDown(y);
            }
        }
        
        //Score if there were lines cleared
        if (numLinesCleared != 0)
        {
            _scm.UpdateScore(numLinesCleared);
            
            //Play SFX based on number of lines cleared
            if (numLinesCleared == 4)
            {
                _am.PlayAudioClip(2);
            }
            else
            {
                _am.PlayAudioClip(1);
            }
        }
    }
    
    /// <summary>
    /// Checks if a given row has a complete line.
    /// Returns true/false.
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Clears a line of Tetromino squares from the grid array.
    /// </summary>
    /// <param name="y"></param>
    void ClearLine(int y)
    {
        //Delete each square GameObject in the line & clear the corresponding grid reference 
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x,y].gameObject);
            grid[x, y] = null;
        }
    }
    
    /// <summary>
    /// Moves all lines above the given row down by one.
    /// </summary>
    /// <param name="clearedRow"></param>
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
    
    /// <summary>
    /// Checks if each square in a given Tetromnio block is moving to a valid space within the grid boundaries.
    /// Returns true/false.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
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