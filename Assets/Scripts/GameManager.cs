using System.IO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject startScreenCanvas;
    public GameObject deathScreenCanvas;

    public GameObject player;

    public ScoreManager scoreManager;
    public CameraController cameraController;

    public TextMeshProUGUI highscoreText;

    private bool isGameOver = false;
    private float currentHighscore = 0;

    private string highscoreFilePath;

    
    void Start()
    {
        highscoreFilePath = Path.Combine(Application.dataPath, "Resources/highscore.json");
        LoadHighscore();
        ShowStartScreen();
    }

    void Update()
    {
        if(!isGameOver && IsPalyerOutOfCameraView())
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        startScreenCanvas.SetActive(false);
        deathScreenCanvas.SetActive(false);
        player.SetActive(true);
        isGameOver = false;
        scoreManager.enabled = true;
        scoreManager.ResetScore();
        cameraController.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Game closed!");
        Application.Quit();
    }
    
    void GameOver()
    {
        isGameOver = true;
        player.SetActive(false);
        deathScreenCanvas.SetActive(true);
        scoreManager.GameOver();

        float finalScore = scoreManager.GetCurrentScore();
        if (finalScore > currentHighscore)
        {
            currentHighscore = finalScore;
            SaveHighscore();
        }
        ShowScore();
        cameraController.StopGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStartScreen()
    {
        startScreenCanvas.SetActive(true);
        deathScreenCanvas.SetActive(false);
        player.SetActive(false);
        isGameOver = false;
        scoreManager.enabled = false;
        highscoreText.text = "Bestscore: " + currentHighscore.ToString("F0");
    }

    private void ShowScore()
    {
        TextMeshProUGUI scoreText = deathScreenCanvas.transform.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

        float score = scoreManager.GetCurrentScore();
        scoreText.text = "Punkte: " + score.ToString("F0");
    }

    private void LoadHighscore()
    {
        if (File.Exists(highscoreFilePath))
        {
            string json = File.ReadAllText(highscoreFilePath);
            HighscoreData data = JsonUtility.FromJson<HighscoreData>(json);
            currentHighscore = data.highscore;
            Debug.Log("Highscore geladen: " + currentHighscore);
        }
        else
        {
            Debug.Log("Keine Highscore-Datei gefunden, setze Standardwert 0!");
            currentHighscore = 0;
        }
    }

    private void SaveHighscore()
    {
        HighscoreData data = new HighscoreData();
        data.highscore = currentHighscore;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(highscoreFilePath, json);
        Debug.Log("Highscore gespeichert: " + currentHighscore);
    }

    private bool IsPalyerOutOfCameraView()
    {
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        return player.transform.position.y < cameraBottom;
    }
}
