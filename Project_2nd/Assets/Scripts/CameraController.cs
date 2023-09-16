using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target; // 카메라가 추적하는 대상
    [SerializeField]
    private float xAxisSpeed = 250; // 카메라의 x축 회전 속도
    [SerializeField]
    private float yAxisSpeed = 500; // 카메라의 y축 회전 속도
    [SerializeField]
    private float minDistance = 3; // 카메라와 target의 최소 거리
    [SerializeField]
    private float maxDistance = 30; // 카메라와 target의 최대 거리
    [SerializeField]
    private float wheelSpeed = 1000; // 마우스 휠 스크롤 속도

    private float distance; // 카메라와 target의 거리
    private float xAxisLimitMin = 5; // 카메라 x축 회전 제한 최소 값
    private float xAxisLimitMax = 80; // 카메라 x축 회전 제한 최대 값
    private float x, y; // 마우스 이동 방향 값
    
    private void Awake()
    {
        // 최초 설정된 target과 카메라의 위치를 바탕으로 distance 값 초기화
        distance = Vector3.Distance(transform.position, target.position);

        // 최초 카메라의 회전 값을 x, y 변수에 저장
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    private void Update()
    {
        // 오른쪽 마우스를 누르고 있을 때
        if(Input.GetMouseButton(1))
        {
            UpdateRotate();
        }

        UpdateZoom();
    }

    private void LateUpdate()
    {
        // 추적할 대상(target)이 없으면 return
        if (target == null)
        {
            return;
        }

        // 카메라의 위치는 추적할 대상(플레이어)의 위치 + 뒤로 distance만큼 떨어진 위치
        transform.position = target.position + transform.rotation * new Vector3(0, 0, -distance);
    }

    private void UpdateRotate()
    {
        // 마우스의 y축 위치 변화를 바탕으로 카메라의 x축 회전 값 설정
        x -= Input.GetAxis("Mouse Y") * xAxisSpeed * Time.deltaTime;
        // 마우스의 x축 위치 변화를 바탕으로 카메라의 y축 회전 값 설정
        y += Input.GetAxis("Mouse X") * yAxisSpeed * Time.deltaTime;

        // 카메라의 x축 회전은 min ~ max 사이로만 가능하도록 설정
        x = ClampAngle(x, xAxisLimitMin, xAxisLimitMax);

        // 카메라의 회전(Rotation) 정보 갱신
        transform.rotation = Quaternion.Euler(x, y, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    private void UpdateZoom()
    {
        // 마우스 휠 스크롤 회전으로 target과 카메라의 거리(distance) 조절
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        // min <= distance <= max
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
}
