using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int score = 0;  // ȹ�� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ���� �߰�
            ScoreManager.Instance.AddScore(score);
            // ������ ����
            Destroy(gameObject);
        }
    }
}
