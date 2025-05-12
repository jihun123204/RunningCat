using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public int healAmount = 10;  // ȸ���� ü�� 

    // �÷��̾ �� ������Ʈ�� �浹���� �� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ����� "Player" �±׸� ���� ��쿡�� ����
        if (other.CompareTag("Player"))
        {
            // �浹�� ������Ʈ���� PlayerHealth ������Ʈ�� ������
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            // PlayerHealth ������Ʈ�� �ִٸ� ü�� ȸ�� ����
            if (health != null)
            {
                health.Heal(healAmount);  // ü�� ȸ��
            }

            // ��ġĵ ������Ʈ�� ���� �� ������
            Destroy(gameObject);
        }
    }
}
