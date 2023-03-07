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

    // ����Ƽ �⺻ ���� �Լ�
    // �ʱⰪ�� ������ �� ���

    void Start()
    {
        //�ӵ� �ʱ�ȭ
        Speed = 5.0f;

        //  player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        onAttack = false;
    }

    // ����Ƽ �⺻ ���� �Լ�
    // ������ ���� �ݺ������� ����Ǵ� �Լ�
    void Update()
    {
        // �Ǽ� ���� IEEE754

        // Input.GetAxis = -1 ~ 1 ������ ���� ��ȯ��
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ
        float Ver = Input.GetAxis("Vertical");  // -1 ~ 1���� �Ǽ��� ��ȯ

        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);

        // ================= ���� Ű �ڵ�
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();
        // ================= ��Ʈ Ű �ڵ�
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        // ================= ���� Ű �ڵ�
        if (Input.GetKey(KeyCode.Space))
            OnJump();
        // ================= ������ Ű �ڵ�
        if (Input.GetKey(KeyCode.LeftAlt))
            OnClimb();
        // ================= ���� Ű �ڵ�
        if (Input.GetKey(KeyCode.Minus))
            OnDead();

        animator.SetFloat("Speed", Hor);  //  Hor -> Movement.x �� �� ��Ȯ�� ǥ��
        transform.position += Movement;
    }

    // ================= ����
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
    // ================= ��Ʈ
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
    // ================= ����
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
    // ================= ������
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
    // ================= ����
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