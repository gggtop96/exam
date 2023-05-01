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
        // ��ũ��Ʈ�� ������Ʈ�� �ִ��� ���� ������ �ʰ� �����ذ� �ɷ��� �⸣�� ����
        // EnemyManager���� "Enemy"��� �±׸� ����� �浹 ������ ������� ������
        // private ������������ �ҷ��� �� ���� ������ ����� �������� ����
        // �ݴ�� EnemyManager���� PlayerHPBar�� �ҷ��ͼ� �ذ��� �غ����� �ص�
        // ���Ͱ��� ������ �ذ����� �ƴѵ���

        /*if (collision.tag == "Enemy")
        {
            ControllerManager.GetInstance().Player_HP -= 1;
        }
        */


    }
}