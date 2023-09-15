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
        // �̵� �ӵ� ����
        navMeshAgent.speed = moveSpeed;
        // ��ǥ���� ���� (��ǥ������ ��� ��� �� �˾Ƽ� �����δ�.)
        navMeshAgent.SetDestination(goal);

        // OnMove() �ڷ�ƾ �޼ҵ尡 ȣ�� ���� ���� ������ ����/����
        StopCoroutine(nameof(OnMove));
        StartCoroutine(nameof(OnMove));
    }

    private IEnumerator OnMove()
    {
        while(true)
        {
            // ��ǥ ������ �����ߴ��� �˻�
            if(Vector3.Distance(navMeshAgent.destination, transform.position) < 0.1f)
            {
                // ��ǥ ��ġ�� �������� �� ó���� �ڵ�
                Debug.Log($"��ǥ ��ġ ���� : {navMeshAgent.destination}");

                break;
            }

            yield return null;
        }
    }
}
