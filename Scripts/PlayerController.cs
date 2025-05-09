using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 5f;    // �⺻ �ӵ�
    public float boostedSpeed = 15f;   // �򸣸� �Ծ��� �� �����ϴ� �ӵ�

    private float currentSpeed;       // ���� �ӵ��� ����
    private Rigidbody2D rb;           // ������� Rigidbody2D ������Ʈ�� ���� ����
    private bool isInvincible = false;  // ���� ���� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    // Rigidbody2D ������Ʈ�� �����ͼ� ������ ����
        currentSpeed = normalSpeed;          // �ʱ� �ӵ��� �⺻ �ӵ�
    }

    private void FixedUpdate()
    {
        // ���� �������θ� �̵�, y���� ���� ��ġ �״�� ����
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    // �򸣸� �Ծ��� �� ����Ǵ� �Լ�
    public void ActivateChuruBuff(float duration)
    {
        StartCoroutine(ChuruBuffCoroutine(duration));   // �ڷ�ƾ�� ���� ���� �ð� ���� ���� ȿ���� �ֱ�
    }

    // �ڷ�ƾ�� ����Ͽ� �� ���� ����
    private IEnumerator ChuruBuffCoroutine(float duration)
    {
        isInvincible = true;                 // ���� ���·� ����
        currentSpeed = boostedSpeed;         // �ӵ��� ������Ŵ

        yield return new WaitForSeconds(duration);   // ���� �ð�(���⼭�� 3��) ���� ���

        isInvincible = false;                // ���� ���� ����
        currentSpeed = normalSpeed;          // �ӵ��� ������� �ǵ���
    }

    // �ܺο��� ���� ���¸� Ȯ���� �� �ֵ��� ��ȯ
    public bool IsInvincible()
    {
        return isInvincible;
    }

}