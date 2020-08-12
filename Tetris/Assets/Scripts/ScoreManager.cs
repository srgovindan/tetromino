using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Public variables
    public TMP_Text ScoreTextbox;
    public TMP_Text LevelTextbox;
    public TMP_Text LinesTextbox;
    public TMP_Text EndGameTextbox;
    public TMP_Text RestartInfoTextbox;
    public Image EndGamePanel;

    //Private variables
    private GameManager _gam;
    private AudioManager _am;
    private int score;
    private int level;
    private int lines;

    void Start()
    {
        //Find references to Managers
        _gam = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    /// <summary>
    /// Updates all the UI with the current score, level & lines cleared values.
    /// </summary>
    void UpdateUI()
    {
        ScoreTextbox.text = score.ToString();
        LevelTextbox.text = level.ToString();
        LinesTextbox.text = lines.ToString();
    }

    /// <summary>
    /// Displays the Game Over UI.
    /// </summary>
    public void DisplayEndGameUI()
    {
        EndGamePanel.enabled = true;
        EndGameTextbox.text = "GAME OVER!";
        RestartInfoTextbox.text = "Press the 'R' key to restart.";
    }

    /// <summary>
    /// Updates the score, level & lines cleared values based on the number of lines cleared,
    /// then updates the UI with the new values.
    /// </summary>
    /// <param name="numLines"></param>
    public void UpdateScore(int numLines)
    {
        //Calculate the score to add based on number of cleared lines
        int scoreToAdd = 0;
        switch (numLines)
        {
            case 1:
                scoreToAdd = 50 * (level + 1);
                break;
            case 2:
                scoreToAdd = 150 * (level + 1);
                break;
            case 3:
                scoreToAdd = 350 * (level + 1);
                break;
            case 4:
                scoreToAdd = 1000 * (level + 1);
                break;
            default:
                Debug.Log("Something went wrong during the score calculation!");
                break;
        }
        //Update the score
        score += scoreToAdd;

        //Update the lines cleared
        for (int i = 0; i < numLines; i++)
        {
            lines++;
            if (isLevelUp())
            {
                LevelUp();
            }
        }

        UpdateUI();
    }

    /// <summary>
    /// Checks if the player has cleared enough lines to level up.
    /// Returns true/false.
    /// </summary>
    /// <returns></returns>
    bool isLevelUp()
    {
        if (lines % _gam.LinesToLevelUp == 0)
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Increases the level of the game & the fall speed of the blocks.
    /// </summary>
    void LevelUp()
    {
        level++;
        if (_gam.BlockFallSpeed > _gam.MaximumFallSpeed)
        {
            _gam.BlockFallSpeed -= _gam.FallSpeedChange;
        }
        
        //Play SFX
        _am.PlayAudioClip(5);
    }
    
}
