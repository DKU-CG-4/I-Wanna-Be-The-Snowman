using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform; // 플레이어의 Transform을 저장할 변수
    Vector3 offset;            // 카메라와 플레이어 사이의 거리(오프셋)
    public float rotationSpeed = 100f; // 카메라 회전 속도

    void Awake()
    {
        // 태그가 "Player"인 객체의 Transform을 찾아 playerTransform에 할당
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 현재 카메라 위치와 플레이어 위치의 차이를 offset으로 계산하여 저장
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // 방향키 입력 받아오기 (좌우 이동)
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            // offset 기준으로 플레이어를 중심으로 카메라 회전
            Quaternion rotation = Quaternion.AngleAxis(horizontalInput * rotationSpeed * Time.deltaTime, Vector3.up);
            offset = rotation * offset; // offset을 회전
        }

        // 플레이어를 따라 이동하며 offset 유지
        transform.position = playerTransform.position + offset;

        // 항상 플레이어를 바라보도록 설정
        transform.LookAt(playerTransform.position);
    }
}

