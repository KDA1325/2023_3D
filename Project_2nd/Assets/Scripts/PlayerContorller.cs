using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    private Camera mainCamera;
    private Movement3D movement;

    private void Awake()
    {
        mainCamera = Camera.main;
        movement = GetComponent<Movement3D>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // ���콺�� Ŭ���� ����(hit.point)�� ��ǥ ��ġ�� ����
                movement.MoveTo(hit.point);
            }
        }
    }
}
