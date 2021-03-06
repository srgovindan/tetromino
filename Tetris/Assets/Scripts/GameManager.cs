﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// INTENT: The GameManager keeps track of the game state, checks for the game over condition & keeps track of the
/// global game variables.
/// USAGE: Place this script on the GameManager GameObject.
/// </summary>
public class GameManager : MonoBehaviour
{
    //Public variables 
    public float BlockFallSpeed = 0.9f;
    public float MaximumFallSpeed = 0.1f;
    public float FallSpeedChange = 0.1f;
    public int LinesToLevelUp = 10;
    
    //Private variables
    private enum GameState
    {
        playing,
        endscreen
    }
    [SerializeField]
    private GameState _currentGameState;
    private SpawnManager _spm;
    private ScoreManager _scm;
    private AudioManager _am;

    void Start()
    {
        //Initialize the game state
        _currentGameState = GameState.playing;
        
        //Find references to Managers
        _spm = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        _scm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        _am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    void Update()
    {
        switch (_currentGameState)
        {
            case GameState.playing:
                //Game is running
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
    
    /// <summary>
    /// Checks if a block has topped out the grid, thus ending the game.
    /// Returns true/false.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Stops the current game and displays the End Screen.
    /// </summary>
    void EndCurrentGame()
    {
        //Set CurrentGameState to EndScreen state
        _currentGameState = GameState.endscreen;
        
        //Stop BGM 
        _am.StopAllAudio();
        
        //Play SFX
        _am.PlayAudioClip(3);

        //Call EndGame UI
        _scm.DisplayEndGameUI();
    }
}