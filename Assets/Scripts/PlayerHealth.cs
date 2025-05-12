using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;  //�ִ�ü��
    private int currentHP;   // ����ü��

    public Slider hpSlider;  // ü�¹� UI

    public float damageInterval = 1f; // �� �ʸ��� ������
    public int damagePerTick = 1;     // �ѹ��� ���̴� ��

    private void Start()
    {
        currentHP = maxHP;  //�����Ҷ� �ִ� ü������ ����
        UpdateHPUI(); // UIHP ü��ǥ�� ������Ʈ

        // ü�� ���� ���� ����
        StartCoroutine(DrainHealthOverTime());
    }

    IEnumerator DrainHealthOverTime()  // �����ð� ���� ü���� �پ��� �ڷ�ƾ
    {
        while (currentHP > 0)
        {
            yield return new WaitForSeconds(damageInterval); // �ð� ��ٸ�
            TakeDamage(damagePerTick);                       // ü�°��� ����
        }

        // HP�� 0�̸� ��� ó��
        Debug.Log("ü�� ����! ���� ����");
    }

    public void TakeDamage(int amount) //ü�°��� �Լ�
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();
    }

    public void Heal(int amount)  //ü��ȸ�� �Լ�
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();
    }

    private void UpdateHPUI()  // ü�� UI ������Ʈ �Լ�
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }
    }
}
