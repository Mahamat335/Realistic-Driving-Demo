using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class High_Score : MonoBehaviour
{
    private float highScore;
    [SerializeField] private TMP_Text highScoreText;
    void Start()
    {
        if (!PlayerPrefs.HasKey("highScore"))
        {
            highScore = 0f;
            PlayerPrefs.SetFloat("highScore", highScore);
        }
        highScore = PlayerPrefs.GetFloat("highScore");
        int minutes = Mathf.FloorToInt(highScore / 60f);
        int seconds = Mathf.FloorToInt(highScore % 60f);
        int milliseconds = Mathf.FloorToInt((int)(highScore * 1000f) % 1000f);
        string timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        highScoreText.text = timerText;
    }

    public void SetHighScore(float hs)
    {
        highScore = hs;
        PlayerPrefs.SetFloat("highScore", highScore);
        int minutes = Mathf.FloorToInt(highScore / 60f);
        int seconds = Mathf.FloorToInt(highScore % 60f);
        int milliseconds = Mathf.FloorToInt((int)(highScore * 1000f) % 1000f);
        string timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        highScoreText.text = timerText;
    }

    public float GetHighScore()
    {
        return highScore;
    }
}
