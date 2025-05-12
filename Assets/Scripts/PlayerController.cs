using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 5f;    // 기본 속도
    public float boostedSpeed = 15f;   // 츄르를 먹었을 때 증가하는 속도

    private float currentSpeed;       // 현재 속도를 추적
    private Rigidbody2D rb;           // 고양이의 Rigidbody2D 컴포넌트를 담을 변수
    private bool isInvincible = false;  // 무적 상태 여부

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    // Rigidbody2D 컴포넌트를 가져와서 변수에 저장
        currentSpeed = normalSpeed;          // 초기 속도는 기본 속도
    }

    private void FixedUpdate()
    {
        // 수평 방향으로만 이동, y축은 현재 위치 그대로 유지
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    // 츄르를 먹었을 때 실행되는 함수
    public void ActivateChuruBuff(float duration)
    {
        StartCoroutine(ChuruBuffCoroutine(duration));   // 코루틴을 통해 일정 시간 동안 버프 효과를 주기
    }

    // 코루틴을 사용하여 츄르 버프 적용
    private IEnumerator ChuruBuffCoroutine(float duration)
    {
        isInvincible = true;                 // 무적 상태로 설정
        currentSpeed = boostedSpeed;         // 속도를 증가시킴

        yield return new WaitForSeconds(duration);   // 일정 시간(여기서는 3초) 동안 대기

        isInvincible = false;                // 무적 상태 해제
        currentSpeed = normalSpeed;          // 속도를 원래대로 되돌림
    }

    // 외부에서 무적 상태를 확인할 수 있도록 반환
    public bool IsInvincible()
    {
        return isInvincible;
    }

}