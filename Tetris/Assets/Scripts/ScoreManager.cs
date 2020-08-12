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

    public void ScoreBasedOnLines(int numLines)
    {
        
    }
    
}
