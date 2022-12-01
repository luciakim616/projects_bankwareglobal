using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    //움직임의 목표지점
    public Vector3 target;

    //움직임 속도 
    public float speed;

    void Update()
    {
        //타겟으로부터 거리 구하기
        float distance = Vector3.Distance(transform.position, target);

        //거리와 현재 위치가 일치하지 않으면 카메라가 움직이도록 로직 설정
        if (distance > 0)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target, step);

        }
       


    }
}
