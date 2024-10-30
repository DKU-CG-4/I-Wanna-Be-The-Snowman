using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform; // �÷��̾��� Transform�� ������ ����
    Vector3 Offset;            // ī�޶�� �÷��̾� ������ �Ÿ�(������)

    void Awake()
    {
        // �±װ� "Player"�� ��ü�� Transform�� ã�� playerTransform�� �Ҵ�
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // ���� ī�޶� ��ġ�� �÷��̾� ��ġ�� ���̸� Offset���� ����Ͽ� ����
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // ī�޶��� ��ġ�� �÷��̾��� ��ġ + Offset���� �����Ͽ� �÷��̾ ���� �̵�
        transform.position = playerTransform.position + Offset;
    }
}
