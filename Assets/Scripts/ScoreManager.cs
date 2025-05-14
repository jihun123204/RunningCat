using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int score = 0;  // 현재 점수
    [SerializeField] private int highScore = 0;  // 최고 점수

    [Header("🟡 점수 UI")]
    [SerializeField] private TextMeshProUGUI scoreText;      // 현재 점수 텍스트
    [SerializeField] private TextMeshProUGUI highScoreText;  // 최고 점수 텍스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // 필요한 경우 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // 최고 점수 저장
        }

        PlayerPrefs.SetInt("CurrentScore", score); // ✅ 게임오버 씬 전달용 현재 점수 저장

        UpdateScoreUI();
    }


    public void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}