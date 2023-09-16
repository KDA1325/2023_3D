using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target; // ī�޶� �����ϴ� ���
    [SerializeField]
    private float xAxisSpeed = 250; // ī�޶��� x�� ȸ�� �ӵ�
    [SerializeField]
    private float yAxisSpeed = 500; // ī�޶��� y�� ȸ�� �ӵ�
    [SerializeField]
    private float minDistance = 3; // ī�޶�� target�� �ּ� �Ÿ�
    [SerializeField]
    private float maxDistance = 30; // ī�޶�� target�� �ִ� �Ÿ�
    [SerializeField]
    private float wheelSpeed = 1000; // ���콺 �� ��ũ�� �ӵ�

    private float distance; // ī�޶�� target�� �Ÿ�
    private float xAxisLimitMin = 5; // ī�޶� x�� ȸ�� ���� �ּ� ��
    private float xAxisLimitMax = 80; // ī�޶� x�� ȸ�� ���� �ִ� ��
    private float x, y; // ���콺 �̵� ���� ��
    
    private void Awake()
    {
        // ���� ������ target�� ī�޶��� ��ġ�� �������� distance �� �ʱ�ȭ
        distance = Vector3.Distance(transform.position, target.position);

        // ���� ī�޶��� ȸ�� ���� x, y ������ ����
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    private void Update()
    {
        // ������ ���콺�� ������ ���� ��
        if(Input.GetMouseButton(1))
        {
            UpdateRotate();
        }

        UpdateZoom();
    }

    private void LateUpdate()
    {
        // ������ ���(target)�� ������ return
        if (target == null)
        {
            return;
        }

        // ī�޶��� ��ġ�� ������ ���(�÷��̾�)�� ��ġ + �ڷ� distance��ŭ ������ ��ġ
        transform.position = target.position + transform.rotation * new Vector3(0, 0, -distance);
    }

    private void UpdateRotate()
    {
        // ���콺�� y�� ��ġ ��ȭ�� �������� ī�޶��� x�� ȸ�� �� ����
        x -= Input.GetAxis("Mouse Y") * xAxisSpeed * Time.deltaTime;
        // ���콺�� x�� ��ġ ��ȭ�� �������� ī�޶��� y�� ȸ�� �� ����
        y += Input.GetAxis("Mouse X") * yAxisSpeed * Time.deltaTime;

        // ī�޶��� x�� ȸ���� min ~ max ���̷θ� �����ϵ��� ����
        x = ClampAngle(x, xAxisLimitMin, xAxisLimitMax);

        // ī�޶��� ȸ��(Rotation) ���� ����
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
        // ���콺 �� ��ũ�� ȸ������ target�� ī�޶��� �Ÿ�(distance) ����
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        // min <= distance <= max
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
}
