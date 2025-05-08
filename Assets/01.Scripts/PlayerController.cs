using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 12f;
    public float slideDuration = 0.5f;      //      임시

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    public float forwardSpeed = 3f;     //      앞으로 나가는 힘

    private bool isGrounded = true;     //      점프를 위한 달리기 bool 값
    private bool isSliding = false;     //      슬라이딩 bool 값

    public bool Dead = false;     //      죽음 확인

    float deathCooldown = 0f;       //      죽고 나서 시간

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = transform.GetComponentInChildren<Animator>();

        // 원본 콜라이더 정보 저장
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;

        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (rb == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }

        animator.SetBool("isGrounded", isGrounded);     //      애니메이션을 위해 땅에 붙어있는지 확인
    }

    public void FixedUpdate()
    {
        if (Dead)
            return;

        Vector3 velocity = rb.velocity;     //      가속도
        velocity.x = forwardSpeed;      //       똑같은 속도

        rb.velocity = velocity;

        float angle = Mathf.Clamp((rb.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        animator.SetTrigger("Jump");
    }

    IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        
        // 콜라이더 사이즈를 줄여 몸을 낮추는 효과를 준다
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y * 0.5f);
        boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y - 0.25f);

        yield return new WaitForSeconds(slideDuration);

        // 콜라이더 사이즈 복구함으로 다시 일어서는 효과
        boxCollider.size = originalColliderSize;
        boxCollider.offset = originalColliderOffset;
        animator.SetBool("isSlidng", false);
       

        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Dead)
            return;

        animator.SetInteger("Dead", 1);
        Dead = true;
        deathCooldown = 1f;

        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
