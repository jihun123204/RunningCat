using UnityEngine;
<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
=======
>>>>>>> origin/Khs_Branch

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;
    private bool isPlayer = false;
    private bool isSliding = false;
<<<<<<< HEAD


    
    // 플레이어 이동 관련 기준 값
    public float normalSpeed = 3f;
    public float jumpForce = 7f;
    public float jumpHeightOffset = 0.5f;
    private bool isGrounded = true;
    public float boostedSpeed = 15f;   // 츄르를 먹었을 때 증가하는 속도
    private float currentSpeed;       // 현재 속도를 추적
    public float timetospeedUp = 10f; //  몇초마다 속도 증가할건지
    public float speedUpAmount = 1f; // 몇만큼 속도 증가할건지
    public Transform groundCheck;         // 바닥 체크를 위한 트랜스폼
    public LayerMask groundLayer;        // 바닥 레이어
    public int maxJumpCount = 2;         // 최대 점프 횟수
    private int jumpCount;             // 현재 점프 횟수
=======
    
    // 플레이어 이동 관련 기준 값
    public float forwardSpeed = 3f;
    public float jumpForce = 7f;
    public float jumpHeightOffset = 0.5f;
    private bool isGrounded = true;
>>>>>>> origin/Khs_Branch

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
<<<<<<< HEAD
    private bool isInvincible = false;  // 무적 상태 여부
    public float isInvincibleTime = 3f; // 무적 상태 지속 시간
    public int maxHealth = 100;
    public int currentHealth;

    public Slider hpSlider;  // 체력바 UI

=======
    public int maxHealth = 100;
    public int currentHealth;

>>>>>>> origin/Khs_Branch
    public int healthDrainRate = 10;      //      초당 ?씩 체력 까이는 코드
    private float healthTimer = 0f;

    public bool Die = false;

<<<<<<< HEAD
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();    // Rigidbody2D 컴포넌트를 가져와서 변수에 저장
        currentSpeed = normalSpeed;          // 초기 속도는 기본 속도
    }

    void Start()
    {
        // ✅ 자식 스프라이트 오브젝트 가져오기
        spriteTransform = transform.Find("Cat_OB");  // 자식 이름에 맞게 수정
=======
    void Start()
    {
        // ✅ 자식 스프라이트 오브젝트 가져오기
        spriteTransform = transform.Find("Model");  // 자식 이름에 맞게 수정
>>>>>>> origin/Khs_Branch
        if (spriteTransform != null)
        {
            animator = spriteTransform.GetComponent<Animator>(); // 자식에서 Animator 가져오기
            originalSpritePosition = spriteTransform.localPosition;
        }
        else
        {
<<<<<<< HEAD
            Debug.LogWarning("❗ 'Cat_OB' 자식 오브젝트를 찾을 수 없습니다. 이름이 정확한지 확인하세요.");
=======
            Debug.LogWarning("❗ 'Model' 자식 오브젝트를 찾을 수 없습니다. 이름이 정확한지 확인하세요.");
>>>>>>> origin/Khs_Branch
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
<<<<<<< HEAD
        // UIHP 체력표시 업데이트
        UpdateHPUI();

        // 속도 증가 코루틴 시작   
        StartCoroutine(SpeedUpOverTime());
=======
>>>>>>> origin/Khs_Branch
    }


    void Update()
    {
        if (!isPlayer) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 슬라이드 시작
        if (Die == true) return;

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) 
<<<<<<< HEAD
            && stateInfo.IsName("Cat_isrunning"))
=======
            && stateInfo.IsName("Run"))
>>>>>>> origin/Khs_Branch
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
<<<<<<< HEAD
            animator.Play("Cat_isrunning");
=======
            animator.Play("Run");
>>>>>>> origin/Khs_Branch
            isSliding = false;

            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;

            if (spriteTransform != null)
                spriteTransform.localPosition = originalSpritePosition;
        }

        // 점프
        if (Die == true) return;
