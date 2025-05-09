using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int score = 0;  //현재점수
    [SerializeField] private TextMeshProUGUI scoreText;  // 점수출력 UI


    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;   //처음생성한 인스턴스
        }
        else
        {
            Destroy(gameObject); //인스턴스가 있다면 자기자신 삭제
            return;
        }
        // (옵션) 씬 전환 시 유지하는 코드
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();  //시작할때 점수 UI 반영
    }

    public void AddScore(int amount)
    {
        score += amount;    //점수 누적
        UpdateScoreUI();    // 점수 UI 반영
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;    //텍스트 표시
    }

}
