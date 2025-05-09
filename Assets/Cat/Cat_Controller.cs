using UnityEngine;

public class CatController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;
    private bool isPlayer = false;
    private bool isSliding = false;

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

   void Start()
{
    // ✅ 자식 스프라이트 오브젝트 가져오기
    spriteTransform = transform.Find("Cat_OB");  // 자식 이름에 맞게 수정
    if (spriteTransform != null)
    {
        animator = spriteTransform.GetComponent<Animator>(); // 자식에서 Animator 가져오기
        originalSpritePosition = spriteTransform.localPosition;
    }
    else
    {
        Debug.LogWarning("❗ 'Cat_OB' 자식 오브젝트를 찾을 수 없습니다. 이름이 정확한지 확인하세요.");
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
}


    void Update()
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

            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0f);
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
        if (!isGrounded && stateInfo.IsName("Cat_jump") && stateInfo.normalizedTime >= 1f)
        {
            animator.speed = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.speed = 1f;
            animator.Play("Cat_isrunning");
        }
    }
}