using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int score = 0;  //��������
    [SerializeField] private int highScore = 0;  // �ְ� ����

    [SerializeField] private TextMeshProUGUI scoreText;  // ������� UI
    [SerializeField] private TextMeshProUGUI highScoreText;  // �ְ� ���� ��� UI


    private void Awake()
    {
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
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);  // ����� �ְ� ���� �ҷ�����
        UpdateScoreUI();  //�����Ҷ� ���� UI �ݿ�
    }

    public void AddScore(int amount)
    {
        score += amount;    //���� ����

        // �ְ� ���� ���� Ȯ��
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);  // �ְ� ���� ����
        }

        UpdateScoreUI();    // ���� UI �ݿ�
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;    //�ؽ�Ʈ ǥ��
        highScoreText.text = "High Score: " + highScore;
    }

}
