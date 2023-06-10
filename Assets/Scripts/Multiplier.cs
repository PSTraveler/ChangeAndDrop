using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [Tooltip("공 프리팹을 지정합니다.")]
    public GameObject ball;

    public static Transform parent_Ball;    // Ball의 부모오브젝트
    bool isBlue = true;                     // 파란색인지 아닌지를 나타내는 Bool 변수
    int mul = 2;                            // 공을 몇 배 생성할 지 정하는 int 변수

    void Awake()
    {
        isBlue = Random.Range(1, 10) % 2 == 0;      // 파란색 공만 통과시킬지, 오렌지색 공만 통과시킬지 랜덤으로 정함
        mul = Random.Range(2, 4);                   // 공을 몇 배 생성할 지 2배와 3배 중 랜덤으로 정함
        if (!isBlue) {
            GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.5f, 0.0f, 0.5f);
        }
        BoxFullCheck.ball_amount *= mul;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Ball")) {
            parent_Ball = other.transform.parent.parent;
            bool b = other.GetComponent<BallCondition>().isBlue;

            // 트리거에 충돌한 공과 장애물의 색이 같을 때
            if (b == isBlue) {
                // 카메라가 추적할 제일 아래에 있는 공이 지정되어 있지 않을 때
                if (!CameraFollow.isExist) {
                    CameraFollow.ball = other.transform;
                    CameraFollow.isExist = true;
                }

                for (int i = 0; i < mul - 1; i++) {
                    BlockFullCheck.ball_Limit++;
                    GameObject g = Instantiate(ball, other.transform.position - Vector3.up * 0.5f, other.transform.rotation, parent_Ball);
                    if (!b)
                        g.GetComponentInChildren<BallCondition>().ChangeColor();
                }
            }
            // 색이 다를 때
            else {
                Destroy(other.transform.parent.gameObject);
                BoxFullCheck.ball_amount--;

                // 공이 전부 터지면 재시작
                if (parent_Ball.childCount == 1)
                {
                    Destroy(parent_Ball.gameObject);
                    StageManager._instance.SpawnWalls();
                    Camera.main.transform.position = new Vector3(7.0f, 12.0f, 2.61f);
                }
            }
        }
    }
}
