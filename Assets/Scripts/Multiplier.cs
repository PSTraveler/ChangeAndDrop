using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [Tooltip("�� �������� �����մϴ�.")]
    public GameObject ball;

    public static Transform parent_Ball;    // Ball�� �θ������Ʈ
    bool isBlue = true;                     // �Ķ������� �ƴ����� ��Ÿ���� Bool ����
    int mul = 2;                            // ���� �� �� ������ �� ���ϴ� int ����

    void Awake()
    {
        isBlue = Random.Range(1, 10) % 2 == 0;      // �Ķ��� ���� �����ų��, �������� ���� �����ų�� �������� ����
        mul = Random.Range(2, 4);                   // ���� �� �� ������ �� 2��� 3�� �� �������� ����
        if (!isBlue) {
            GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.5f, 0.0f, 0.5f);
        }
        BoxFullCheck.ball_amount *= mul;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Ball")) {
            parent_Ball = other.transform.parent.parent;
            bool b = other.GetComponent<BallCondition>().isBlue;

            // Ʈ���ſ� �浹�� ���� ��ֹ��� ���� ���� ��
            if (b == isBlue) {
                // ī�޶� ������ ���� �Ʒ��� �ִ� ���� �����Ǿ� ���� ���� ��
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
            // ���� �ٸ� ��
            else {
                Destroy(other.transform.parent.gameObject);
                BoxFullCheck.ball_amount--;

                // ���� ���� ������ �����
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
