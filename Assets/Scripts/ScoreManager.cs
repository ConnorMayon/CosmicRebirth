using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    private int score = 0;
    private int highScore = 1000;
    private float timer = 0f;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreText.text = "High Score: " + highScore;
        StartCoroutine(IncrementScore());
    }

    private void Update()
    {
        // Update the score every 10 seconds
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            AddScore(10);
            timer = 0f;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        // Update high score if necessary
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    IEnumerator IncrementScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            AddScore(10);
        }
    }
}
