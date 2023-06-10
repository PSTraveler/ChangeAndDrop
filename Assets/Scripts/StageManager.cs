using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager _instance;       // �ܺ� ��ũ��Ʈ���� ������ StageManager �ν��Ͻ�
    public int wall_num = 5;                    // ���������� ������ ���� �� ����
    public int obstacle_num = 3;                // �� �� ��ֹ� ���� �����ϴ� ����
    public List<GameObject> walls;              // ���������� ������ ���� ������ ����Ʈ
    Vector3 spawnPos = new Vector3(0.0f, 10.0f, 0.0f);      // ���� ������ų �ֻ���� ��ġ
    bool isLeftorRight = false;                 // �� �� ���θ� ������ ������Ű�� Bool ����

    void Start()
    {
        _instance = this;
        SpawnWalls();
    }

    /// <summary>
    /// ������ ���ǿ� ���� �������� ���������� �����մϴ�.
    /// </summary>
    public void SpawnWalls()
    {
        // �� ���������� �ʱ�ȭ
        BlockFullCheck.ball_Limit = 5;
        BoxSlide.isStart = false;
        BoxFullCheck.ball_amount = 5;
        BoxFullCheck.check_num = 0;

        // ������� ������� ��ֹ� ���� ����
        int obstacle_amount = 0;

        // ���������� ������ ���¿��� �Լ� ���� �� ���� �������� ����
        if (transform.childCount > 0)
        {
            for (int c = 0; c < transform.childCount; c++)
            {
                Destroy(transform.GetChild(c).gameObject);
            }
        }

        // �������� ���� �κ� �� ����
        Instantiate(walls[0], spawnPos, Quaternion.identity, transform);

        // �������� ���� �Ǵ� ��ֹ� ����
        for (int i = 1; i < wall_num - 1; i++)
        {
            int index = Random.Range(1, walls.Count - 1);

            // ���ΰ� �����Ǿ��� �ÿ� �¿� ������ �����ǵ��� ����
            if (index == 1 || index == 2)
            {
                // ���� ���� ���� ���� ��ֹ� ���� �Ǿ�� �� ��
                if (i >= wall_num - obstacle_num - 1) {
                    index = 3;
                }
                else if (isLeftorRight)
                {
                    index = 2;
                    isLeftorRight = false;
                }
                else
                {
                    index = 1;
                    isLeftorRight = true;
                }
            }

            if (i >= wall_num - obstacle_num - 1)
            {
                index = 3;
            }

            // ��ֹ��� �����Ǿ��� �ÿ� ���Ǻ��� �� ���� �������� �ʵ��� ����
            if (index == 3) {
                if (obstacle_amount == obstacle_num) {
                    while (index == 3) {
                        index = Random.Range(1, walls.Count - 1);
                    }
                }
                else 
                    obstacle_amount++;
            }

            // �������� �߰� �κ� �� ����
            Instantiate(walls[index], spawnPos - (Vector3.up * 5.0f * i), Quaternion.identity, transform);
        }

        // �������� ������ �κ� �� ����
        Instantiate(walls[walls.Count - 1], spawnPos - (Vector3.up * 5.0f * (wall_num - 1)), Quaternion.identity, transform);
    }
}
