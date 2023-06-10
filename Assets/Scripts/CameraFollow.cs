using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static bool isExist = false;     // 카메라가 추적할 대상이 존재하는지 나타내는 Bool 전역변수
    public static Transform ball;           // 카메라가 추적할 대상을 나타내는 Transform 전역변수

    public bool isEnd = false;              // 스테이지 마무리 부분인지 (또는 게임이 진행될 수 없을 시) 나타내는 Bool 변수
    Transform viewPoint;                    // 메인 카메라 지정

    float deltaY;                           // 카메라와 추적 대상과의 Y축 거리

    private void Start()
    {
        viewPoint = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            isExist = true;
            GetComponent<Collider>().enabled = false;

            // 마무리 부분일 시 추적 정지 및 시야 고정
            if (isEnd) ball = transform;
            else ball = other.transform;
            deltaY = viewPoint.position.y - ball.position.y;

            StartCoroutine(FollowBall());
        }
    }

    /// <summary>
    /// 가장 아래에 존재하는 공을 추적합니다. 마무리 부분이거나 게임을 더 이상 지속할 수 없을 경우 추적이 종료되고 스테이지가 재시작됩니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator FollowBall() {
        while (!isEnd) {
            // 추적 대상이 삭제되었을 경우 다시 지정될 때까지 대기
            if (ball == null) {
                isExist = false;
                yield return new WaitUntil(() => { return ball != null; });

                deltaY = viewPoint.position.y - ball.position.y;
                if (deltaY == 0) isEnd = true;
            }
            viewPoint.position = new Vector3(viewPoint.position.x, ball.position.y + deltaY, viewPoint.position.z);
            yield return null;
        }

        // 스테이지 재시작
        yield return new WaitForSeconds(5.0f);
        Destroy(Multiplier.parent_Ball.gameObject);
        StageManager._instance.SpawnWalls();
        viewPoint.position = new Vector3(7.0f, 12.0f, 2.61f);
        yield return null;
    }
}
