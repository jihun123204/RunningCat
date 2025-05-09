using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;  //최대체력
    private int currentHP;   // 현재체력

    public Slider hpSlider;  // 체력바 UI

    public float damageInterval = 1f; // 몇 초마다 깎을지
    public int damagePerTick = 1;     // 한번에 깎이는 양

    private void Start()
    {
        currentHP = maxHP;  //시작할때 최대 체력으로 설정
        UpdateHPUI(); // UIHP 체력표시 업데이트

        // 체력 감소 루프 시작
        StartCoroutine(DrainHealthOverTime());
    }

    IEnumerator DrainHealthOverTime()  // 일정시간 마다 체력이 줄어드는 코루틴
    {
        while (currentHP > 0)
        {
            yield return new WaitForSeconds(damageInterval); // 시간 기다림
            TakeDamage(damagePerTick);                       // 체력감소 적용
        }

        // HP가 0이면 사망 처리
        Debug.Log("체력 소진! 게임 오버");
    }

    public void TakeDamage(int amount) //체력감소 함수
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();
    }

    public void Heal(int amount)  //체력회복 함수
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();
    }

    private void UpdateHPUI()  // 체력 UI 업데이트 함수
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }
    }
}
