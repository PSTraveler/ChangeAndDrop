using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFullCheck : MonoBehaviour
{
    public static int ball_amount = 5;          // 현재 공이 얼마나 존재하는지(존재해야 하는지) 나타내는 int 전역변수
    public static int check_num = 0;            // 마무리 박스 안에 들어간 공의 개수
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            check_num++;
        }
    }
}
