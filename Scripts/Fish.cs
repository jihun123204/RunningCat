using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int score = 0;  // 획득 점수

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 점수 추가
            ScoreManager.Instance.AddScore(score);
            // 아이템 제거
            Destroy(gameObject);
        }
    }
}
