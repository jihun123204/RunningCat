using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;
    private bool isPlayer = false;
    private bool isSliding = false;
    
    // 플레이어 이동 관련 기준 값
    public float forwardSpeed = 3f;
    public float jumpForce = 7f;
    public float jumpHeightOffset = 0.5f;
    private bool isGrounded = true;

    // 슬라이드용 콜라이더 사이즈 및 오프셋
    private Vector2 originalColliderSize;
    private Vector2 slideColliderSize;
    private Vector2 originalColliderOffset;
    private Vector2 slideColliderOffset;

    // 🎯 슬라이드 시 스프라이트 위치 조절용
    private Transform spriteTransform;
    private Vector3 originalSpritePosition;
    private Vector3 slideSpriteOffset = new Vector3(0, -0.1f, 0);

    // 플레이어 체력 및 초당 틱뎀
    public int maxHealth = 100;
    public int currentHealth;

    public int healthDrainRate = 10;      //      초당 ?씩 체력 까이는 코드
    private float healthTimer = 0f;

    public bool Die = false;

    void Start()
    {
        // ✅ 자식 스프라이트 오브젝트 가져오기
        spriteTransform = transform.Find("Model");  // 자식 이름에 맞게 수정
        if (spriteTransform != null)
        {
            animator = spriteTransform.GetComponent<Animator>(); // 자식에서 Animator 가져오기
            originalSpritePosition = spriteTransform.localPosition;
        }
        else
        {
            Debug.LogWarning("❗ 'Model' 자식 오브젝트를 찾을 수 없습니다. 이름이 정확한지 확인하세요.");
        }

        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (CompareTag("Player"))
            isPlayer = true;

        // 콜라이더 정보 저장
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;

        slideColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
        slideColliderOffset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y * 0.25f);

        // 현재 체력에 최대 체력값으로 초기화
        currentHealth = maxHealth;
    }


    void Update()
    {
        if (!isPlayer) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 슬라이드 시작
        if (Die == true) return;

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) 
            && stateInfo.IsName("Run"))
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
            animator.Play("Run");
            isSliding = false;

            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;

            if (spriteTransform != null)
                spriteTransform.localPosition = originalSpritePosition;
        }

        // 점프
        if (Die == true) return;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.ResetTrigger("Slide");
            animator.SetTrigger("Jump");

            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        if (!isGrounded && stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 1f)
        {
            animator.speed = 0f;
        }

        // 틱뎀 로직, 사망 로직
        if (Die == true) return;

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
    public void FixedUpdate()
    {
        if (Die == true)
            return;

        Vector3 velocity = rb2d.velocity;     //      가속도
        velocity.x = forwardSpeed;      //       똑같은 속도

        rb2d.velocity = velocity;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.speed = 1f;
            animator.Play("Run");
        }
    }

    // 게임 오버 처리
    void Dead()
    {
        Debug.Log("체력 없음");
        animator.SetBool("Dead", true);
        Die = true;
        //  이제 이곳에 캐릭터 사망시 GameOver UI 등을 넣으시면 됩니다
    }
}
