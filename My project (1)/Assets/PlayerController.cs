using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 움직이는 속도
    private float Speed;

    // 움직임을 저장하는 벡터
    private Vector3 Movement;

    // 플레이어의 animator 구성요소를 받아오기 위해
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //상태체크
    private bool onAttack;
    private bool onHit;
    private bool onJump;
    private bool onClimb;
    private bool onDead;

    // 복제할 총알 원본
    public GameObject BulletPrefab;

    // 복제할 FX 원본
    public GameObject fxPrefab;

    //
    public GameObject[] stageBack = new GameObject[7];

    // 복제된 총알의 저장공간
    public List<GameObject> Bullets = new List<GameObject>();
   
    // 플레이어가 마지막으로 바라본 방향
    private float Direction;


    private void Awake()
    {
        //  player 의 Animator를 받아온다.
        animator = this.GetComponent<Animator>();
        //  player 의 SpriteRenderer를 받아온다.
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    // 유니티 기본 제공 함수
    // 초기값을 설정할 때 사용
    void Start()
    {
        // 속도 초기화
        Speed = 5.0f;
     
        // 초기값 세팅
        onAttack = false;
        onHit = false;
        Direction = 1.0f;

        for (int i = 0; i < 7; ++i)
            stageBack[i] = GameObject.Find(i.ToString());
    }

    // 유니티 기본 제공 함수
    // 프레임 마다 반복적으로 실행되는 함수
    void Update()
    {
        // 실수 연산 IEEE754

        // Input.GetAxis = -1 ~ 1 사이의 값을 반환함
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환

        //Hor이 0이라면 멈춰 있는 상태이므로 예외처리를 해준다
        if (Hor != 0)
            Direction = Hor;

        // 플레이어가 바라보고 있는 방향에 따라 이미지 플립 설정
        if(Direction < 0)
            spriteRenderer.flipX = true; //player
        else if(Direction > 0)
            spriteRenderer.flipX = false;
        spriteRenderer.flipX = (Hor < 0) ? true : false;



        // 입력받은 값으로 플레이어를 움직인다.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            0.0f,
            0.0f);

        // ================= 공격 키 코드
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();
        // ================= 히트 키 코드
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        // ================= 점프 키 코드
        if (Input.GetKey(KeyCode.UpArrow))
            OnJump();
        // ================= 오르기 키 코드
        if (Input.GetKey(KeyCode.LeftAlt))
            OnClimb();
        // ================= 죽음 키 코드
        if (Input.GetKey(KeyCode.Minus))
            OnDead();
        // ================= 총알 키 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 총알 원본을 복제한다
            GameObject Obj = Instantiate(BulletPrefab);

            //복제된 총알의 위치를 현재플레이어의 위치로 초기화
            Obj.transform.position = transform.position;

            // 총알의 BulletController 스크립트를 받아온다
            BulletController Controller = Obj.AddComponent<BulletController>();

            // 총알 스크립트내부의 방향 변수를 현재 플레이어의 방향 변수로 초기화 한다.
            // Controller.Direction = transform.right;
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);
            
            //
             Controller.fxPrefab = fxPrefab;

    // 총알의 SpriteRenderer를 받아온다
    SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();

            // 총알의 이미지 반전 상태를 플레이어의 이미지 반전상태로 설정한다.
            renderer.flipY = spriteRenderer.flipX;


            // 모든 설정이 종료되었다면 저장소에 보관한다.
            Bullets.Add(Obj);   
        }

        // 플레이어의 움직임에 따라 이동 모션을 실행한다.
        animator.SetFloat("Speed", Hor);  //  Hor -> Movement.x 가 더 정확한 표현

        // 실제 플레이어를 움직인다.
        //transform.position += Movement;

        //offset box
        //transform.position += Movement;
    }

    // ================= 공격
    private void OnAttack()
    {
        // 이미 공격모션이 진행중이라면
        if (onAttack)
            // 함수 종료
            return;

        // 함수가 종료되지 않았다면
        // 공격상태를 활성화
        onAttack = true;

        //공격모션을 실행
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // 함수가 실행되면 공격모션이 비활성화
        // 함수는 애니메이션 클립 이벤트 프레임으로 삽입됨
        onAttack = false;
    }

    // ================= 히트
    private void OnHit()
    {
        // 이미 피격모션이 진행중이라면
        if (onHit)
            // 함수 종료
            return;

        // 함수가 종료되지 않았다면
        // 피격상태를 활성화
        onHit = true;

        //피격모션을 실행
        animator.SetTrigger("Hit");
    }
    
    private void SetHit()
    {
        // 함수가 실행되면 피격모션이 비활성화
        // 함수는 애니메이션 클립 이벤트 프레임으로 삽입됨
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