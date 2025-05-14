using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

<<<<<<< HEAD
    [SerializeField] private int score = 0;  //��������
    [SerializeField] private int highScore = 0;  // �ְ� ����

    [SerializeField] private TextMeshProUGUI scoreText;  // ������� UI
    [SerializeField] private TextMeshProUGUI highScoreText;  // �ְ� ���� ��� UI
=======
    [SerializeField] private int score = 0;  // 현재 점수
    [SerializeField] private int highScore = 0;  // 최고 점수
>>>>>>> YDH_Branch

    [Header("🟡 점수 UI")]
    [SerializeField] private TextMeshProUGUI scoreText;      // 현재 점수 텍스트
    [SerializeField] private TextMeshProUGUI highScoreText;  // 최고 점수 텍스트

    private void Awake()
    {
<<<<<<< HEAD
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;   //ó�������� �ν��Ͻ�
        }
        else
        {
            Destroy(gameObject); //�ν��Ͻ��� �ִٸ� �ڱ��ڽ� ����
            return;
        }
        // (�ɼ�) �� ��ȯ �� �����ϴ� �ڵ�
        // DontDestroyOnLoad(gameObject);
=======
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
>>>>>>> YDH_Branch
    }

    private void Start()
    {
<<<<<<< HEAD
        highScore = PlayerPrefs.GetInt("HighScore", 0);  // ����� �ְ� ���� �ҷ�����
        UpdateScoreUI();  //�����Ҷ� ���� UI �ݿ�
=======
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
>>>>>>> YDH_Branch
    }

    public void AddScore(int amount)
    {
<<<<<<< HEAD
        score += amount;    //���� ����

        // �ְ� ���� ���� Ȯ��
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);  // �ְ� ���� ����
        }

        UpdateScoreUI();    // ���� UI �ݿ�
=======
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // 최고 점수 저장
        }

        PlayerPrefs.SetInt("CurrentScore", score); // ✅ 게임오버 씬 전달용 현재 점수 저장

        UpdateScoreUI();
>>>>>>> YDH_Branch
    }


    public void UpdateScoreUI()
    {
<<<<<<< HEAD
        scoreText.text = "Score: " + score;    //�ؽ�Ʈ ǥ��
        highScoreText.text = "High Score: " + highScore;
    }
=======
        if (scoreText != null)
            scoreText.text = "Score: " + score;
>>>>>>> YDH_Branch

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}