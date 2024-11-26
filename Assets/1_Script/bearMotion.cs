using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bearMotion : MonoBehaviour
{
    public float detectionDistance = 5f; // PolarBear�� z�� �������� ���� �Ÿ�
    public float chargeSpeed = 5f;      // ���� �ӵ�

    private bool isCharging = false;   // ���� ���� Ȯ��
    private Transform playerTransform; // Player�� Transform

    void Start()
    {
        // Tag�� Player�� ��ü�� ã�� Transform�� ������
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player �±׸� ���� ��ü�� ã�� �� �����ϴ�!");
        }
    }

    void Update()
    {
        // Tag�� Bear�� �ƴϸ� �������� ����
        if (!CompareTag("Bear"))
            return;

        // Player�� ������ �������� ����
        if (playerTransform == null)
            return;

        // PolarBear�� z�� ����� Player�� z�� ������ ��ġ�ϴ��� Ȯ��
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float zAlignment = Vector3.Dot(transform.forward, directionToPlayer.normalized);

        // z�� ���⿡ Player�� �ְ�, ���� �Ÿ� �ȿ� ������ ��� ���� ����
        if (zAlignment > 0.9f && directionToPlayer.magnitude <= detectionDistance && !isCharging)
        {
            isCharging = true; // ���� ���� ���·� ����
        }

        // ���� ���� ��
        if (isCharging)
        {
            // z�� �������� �̵�
            transform.Translate(Vector3.forward * chargeSpeed * Time.deltaTime);
        }
    }
}
