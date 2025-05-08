using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 7f;
    public float jumpHeightOffset = 0.5f;
    public float slideDuration = 0.5f;      //      임시

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    public float forwardSpeed = 3f;     //      앞으로 나가는 힘

    private bool isGrounded = true;     //      점프를 위한 달리기 bool 값
    private bool isSliding = false;     //      슬라이딩 bool 값
    private bool isPlayer = false;

    public bool Dead = false;     //      죽음 확인

    float deathCooldown = 0f;       //      죽고 나서 시간

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector2 slideColliderSize;
    private Vector2 slideColliderOffset;

    // 🎯 슬라이드 시 스프라이트 위치 조절용
    private Transform spriteTransform;
    private Vector3 originalSpritePosition;
    private Vector3 slideSpriteOffset = new Vector3(0, -0.1f, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = transform.GetComponentInChildren<Animator>();

        // 콜라이더 정보 저장
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;

        slideColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
        slideColliderOffset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y * 0.25f);

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
        if (!isPlayer) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 슬라이드 시작
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            && stateInfo.IsName("Cat_isrunning"))
        {
            animator.ResetTrigger("Jump");
            animator.SetTrigger("Slide");
            isSliding = true;

            boxCollider.size = slideColliderSize;
            boxCollider.offset = slideColliderOffset;

            if (spriteTransform != null)
                spriteTransform.localPosition = originalSpritePosition + slideSpriteOffset;
        }

        // 슬라이드 종료
        if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) && isSliding)
        {
            animator.ResetTrigger("Slide");
            animator.Play("Cat_isrunning");
            isSliding = false;

            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;

            if (spriteTransform != null)
                spriteTransform.localPosition = originalSpritePosition;
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.ResetTrigger("Slide");
            animator.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            transform.position += new Vector3(0, jumpHeightOffset, 0);

            isGrounded = false;

            if (isSliding)
            {
                boxCollider.size = originalColliderSize;
                boxCollider.offset = originalColliderOffset;
                isSliding = false;

                if (spriteTransform != null)
                    spriteTransform.localPosition = originalSpritePosition;
            }

            animator.speed = 1f;
        }

        // 점프 애니메이션 종료 시 멈춤
        if (!isGrounded && stateInfo.IsName("Cat_jump") && stateInfo.normalizedTime >= 1f)
        {
            animator.speed = 0f;
        }

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
