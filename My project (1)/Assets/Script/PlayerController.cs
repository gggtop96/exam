using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // ** �����̴� �ӵ�
    private float Speed;

    // ** �������� �����ϴ� ����
    private Vector3 Movement;

    // ** �÷��̾��� Animator ������Ҹ� �޾ƿ�������...
    private Animator animator;

    // ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������...
    private SpriteRenderer playerRenderer;

    // ** [����üũ]
    private bool onAttack; // ���ݻ���
    private bool onHit; // �ǰݻ���

    // ** ������ �Ѿ� ����
    public GameObject BulletPrefab;

    // ** ������ FX ����
    public GameObject fxPrefab;

    public GameObject[] stageBack = new GameObject[7];

    // ** ������ �Ѿ��� �������.
    private List<GameObject> Bullets = new List<GameObject>();

    // ** �÷��̾ ���������� �ٶ� ����.
    private float Direction;


    // ** �÷��̾ �ٶ󺸴� ����
    public bool DirLeft;
    public bool DirRight;

    private void Awake()
    {
        // ** player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        // ** player �� SpriteRenderer�� �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        // [resource] �������� ����� ���ҽ��� ��� �´�.
        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject ;
        
        fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
    }

    // ** ����Ƽ �⺻ ���� �Լ�
    // ** �ʱⰪ�� ������ �� ���
    void Start()
    {
        // ** �ӵ��� �ʱ�ȭ.
        Speed = 5.0f;

        // ** �ʱⰪ ����
        onAttack = false;
        onHit = false;
        Direction = 1.0f;

        DirLeft = false;
        DirRight = false;

        for (int i = 0; i < 7; ++i)
            stageBack[i] = GameObject.Find(i.ToString());
    }

    // ** ����Ƽ �⺻ ���� �Լ�
    // ** �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.

        // ** Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�. 
        if (Hor != 0)
            Direction = Hor;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 0)
                transform.position += Movement;
            else { 
            ControllerManager.GetInstace().DirRight = true;
            ControllerManager.GetInstace().DirLeft = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ControllerManager.GetInstace().DirRight =false;
            ControllerManager.GetInstace().DirLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) )
        {
            ControllerManager.GetInstace().DirRight = false;
            ControllerManager.GetInstace().DirLeft = false;
        }

        // ** �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            0.0f,
            0.0f);

        // ** �÷��̾ �ٶ󺸰��ִ� ���⿡ ���� �̹��� ���� ����.
        if (Direction < 0)
        {
            playerRenderer.flipX = DirLeft = true;
            // ** ���� �÷��̾ �����δ�.
            transform.position += Movement;
        }
        else if (Direction > 0)
        {
            playerRenderer.flipX = false;
            DirRight = true;
        }

        // ** ���� ����ƮŰ�� �Է��Ѵٸ�.....
        if (Input.GetKey(KeyCode.LeftShift))
            // ** �ǰ�
            OnHit();

        // ** �����̽��ٸ� �Է��Ѵٸ�..
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ** ����
            OnAttack();

            // ** �Ѿ˿����� �����Ѵ�.
            GameObject Obj = Instantiate(BulletPrefab);

            // ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            Obj.transform.position = transform.position;

            // ** �Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�.
            BullerController Controller = Obj.AddComponent<BullerController>();

            // ** �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ ���� �Ѵ�.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            // ** �Ѿ� ��ũ��Ʈ������ FX Prefab�� �����Ѵ�.
            Controller.fxPrefab = fxPrefab;

            // ** �Ѿ��� SpriteRenderer�� �޾ƿ´�.
            SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

            // ** �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
            buleltRenderer.flipY = playerRenderer.flipX;

            // ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Bullets.Add(Obj);
        }

        // ** �÷����� �����ӿ� ���� �̵� ����� ���� �Ѵ�.
        animator.SetFloat("Speed", Hor);
    }

    private void OnAttack()
    {
        // ** �̹� ���ݸ���� �������̶��
        if (onAttack)
            // ** �Լ��� �����Ų��.
            return;

        // ** �Լ��� ������� �ʾҴٸ�...
        // ** ���ݻ��¸� Ȱ��ȭ �ϰ�.
        onAttack = true;

        // ** ���ݸ���� ���� ��Ų��.
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // ** �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onAttack = false;
    }

    private void OnHit()
    {
        // ** �̹� �ǰݸ���� �������̶��
        if (onHit)
            // ** �Լ��� �����Ų��.
            return;

        // ** �Լ��� ������� �ʾҴٸ�...
        // ** �ǰݻ��¸� Ȱ��ȭ �ϰ�.
        onHit = true;

        // ** �ǰݸ���� ���� ��Ų��.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // ** �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onHit = false;
    }
}
