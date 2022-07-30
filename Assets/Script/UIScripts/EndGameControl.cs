using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameControl : MonoBehaviour
{
     public Text resultText;
     public Text finalScore;
     public Text finalScoreNum;

    void Start()
    {
        if (GameOnController.lastGameWon)
        {
            this.resultText.text = "You Won!";
            finalScoreNum.text = (GameOnController.lastGameScore).ToString();
        }
        else
        {
            this.resultText.text = "You Lost!";
            finalScoreNum.text = (GameOnController.lastGameScore).ToString();
        }
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("Menu 1");
    }
}
