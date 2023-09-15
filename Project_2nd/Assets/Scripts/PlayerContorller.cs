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
                // 마우스가 클릭한 지점(hit.point)을 목표 위치로 설정
                movement.MoveTo(hit.point);
            }
        }
    }
}
