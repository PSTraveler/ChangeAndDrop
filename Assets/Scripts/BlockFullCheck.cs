using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFullCheck : MonoBehaviour
{
    public static float ball_Limit = 5;         // ���� �μ��� ���� ���� (�Ǵ� ���� ���� ���� ������ ���� ����)
    int ball_Count = 0;                         // �� ���� ���� ���� ����
    Animator anim;                              // ���� ������ �ִϸ��̼�
    Coroutine cor;                              // ���� ������ �ʾ��� �� ����Ǵ� ����� �ڷ�ƾ

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �ִϸ��̼Ǵ�� ���� �����ٸ� ����� �ڷ�ƾ�� �����ϰ� �� ������Ʈ ��Ȱ��ȭ
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // ù �浹 �� ����� �ڷ�ƾ ����
            if (cor == null)
            {
                cor = StartCoroutine(ResetStage());
            }

            ball_Count++;

            // ���� ����� �÷��ִ� ��ֹ��� ���� ������ �ʾ��� ��� ���
            if (ball_Limit == 5)
            {
                anim.SetTrigger("1st");
                anim.SetTrigger("2nd");
                anim.SetTrigger("3rd");
            }

            // �ִϸ��̼��� 3�ܰ�� ������ ���� ������ �Ѱ迡 �ٴٸ��� ������ ���� ����
            if (ball_Count >= ball_Limit / 3.0f)
            {
                if (ball_Count >= ball_Limit * 2.0f / 3.0f)
                {
                    if (ball_Count >= ball_Limit)
                    {
                        anim.SetTrigger("3rd");
                    }
                    else anim.SetTrigger("2nd");
                }
                else anim.SetTrigger("1st");
            }
        }
    }

    // �������� ����� �ڷ�ƾ
    IEnumerator ResetStage()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(Multiplier.parent_Ball.gameObject);
        StageManager._instance.SpawnWalls();
        Camera.main.transform.position = new Vector3(7.0f, 12.0f, 2.61f);
        StopAllCoroutines();
        yield return null;
    }
}
