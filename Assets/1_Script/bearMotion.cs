using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bearMotion : MonoBehaviour
{
    public float detectionDistance = 5f; // PolarBear의 z축 방향으로 감지 거리
    public float chargeSpeed = 5f;      // 돌진 속도

    private bool isCharging = false;   // 돌진 상태 확인
    private Transform playerTransform; // Player의 Transform

    void Start()
    {
        // Tag가 Player인 객체를 찾아 Transform을 가져옴
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player 태그를 가진 객체를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        // Tag가 Bear가 아니면 동작하지 않음
        if (!CompareTag("Bear"))
            return;

        // Player가 없으면 동작하지 않음
        if (playerTransform == null)
            return;

        // PolarBear의 z축 방향과 Player의 z축 방향이 일치하는지 확인
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float zAlignment = Vector3.Dot(transform.forward, directionToPlayer.normalized);

        // z축 방향에 Player가 있고, 일정 거리 안에 들어왔을 경우 돌진 시작
        if (zAlignment > 0.9f && directionToPlayer.magnitude <= detectionDistance && !isCharging)
        {
            isCharging = true; // 돌진 시작 상태로 변경
        }

        // 돌진 중일 때
        if (isCharging)
        {
            // z축 방향으로 이동
            transform.Translate(Vector3.forward * chargeSpeed * Time.deltaTime);
        }
    }
}
