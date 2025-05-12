using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Churu : MonoBehaviour
{
    public float buffDuration = 3f;  // 버프 지속 시간 (기본적으로 3초)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 고양이와 충돌한 경우
        {
            PlayerController player = other.GetComponent<PlayerController>();  // 충돌한 객체가 플레이어인지 확인
            if (player != null)  // 만약 플레이어가 맞다면
            {
                player.ActivateChuruBuff(buffDuration);  // 플레이어에게 츄르 버프 적용
            }

            Destroy(gameObject);  // 츄르 아이템은 먹은 후 제거
        }
    }
}
