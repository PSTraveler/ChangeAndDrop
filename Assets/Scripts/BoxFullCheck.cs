using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFullCheck : MonoBehaviour
{
    public static int ball_amount = 5;          // ���� ���� �󸶳� �����ϴ���(�����ؾ� �ϴ���) ��Ÿ���� int ��������
    public static int check_num = 0;            // ������ �ڽ� �ȿ� �� ���� ����
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            check_num++;
        }
    }
}
