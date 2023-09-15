using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshLinkClimb : MonoBehaviour
{
    [SerializeField]
    private int navMeshArea = 3; // ������ ����(Climb)
    [SerializeField]
    private float climbSpeed = 1.5f; // ���������� �̵� �ӵ�
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            // IsOnClimb() �޼ҵ��� ��ȯ ���� true�� ������ �ݺ� ȣ��
            yield return new WaitUntil(() => IsOnClimb());

            // �ö󰡰ų� �������� �ൿ (����� �ܼ��� Start <-> end ��ġ�� �̵�
            yield return StartCoroutine(ClimbOrDescend());
        }
    }

    private bool IsOnClimb()
    {
        // ���� ������Ʈ(navMeshAgent) ��ġ�� NavMeshLink ���� �ִ���?
        if(navMeshAgent.isOnOffMeshLink)
        {
            // ���� ��ġ�� �ִ� NavMeshLink�� (NavMeshLink ������Ʈ�� �����ؼ�) �������� �����߰�,
            // area ������ navMeshArea�� ������ "Climb"�� ������
            if(navMeshAgent.currentOffMeshLinkData.linkType.Equals(OffMeshLinkType.LinkTypeManual) && 
                (navMeshAgent.navMeshOwner as NavMeshLink).area.Equals(navMeshArea))
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator ClimbOrDescend()
    {
        // �׺���̼��� �̿��� �ڵ� �̵��� ��� �ߴ�
        navMeshAgent.isStopped = true;

        // ���� ��ġ�� �ִ� NavMeshLink�� ����/���� ��ġ
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        // ���������� �̵� �ð� ����
        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed;
        float currentTime = 0;
        float percent = 0;

        while(percent < 1)
        {
            // �ܼ��� deltaTime�� ���ϸ� ������ 1�� �Ŀ� percent�� 1�� �Ǳ� ������
            // climbTime �ð��� ���� �Ŀ� percent�� 1�� �ǵ��� ����
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;

            // �ð� ����� ���� ������Ʈ�� ��ġ ����
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        // NavMeshLink�� �̿��� �̵� �Ϸ�
        navMeshAgent.CompleteOffMeshLink();
        // NavMeshLink �̵��� �Ϸ�Ǿ����� �׺���̼��� �̿��� �ڵ� �̵� �ٽ� ����
        navMeshAgent.isStopped = false;
    }
}
