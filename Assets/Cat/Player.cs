using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;
    private bool isPlayer = false;
    private bool isSliding = false;


    
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
    private bool isInvincible = false;  // 무적 상태 여부
    public float isInvincibleTime = 3f; // 무적 상태 지속 시간
    public int maxHealth = 100;
    public int currentHealth;

    [Header("부스트 이펙트")]
    public GameObject boostEffectPrefab;
    private GameObject boostEffectInstance;

    [Header("질주 잔상 이펙트")]
    public GameObject boostTrailPrefab;
    private GameObject boostTrailInstance;



    public Slider hpSlider;  // 체력바 UI

    public int healthDrainRate = 10;      //      초당 ?씩 체력 까이는 코드
    private float healthTimer = 0f;

    public bool Die = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();    // Rigidbody2D 컴포넌트를 가져와서 변수에 저장
        currentSpeed = normalSpeed;          // 초기 속도는 기본 속도
    }

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

        // 현재 체력에 최대 체력값으로 초기화
        currentHealth = maxHealth;
        // UIHP 체력표시 업데이트
        UpdateHPUI();

        // 속도 증가 코루틴 시작   
        StartCoroutine(SpeedUpOverTime());
    }


    void Update()
    {
        if (!isPlayer) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 슬라이드 시작
        if (Die == true) return;

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

            // ✅ 점프 사운드 재생
            if (SoundManager.Instance != null && SoundManager.Instance.SlideSFX != null)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.SlideSFX);
            }

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
        if (Die == true) return;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            animator.ResetTrigger("Slide");
            animator.SetTrigger("Jump");

            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;

            isGrounded = false;

            // ✅ 슬라이딩 상태 강제 해제
            if (isSliding)
            {
                isSliding = false;
                boxCollider.size = originalColliderSize;
                boxCollider.offset = originalColliderOffset;

                if (spriteTransform != null)
                    spriteTransform.localPosition = originalSpritePosition;
            }

            // ✅ 점프 사운드 재생
            if (SoundManager.Instance != null && SoundManager.Instance.jumpSFX != null)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.jumpSFX);
            }


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
            UpdateHPUI();

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
        velocity.x = currentSpeed;      //       똑같은 속도

        rb2d.velocity = velocity;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.speed = 1f;

            animator.ResetTrigger("Jump");
            animator.ResetTrigger("Slide");
            animator.Play("Cat_isrunning");

            jumpCount = 0;      //      바닥에 착지할 때 마다 "점프 횟수 초기화" 로 2단점프 지속 가능

            isSliding = false;

            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
            if (spriteTransform != null)
                spriteTransform.localPosition = originalSpritePosition;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Obstacle>() != null && !isInvincible)  // 장애물 컴포넌트를 가진 오브젝트와 충돌하고 무적상태가 아닐때
        {
            Player health = GetComponent<Player>();
            if (health != null)
            {
                animator.SetTrigger("Hit");
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


    public void ActivateChuruBuff(float duration)
    {
        StartCoroutine(ChuruBuffCoroutine(duration));
    }

    private IEnumerator ChuruBuffCoroutine(float duration)
    {
        isInvincible = true;
        currentSpeed = boostedSpeed;

        if (animator != null)
        {
            animator.SetBool("IsBoosting", true);
        }

        // ✅ 부스트 이펙트 (불꽃)
        if (boostEffectPrefab != null && boostEffectInstance == null)
        {
            boostEffectInstance = Instantiate(boostEffectPrefab, transform.position, Quaternion.identity);
            boostEffectInstance.transform.SetParent(transform);
            boostEffectInstance.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
        }

        // ✅ 질주 잔상 TrailRenderer 프리팹 생성
        if (boostTrailPrefab != null && boostTrailInstance == null)
        {
            boostTrailInstance = Instantiate(boostTrailPrefab, transform.position, Quaternion.identity);
            boostTrailInstance.transform.SetParent(transform);
            boostTrailInstance.transform.localPosition = Vector3.zero;
        }

        StartCoroutine(DashFlashEffect());

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        currentSpeed = normalSpeed;

        if (animator != null)
        {
            animator.SetBool("IsBoosting", false);
        }

        // ✅ 이펙트 제거
        if (boostEffectInstance != null)
        {
            Destroy(boostEffectInstance);
            boostEffectInstance = null;
        }

        // ✅ TrailRenderer 제거
        if (boostTrailInstance != null)
        {
            Destroy(boostTrailInstance);
            boostTrailInstance = null;
        }
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

 private IEnumerator DashFlashEffect()
    {
        if (spriteTransform == null)
            yield break;

        SpriteRenderer sr = spriteTransform.GetComponent<SpriteRenderer>();
        if (sr == null)
            yield break;

        float flashInterval = 0.1f;    // 반짝이는 간격
        float elapsed = 0f;

        while (elapsed < isInvincibleTime)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f); // 반투명하게
            yield return new WaitForSeconds(flashInterval);

            sr.color = new Color(1f, 1f, 1f, 1f);   // 원래 색상
            yield return new WaitForSeconds(flashInterval);

            elapsed += flashInterval * 2;
        }

        sr.color = new Color(1f, 1f, 1f, 1f); // 효과 끝나면 정상 복원
    }



    void Dead()
    {
        Debug.Log("체력 없음");
        animator.SetBool("Dead", true);
        Die = true;

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1f); // 사망 애니메이션 대기
        if (FadeManager.Instance != null)
            FadeManager.Instance.FadeToScene("GameOver");
        else
            SceneManager.LoadScene("GameOver"); // 예외 처리
    }

}
