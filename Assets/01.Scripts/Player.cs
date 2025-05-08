using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float forwardSpeed = 3f; // 앞으로 나가는 힘
    public bool isDead = false; // 죽음 확인
    float deathCooldown = 0f; // 죽고 나서 시간

    void Start()
    {
         animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>(); 


        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void FixedUpdate()
    {
        if (isDead)
            return;
        
        Vector3 velocity = _rigidbody.velocity; // 가속도
        velocity.x = forwardSpeed; // 똑같은 속도

        _rigidbody.velocity = velocity;
        
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
	      if (isDead)
            return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
        deathCooldown = 1f;
    }
}
