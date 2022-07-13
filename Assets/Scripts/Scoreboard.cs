using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    TMP_Text scoreText;
    int score;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        updateScoreText();
    }

    public void IncreaseScore(int ammount)
    {
        score += ammount;
        updateScoreText();
    }
    private void updateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
}
