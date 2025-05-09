using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int score = 0;  //��������
    [SerializeField] private TextMeshProUGUI scoreText;  // ������� UI


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
        UpdateScoreUI();  //�����Ҷ� ���� UI �ݿ�
    }

    public void AddScore(int amount)
    {
        score += amount;    //���� ����
        UpdateScoreUI();    // ���� UI �ݿ�
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;    //�ؽ�Ʈ ǥ��
    }

}
