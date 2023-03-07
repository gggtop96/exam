using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed;
    private Vector3 Movement;

    public Animator animator;

    private bool onAttack;
    private bool onHit;
    private bool onJump;
    private bool onClimb;
    private bool onDead;

    // 유니티 기본 제공 함수
    // 초기값을 설정할 때 사용

    void Start()
    {
        //속도 초기화
        Speed = 5.0f;

        //  player 의 Animator를 받아온다.
        animator = this.GetComponent<Animator>();

        onAttack = false;
    }

    // 유니티 기본 제공 함수
    // 프레임 마다 반복적으로 실행되는 함수
    void Update()
    {
        // 실수 연산 IEEE754

        // Input.GetAxis = -1 ~ 1 사이의 값을 반환함
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환
        float Ver = Input.GetAxis("Vertical");  // -1 ~ 1까지 실수로 반환

        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);

        // ================= 공격 키 코드
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();
        // ================= 히트 키 코드
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        // ================= 점프 키 코드
        if (Input.GetKey(KeyCode.Space))
            OnJump();
        // ================= 오르기 키 코드
        if (Input.GetKey(KeyCode.LeftAlt))
            OnClimb();
        // ================= 죽음 키 코드
        if (Input.GetKey(KeyCode.Minus))
            OnDead();

        animator.SetFloat("Speed", Hor);  //  Hor -> Movement.x 가 더 정확한 표현
        transform.position += Movement;
    }

    // ================= 공격
    private void OnAttack()
    {
        if (onAttack)
            return;

        onAttack = true;
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        onAttack = false;
    }
    // ================= 히트
    private void OnHit()
    {
        if (onHit)
            return;
    
    onHit = true;
    animator.SetTrigger("Hit");
    }
    
    private void SetHit()
    {
        onHit = false;
    }
    // ================= 점프
    private void OnJump()
    {
        if (onJump)
            return;

        onJump = true;
    animator.SetTrigger("Jump");
    }
    
    private void SetJump()
    {
        onJump = false;
    }
    // ================= 오르기
    private void OnClimb()
    {
        if (onClimb)
            return;

        onClimb = true;
        animator.SetTrigger("Climb");
    }

    private void SetClimb()
    {
        onClimb = false;
    }
    // ================= 죽음
    private void OnDead()
    {
        if (onDead)
            return;

        onClimb = true;
        animator.SetTrigger("Dead");
    }

    private void SetDead()
    {
        onDead = false;
    }
}