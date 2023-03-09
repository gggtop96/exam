using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �����̴� �ӵ�
    private float Speed;

    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� animator ������Ҹ� �޾ƿ��� ����
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //����üũ
    private bool onAttack;
    private bool onHit;
    private bool onJump;
    private bool onClimb;
    private bool onDead;

    // ������ �Ѿ� ����
    public GameObject BulletPrefab;

    // ������ FX ����
    public GameObject fxPrefab;

    //
    public GameObject[] stageBack = new GameObject[7];

    // ������ �Ѿ��� �������
    public List<GameObject> Bullets = new List<GameObject>();
   
    // �÷��̾ ���������� �ٶ� ����
    private float Direction;


    private void Awake()
    {
        //  player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();
        //  player �� SpriteRenderer�� �޾ƿ´�.
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    // ����Ƽ �⺻ ���� �Լ�
    // �ʱⰪ�� ������ �� ���
    void Start()
    {
        // �ӵ� �ʱ�ȭ
        Speed = 5.0f;
     
        // �ʱⰪ ����
        onAttack = false;
        onHit = false;
        Direction = 1.0f;

        for (int i = 0; i < 7; ++i)
            stageBack[i] = GameObject.Find(i.ToString());
    }

    // ����Ƽ �⺻ ���� �Լ�
    // ������ ���� �ݺ������� ����Ǵ� �Լ�
    void Update()
    {
        // �Ǽ� ���� IEEE754

        // Input.GetAxis = -1 ~ 1 ������ ���� ��ȯ��
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ

        //Hor�� 0�̶�� ���� �ִ� �����̹Ƿ� ����ó���� ���ش�
        if (Hor != 0)
            Direction = Hor;

        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� �ø� ����
        if(Direction < 0)
            spriteRenderer.flipX = true; //player
        else if(Direction > 0)
            spriteRenderer.flipX = false;
        spriteRenderer.flipX = (Hor < 0) ? true : false;



        // �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            0.0f,
            0.0f);

        // ================= ���� Ű �ڵ�
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();
        // ================= ��Ʈ Ű �ڵ�
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        // ================= ���� Ű �ڵ�
        if (Input.GetKey(KeyCode.UpArrow))
            OnJump();
        // ================= ������ Ű �ڵ�
        if (Input.GetKey(KeyCode.LeftAlt))
            OnClimb();
        // ================= ���� Ű �ڵ�
        if (Input.GetKey(KeyCode.Minus))
            OnDead();
        // ================= �Ѿ� Ű �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �Ѿ� ������ �����Ѵ�
            GameObject Obj = Instantiate(BulletPrefab);

            //������ �Ѿ��� ��ġ�� �����÷��̾��� ��ġ�� �ʱ�ȭ
            Obj.transform.position = transform.position;

            // �Ѿ��� BulletController ��ũ��Ʈ�� �޾ƿ´�
            BulletController Controller = Obj.AddComponent<BulletController>();

            // �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ �ʱ�ȭ �Ѵ�.
            // Controller.Direction = transform.right;
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);
            
            //
             Controller.fxPrefab = fxPrefab;

    // �Ѿ��� SpriteRenderer�� �޾ƿ´�
    SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();

            // �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� �������·� �����Ѵ�.
            renderer.flipY = spriteRenderer.flipX;


            // ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Bullets.Add(Obj);   
        }

        // �÷��̾��� �����ӿ� ���� �̵� ����� �����Ѵ�.
        animator.SetFloat("Speed", Hor);  //  Hor -> Movement.x �� �� ��Ȯ�� ǥ��

        // ���� �÷��̾ �����δ�.
        //transform.position += Movement;

        //offset box
        //transform.position += Movement;
    }

    // ================= ����
    private void OnAttack()
    {
        // �̹� ���ݸ���� �������̶��
        if (onAttack)
            // �Լ� ����
            return;

        // �Լ��� ������� �ʾҴٸ�
        // ���ݻ��¸� Ȱ��ȭ
        onAttack = true;

        //���ݸ���� ����
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ
        // �Լ��� �ִϸ��̼� Ŭ�� �̺�Ʈ ���������� ���Ե�
        onAttack = false;
    }

    // ================= ��Ʈ
    private void OnHit()
    {
        // �̹� �ǰݸ���� �������̶��
        if (onHit)
            // �Լ� ����
            return;

        // �Լ��� ������� �ʾҴٸ�
        // �ǰݻ��¸� Ȱ��ȭ
        onHit = true;

        //�ǰݸ���� ����
        animator.SetTrigger("Hit");
    }
    
    private void SetHit()
    {
        // �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ
        // �Լ��� �ִϸ��̼� Ŭ�� �̺�Ʈ ���������� ���Ե�
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