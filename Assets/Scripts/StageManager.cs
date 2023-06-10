using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager _instance;       // 외부 스크립트에서 접근할 StageManager 인스턴스
    public int wall_num = 5;                    // 스테이지를 구성할 벽의 총 개수
    public int obstacle_num = 3;                // 그 중 장애물 벽이 차지하는 개수
    public List<GameObject> walls;              // 스테이지를 구성할 벽의 프리팹 리스트
    Vector3 spawnPos = new Vector3(0.0f, 10.0f, 0.0f);      // 벽을 스폰시킬 최상단의 위치
    bool isLeftorRight = false;                 // 좌 우 경사로를 번갈아 스폰시키는 Bool 변수

    void Start()
    {
        _instance = this;
        SpawnWalls();
    }

    /// <summary>
    /// 지정된 조건에 따라 랜덤으로 스테이지를 구성합니다.
    /// </summary>
    public void SpawnWalls()
    {
        // 각 전역변수를 초기화
        BlockFullCheck.ball_Limit = 5;
        BoxSlide.isStart = false;
        BoxFullCheck.ball_amount = 5;
        BoxFullCheck.check_num = 0;

        // 현재까지 만들어진 장애물 벽의 개수
        int obstacle_amount = 0;

        // 스테이지가 구성된 상태에서 함수 실행 시 기존 스테이지 삭제
        if (transform.childCount > 0)
        {
            for (int c = 0; c < transform.childCount; c++)
            {
                Destroy(transform.GetChild(c).gameObject);
            }
        }

        // 스테이지 시작 부분 벽 생성
        Instantiate(walls[0], spawnPos, Quaternion.identity, transform);

        // 랜덤으로 경사로 또는 장애물 생성
        for (int i = 1; i < wall_num - 1; i++)
        {
            int index = Random.Range(1, walls.Count - 1);

            // 경사로가 지정되었을 시에 좌우 번갈아 생성되도록 조정
            if (index == 1 || index == 2)
            {
                // 남은 구성 벽이 전부 장애물 벽이 되어야 할 때
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

            // 장애물이 지정되었을 시에 조건보다 더 많이 생성되지 않도록 조정
            if (index == 3) {
                if (obstacle_amount == obstacle_num) {
                    while (index == 3) {
                        index = Random.Range(1, walls.Count - 1);
                    }
                }
                else 
                    obstacle_amount++;
            }

            // 스테이지 중간 부분 벽 생성
            Instantiate(walls[index], spawnPos - (Vector3.up * 5.0f * i), Quaternion.identity, transform);
        }

        // 스테이지 마무리 부분 벽 생성
        Instantiate(walls[walls.Count - 1], spawnPos - (Vector3.up * 5.0f * (wall_num - 1)), Quaternion.identity, transform);
    }
}
