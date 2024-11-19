using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : MonoBehaviour
{
    public float translateSpeed = 1f; // 아이템의 위아래 이동 속도
    public float boostSpeed = 10f;    // 플레이어가 얻을 속도 증가량
    public float boostDuration = 5f; // 속도 증가 지속 시간

    private float initialY;           // 아이템의 초기 Y 위치

    void Start()
    {
        // 아이템의 초기 Y 위치 저장
        initialY = transform.position.y;
    }

    void Update()
    {
        // 아이템이 위아래로 움직이도록 설정
        float newY = initialY + Mathf.Sin(Time.time * translateSpeed) * 0.5f; // Sin 함수를 이용해 자연스럽게 위아래로 이동
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
