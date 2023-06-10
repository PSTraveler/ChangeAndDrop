using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFullCheck : MonoBehaviour
{
    public static float ball_Limit = 5;         // 블럭이 부서질 공의 개수 (또는 현재 블럭에 도달 가능한 공의 개수)
    int ball_Count = 0;                         // 블럭 위에 쌓인 공의 개수
    Animator anim;                              // 블럭이 깨지는 애니메이션
    Coroutine cor;                              // 블럭이 깨지지 않았을 때 진행되는 재시작 코루틴

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 애니메이션대로 블럭이 깨진다면 재시작 코루틴을 정지하고 블럭 오브젝트 비활성화
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
            // 첫 충돌 시 재시작 코루틴 시작
            if (cor == null)
            {
                cor = StartCoroutine(ResetStage());
            }

            ball_Count++;

            // 공을 배수로 늘려주는 장애물을 아직 만나지 않았을 경우 통과
            if (ball_Limit == 5)
            {
                anim.SetTrigger("1st");
                anim.SetTrigger("2nd");
                anim.SetTrigger("3rd");
            }

            // 애니메이션을 3단계로 나누어 공의 개수가 한계에 다다르는 정도에 따라 진행
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

    // 스테이지 재시작 코루틴
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
