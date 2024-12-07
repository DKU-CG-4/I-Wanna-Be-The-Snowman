using UnityEngine;

public class LastCamera : MonoBehaviour
{
    public float zoomOutSpeed = 5f;        // 카메라 줌 아웃 속도
    public float maxZoomOutDistance = 14f; // 최대 줌 아웃 거리
    private float initialZPosition;        // 초기 Z 위치

    private bool isZoomingOut = true;      // 줌 아웃 상태인지 확인

    void Start()
    {
        // 카메라의 초기 Z축 위치를 저장
        initialZPosition = transform.position.z;
    }

    void Update()
    {
        if (!isZoomingOut) return;

        // 현재 카메라의 Z축 이동 거리 계산
        float currentZoomOutDistance = Mathf.Abs(transform.position.z - initialZPosition);

        if (currentZoomOutDistance < maxZoomOutDistance)
        {
            // 카메라의 Z축을 기준으로 앞으로 이동하여 줌 아웃 (+Z 방향)
            transform.position += Vector3.forward * zoomOutSpeed * Time.deltaTime;
        }
        else
        {
            // 최대 줌 아웃 거리 도달 시 줌 아웃 멈춤
            isZoomingOut = false;
            Debug.Log("줌 아웃 완료");
        }
    }
}
