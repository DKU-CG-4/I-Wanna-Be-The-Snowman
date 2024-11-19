using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform; // �÷��̾��� Transform�� ������ ����
    Vector3 offset;            // ī�޶�� �÷��̾� ������ �Ÿ�(������)
    public float rotationSpeed = 100f; // ī�޶� ȸ�� �ӵ�

    void Awake()
    {
        // �±װ� "Player"�� ��ü�� Transform�� ã�� playerTransform�� �Ҵ�
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // ���� ī�޶� ��ġ�� �÷��̾� ��ġ�� ���̸� offset���� ����Ͽ� ����
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // ����Ű �Է� �޾ƿ��� (�¿� �̵�)
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            // offset �������� �÷��̾ �߽����� ī�޶� ȸ��
            Quaternion rotation = Quaternion.AngleAxis(horizontalInput * rotationSpeed * Time.deltaTime, Vector3.up);
            offset = rotation * offset; // offset�� ȸ��
        }

        // �÷��̾ ���� �̵��ϸ� offset ����
        transform.position = playerTransform.position + offset;

        // �׻� �÷��̾ �ٶ󺸵��� ����
        transform.LookAt(playerTransform.position);
    }
}

