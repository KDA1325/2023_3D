using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goal)
    {
        // 이동 속도 설정
        navMeshAgent.speed = moveSpeed;
        // 목표지점 설정 (목표까지의 경로 계산 후 알아서 움직인다.)
        navMeshAgent.SetDestination(goal);

        // OnMove() 코루틴 메소드가 호출 중일 수도 있으니 중지/시작
        StopCoroutine(nameof(OnMove));
        StartCoroutine(nameof(OnMove));
    }

    private IEnumerator OnMove()
    {
        while(true)
        {
            // 목표 지점에 도달했는지 검사
            if(Vector3.Distance(navMeshAgent.destination, transform.position) < 0.1f)
            {
                // 목표 위치에 도착했을 때 처리할 코드
                Debug.Log($"목표 위치 도착 : {navMeshAgent.destination}");

                break;
            }

            yield return null;
        }
    }
}
