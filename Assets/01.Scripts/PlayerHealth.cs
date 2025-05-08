using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Animator animator;

    public int maxHealth = 100;
    public int currentHealth;

    public float healthDrainRate = 1f;      //      초당 1씩 체력 까이는 코드
    private float healthTimer = 0f;

    public bool Die = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthTimer += Time.deltaTime;
        if (healthTimer >= 1f)
        {
            currentHealth -= (int)healthDrainRate;
            healthTimer = 0f;

            Debug.Log($"현재 체력: {currentHealth}");

            if (currentHealth <= 0)
            {
                Dead();
            }
        }
    }

    void Dead()
    {
        Debug.Log("체력 없음");
        animator.SetBool("Dead", true);
        //  이제 이곳에 캐릭터 사망시 GameOver UI 등을 넣으시면 됩니다
    }
}
