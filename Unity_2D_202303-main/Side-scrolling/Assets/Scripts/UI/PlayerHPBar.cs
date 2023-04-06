using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{

    private Slider HPBar;
    private Animator Anim;

    private void Awake()
    {
        HPBar = GetComponent<Slider>();
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        HPBar.maxValue = ControllerManager.GetInstance().Player_HP;
        HPBar.value = HPBar.maxValue;
        HPBar.value = 3;
    }

    private void Update()
    {
     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (Input.GetMouseButton(0))
        {
            ControllerManager.GetInstance().Player_HP -= 1;
        }

        if (Input.GetMouseButton(1))
        {
            ControllerManager.GetInstance().Player_HP += 1;
        }


        HPBar.value = ControllerManager.GetInstance().Player_HP;

        if (ControllerManager.GetInstance().Player_HP <= 0)
        {

        }
        // 스크립트나 오브젝트를 최대한 새로 만들지 않고 문제해결 능력을 기르기 위해
        // EnemyManager에서 "Enemy"라는 태그를 만들어 충돌 판정을 만들려고 했으나
        // private 성질때문인지 불러올 수 없어 마땅한 방법이 생각나지 않음
        // 반대로 EnemyManager에서 PlayerHPBar를 불러와서 해결을 해볼려고 해도
        // 위와같은 이유로 해결방법이 아닌듯함

        /*if (collision.tag == "Enemy")
        {
            ControllerManager.GetInstance().Player_HP -= 1;
        }
        */


    }
}