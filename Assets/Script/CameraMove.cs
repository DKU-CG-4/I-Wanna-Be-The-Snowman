using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform; // 플레이어의 Transform을 저장할 변수
    Vector3 Offset;            // 카메라와 플레이어 사이의 거리(오프셋)

    void Awake()
    {
        // 태그가 "Player"인 객체의 Transform을 찾아 playerTransform에 할당
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 현재 카메라 위치와 플레이어 위치의 차이를 Offset으로 계산하여 저장
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // 카메라의 위치를 플레이어의 위치 + Offset으로 설정하여 플레이어를 따라 이동
        transform.position = playerTransform.position + Offset;
    }
}
