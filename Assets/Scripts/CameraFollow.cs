using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static bool isExist = false;     // ī�޶� ������ ����� �����ϴ��� ��Ÿ���� Bool ��������
    public static Transform ball;           // ī�޶� ������ ����� ��Ÿ���� Transform ��������

    public bool isEnd = false;              // �������� ������ �κ����� (�Ǵ� ������ ����� �� ���� ��) ��Ÿ���� Bool ����
    Transform viewPoint;                    // ���� ī�޶� ����

    float deltaY;                           // ī�޶�� ���� ������ Y�� �Ÿ�

    private void Start()
    {
        viewPoint = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            isExist = true;
            GetComponent<Collider>().enabled = false;

            // ������ �κ��� �� ���� ���� �� �þ� ����
            if (isEnd) ball = transform;
            else ball = other.transform;
            deltaY = viewPoint.position.y - ball.position.y;

            StartCoroutine(FollowBall());
        }
    }

    /// <summary>
    /// ���� �Ʒ��� �����ϴ� ���� �����մϴ�. ������ �κ��̰ų� ������ �� �̻� ������ �� ���� ��� ������ ����ǰ� ���������� ����۵˴ϴ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator FollowBall() {
        while (!isEnd) {
            // ���� ����� �����Ǿ��� ��� �ٽ� ������ ������ ���
            if (ball == null) {
                isExist = false;
                yield return new WaitUntil(() => { return ball != null; });

                deltaY = viewPoint.position.y - ball.position.y;
                if (deltaY == 0) isEnd = true;
            }
            viewPoint.position = new Vector3(viewPoint.position.x, ball.position.y + deltaY, viewPoint.position.z);
            yield return null;
        }

        // �������� �����
        yield return new WaitForSeconds(5.0f);
        Destroy(Multiplier.parent_Ball.gameObject);
        StageManager._instance.SpawnWalls();
        viewPoint.position = new Vector3(7.0f, 12.0f, 2.61f);
        yield return null;
    }
}