<<<<<<< HEAD
                                            // isGround 대신 넣은 조건 코드 <두번 이상 점프할 수 없다>
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
=======

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
>>>>>>> origin/Khs_Branch
        {
            animator.ResetTrigger("Slide");
            animator.SetTrigger("Jump");

<<<<<<< HEAD
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
=======
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            transform.position += new Vector3(0, jumpHeightOffset, 0);
>>>>>>> origin/Khs_Branch

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
<<<<<<< HEAD
            UpdateHPUI();
=======
>>>>>>> origin/Khs_Branch

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
<<<<<<< HEAD
        velocity.x = currentSpeed;      //       똑같은 속도
=======
        velocity.x = forwardSpeed;      //       똑같은 속도
>>>>>>> origin/Khs_Branch

        rb2d.velocity = velocity;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.speed = 1f;
<<<<<<< HEAD
            animator.ResetTrigger("Jump");      //      착지 시 달리기 애니메이션이 씹히지 않게 하기 위해
            animator.Play("Cat_isrunning");
            jumpCount = 0;      //      바닥에 착지할 때 마다 "점프 횟수 초기화" 로 2단점프 지속 가능
        }
    }

=======
            animator.Play("Run");
        }
    }

<<<<<<< Updated upstream
=======
>>>>>>> origin/Khs_Branch

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Obstacle>() != null && !isInvincible)  // 장애물 컴포넌트를 가진 오브젝트와 충돌하고 무적상태가 아닐때
        {
            Player health = GetComponent<Player>();
            if (health != null)
            {
<<<<<<< HEAD
=======
                animator.SetTrigger("Hit"); // 맞는 애니메이션 트리거


>>>>>>> origin/Khs_Branch
                health.TakeDamage(10); // 체력 감소 수치는 조절 가능
                StartCoroutine(ObstacleCoroutine()); // 무적 상태 코루틴 시작
            }
        }
    }

    private IEnumerator ObstacleCoroutine()
    {
        isInvincible = true; // 무적 상태로 설정
        yield return new WaitForSeconds(isInvincibleTime); // 일정 시간(여기서는 3초) 동안 대기
        isInvincible = false; // 무적 상태 해제
    }


    // 츄르를 먹었을 때 실행되는 함수
    public void ActivateChuruBuff(float duration)
    {
        StartCoroutine(ChuruBuffCoroutine(duration));   // 코루틴을 통해 일정 시간 동안 버프 효과를 주기
    }

    // 코루틴을 사용하여 츄르 버프 적용
    private IEnumerator ChuruBuffCoroutine(float duration)
    {
        isInvincible = true;                 // 무적 상태로 설정
        currentSpeed = boostedSpeed;         // 속도를 증가시킴

        yield return new WaitForSeconds(duration);   // 일정 시간(여기서는 3초) 동안 대기

        isInvincible = false;                // 무적 상태 해제
        currentSpeed = normalSpeed;          // 속도를 원래대로 되돌림
    }

    // 외부에서 무적 상태를 확인할 수 있도록 반환
    public bool IsInvincible()
    {
        return isInvincible;
    }

    //체력감소 함수
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHPUI();
    }

    //체력회복 함수
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHPUI();
    }

    // 체력 UI 업데이트 함수
    private void UpdateHPUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }
    }

    // 속도 증가 코루틴
    IEnumerator SpeedUpOverTime()
    {
        while (true) // 무한 루프
        {
            yield return new WaitForSeconds(timetospeedUp);  //10초 기다림
            normalSpeed += speedUpAmount; // 속도 증가

            if (!isInvincible) // 무적 상태가 아닐 때만 속도 증가
            {
                currentSpeed = normalSpeed; // 현재 속도를 기본 속도로 설정
            }
        }
    }




<<<<<<< HEAD
=======
>>>>>>> Stashed changes
>>>>>>> origin/Khs_Branch
    // 게임 오버 처리
    void Dead()
    {
        Debug.Log("체력 없음");
        animator.SetBool("Dead", true);
        Die = true;
        //  이제 이곳에 캐릭터 사망시 GameOver UI 등을 넣으시면 됩니다
    }
}
