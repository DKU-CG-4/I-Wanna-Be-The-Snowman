using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : MonoBehaviour
{
    public float translateSpeed = 1f;  // �������� ���Ʒ� �̵� �ӵ�
    public float translateRange = 0.5f; // �������� �̵��� �ִ� ����
    public float boostSpeed = 10f;     // �÷��̾ ���� �ӵ� ������
    public float boostDuration = 5f;   // �ӵ� ���� ���� �ð�

    private float initialY;            // �������� �ʱ� Y ��ġ

    void Start()
    {
        // �������� �ʱ� Y ��ġ ����
        initialY = transform.position.y;
    }

    void Update()
    {
        // �������� ���Ʒ��� �����̵��� ���� (������ translateRange�� ����)
        float newY = initialY + Mathf.Sin(Time.time * translateSpeed) * translateRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� PlayerBall ������Ʈ�� ������
            PlayerBall player = other.GetComponent<PlayerBall>();

            if (player != null)
            {
                // �ӵ� ���� �ڷ�ƾ ȣ��
                player.StartSpeedBoost(boostSpeed, boostDuration);
            }

            // ������ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
