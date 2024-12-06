using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : MonoBehaviour
{
    public float translateSpeed = 1f;  // 아이템의 위아래 이동 속도
    public float translateRange = 0.5f; // 아이템이 이동할 최대 범위
    public float boostSpeed = 10f;     // 플레이어가 얻을 속도 증가량
    public float boostDuration = 5f;   // 속도 증가 지속 시간

    private float initialY;            // 아이템의 초기 Y 위치
    private Transform mainCamera;      // 메인 카메라의 Transform

    void Start()
    {
        // 아이템의 초기 Y 위치 저장
        initialY = transform.position.y;

        // 메인 카메라의 Transform 가져오기
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        // 아이템이 위아래로 움직이도록 설정 (범위는 translateRange로 조정)
        float newY = initialY + Mathf.Sin(Time.time * translateSpeed) * translateRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void LateUpdate()
    {
        // 아이템이 항상 카메라를 바라보도록 회전 조정 (빌보드 효과)
        if (mainCamera != null)
        {
            Vector3 direction = mainCamera.position - transform.position;
            direction.y = 0; // 수직 회전 방지 (카메라의 Y축 무시)
            transform.forward = direction;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerBall 컴포넌트를 가져옴
            PlayerBall player = other.GetComponent<PlayerBall>();

            if (player != null)
            {
                // 속도 증가 코루틴 호출
                player.StartSpeedBoost(boostSpeed, boostDuration);
            }

            // 아이템 비활성화
            gameObject.SetActive(false);
        }
    }
}
