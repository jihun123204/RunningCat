using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public int healAmount = 10;  // 회복할 체력 

    // 플레이어가 이 오브젝트와 충돌했을 때 실행되는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 대상이 "Player" 태그를 가진 경우에만 실행
        if (other.CompareTag("Player"))
        {
            // 충돌한 오브젝트에서 PlayerHealth 컴포넌트를 가져옴
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            // PlayerHealth 컴포넌트가 있다면 체력 회복 실행
            if (health != null)
            {
                health.Heal(healAmount);  // 체력 회복
            }

            // 참치캔 오브젝트는 먹힌 후 삭제됨
            Destroy(gameObject);
        }
    }
}
