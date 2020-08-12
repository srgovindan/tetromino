using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Public variables
    public TMP_Text ScoreTextbox;
    public TMP_Text LevelTextbox;
    public TMP_Text LinesTextbox;
    public TMP_Text EndGameTextbox;
    public TMP_Text RestartInfoTextbox;

    //Private variables
    private int score;
    private int level;
    private int lines;

    void UpdateUI()
    {
        ScoreTextbox.text = score.ToString();
        LevelTextbox.text = level.ToString();
        LinesTextbox.text = lines.ToString();
    }

    public void DisplayEndGameUI()
    {
        EndGameTextbox.text = "GAME OVER!";
        RestartInfoTextbox.text = "Press the 'R' key to restart.";
    }

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
        //Update the score & lines
        lines += numLines;
        score += scoreToAdd;
        
        //todo CheckAndUpdateLevel()
        UpdateUI();
    }
    
}
