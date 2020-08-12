﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //Private variables
    private enum GameState
    {
        playing,
        endscreen
    }
    private GameState _currentGameState;
    private SpawnManager _spm;

    void Start()
    {
        //Initialize the game state
        _currentGameState = GameState.playing;
        //Find references to Managers
        _spm = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }
    
    void Update()
    {
        switch (_currentGameState)
        {
            case GameState.playing:
                //stuff
            break;
            
            case GameState.endscreen:
                //Reset the game when the R key is pressed
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            break;
            
            default:
                Debug.Log("Game Manager state machine broke!");
                break;
        }
    }
    
    public bool IsGameOver(Transform block)
    {
        foreach (Transform square in block)
        {
            //Get the y position as int
            int yPos = Mathf.RoundToInt(square.transform.position.y);
            
            //Check if any square has topped out past the spawner location at the top of the grid - return true
            if (yPos >= _spm.gameObject.transform.position.y)
            {
                EndCurrentGame();
                return true;
            }
        }

        //None of the squares has topped out - return false
        return false;
    }

    void EndCurrentGame()
    {
        //Set CurrentGameState to EndScreen state
        _currentGameState = GameState.endscreen;
        //todo call end game ui
    }
}
