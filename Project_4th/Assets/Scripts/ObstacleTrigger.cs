using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // 다른 오브젝트와 충돌하는 순간 1회 호출
    private void OnTriggerEnter(Collider other)
    {
        // 오브젝트의 색상을 임의의 색상으로 설정
        meshRenderer.material.color = Random.ColorHSV();

        Debug.Log($"{other.name} 오브젝트와 충돌 시작");
    }

    // 다른 오브젝트와 충돌하고 있는 동안 매 프레임 호출
    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"{other.name} 오브젝트와 충돌 중...");
    }

    // 다른 오브젝트와의 충돌이 종료되는 순간 1회 호출
    private void OnTriggerExit(Collider other)
    {
        // 오브젝트의 색상을 하얀색으로 설정
        meshRenderer.material.color = Color.white;

        Debug.Log($"{other.name} 오브젝트와 충돌 종료");
    }
}
