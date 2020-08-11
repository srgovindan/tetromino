using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Public variables
    public int gridHeight = 20;
    public int gridLength = 10;
    
    public bool ValidMove(Transform block)
    {
        //Check if each square in the Tetromino block is moving to a valid location in the grid
        foreach (Transform square in block)
        {
            //Get the x,y positions as int
            int xPos = Mathf.RoundToInt(square.transform.position.x);
            int yPos = Mathf.RoundToInt(square.transform.position.y);
            
            //Check if the position of the square is outside the grid - return false
            if (xPos < 0 || xPos >= gridLength || yPos < 0 || yPos >= gridHeight)
            {
                return false;
            }
        }
        
        //The move is valid - return true
        return true;
    }
    
    
    
}
