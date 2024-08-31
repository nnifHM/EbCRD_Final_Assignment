using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private float startTime;
    private bool isGameOver = false;

    void Start()
    {
        startTime = Time.time;
        UpdateScoreText(0);
    }

    void Update()
    {
        if (!isGameOver)
        {
            float score = Time.time - startTime;
            UpdateScoreText(score);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void ResetScore()
    {
        startTime = Time.time;
        isGameOver = false;
    }

    private void UpdateScoreText(float score)
    {
        scoreText.text = "Score: " + score.ToString("F0");
    }

    public float GetCurrentScore()
    {
        return Time.time - startTime;
    }
}
