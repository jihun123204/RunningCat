using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Churu : MonoBehaviour
{
    public float buffDuration = 3f;  // ���� ���� �ð� (�⺻������ 3��)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // ����̿� �浹�� ���
        {
            PlayerController player = other.GetComponent<PlayerController>();  // �浹�� ��ü�� �÷��̾����� Ȯ��
            if (player != null)  // ���� �÷��̾ �´ٸ�
            {
                player.ActivateChuruBuff(buffDuration);  // �÷��̾�� �� ���� ����
            }

            Destroy(gameObject);  // �� �������� ���� �� ����
        }
    }
}
