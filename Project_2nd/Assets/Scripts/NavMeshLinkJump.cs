using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshLinkJump : MonoBehaviour
{
    [SerializeField]
    private int navMeshArea = 2; // ������ ���� (Jump)
    [SerializeField]
    private float jumpSpeed = 10; // ���� �ӵ�
    [SerializeField]
    private float gravity = -9.81f; // �߷�
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            // IsOnJump() �޼ҵ��� ��ȯ ���� true�� ������ �ݺ� ȣ��
            yield return new WaitUntil(() => IsOnJump());

            // ���� �ൿ
            yield return StartCoroutine(JumpTo());
        }
    }

    private bool IsOnJump()
    {
        // ���� ������Ʈ(navMeshAgent) ��ġ�� NavMeshLink ���� �ִ���?
        if(navMeshAgent.isOnOffMeshLink)
        {
            // ���� ��ġ�� �ִ� NavMeshLink�� (NavMeshLink ������Ʈ�� �����ؼ�) �������� �����߰�,
            // area ������ navMeshArea�� ������ "Jump"�� ������
            if(navMeshAgent.currentOffMeshLinkData.linkType.Equals(OffMeshLinkType.LinkTypeManual) &&
                (navMeshAgent.navMeshOwner as NavMeshLink).area.Equals(navMeshArea))
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator JumpTo()
    {
        // �׺���̼��� �̿��� �ڵ� �̵��� ��� �ߴ�
        navMeshAgent.isStopped = true;

        // ���� ��ġ�� �ִ� NavMeshLink�� ����/���� ��ġ
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        // �پ �̵��ϴ� �ð� ����
        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currentTime = 0;
        float percent = 0;
        // y ������ �ʱ� �ӵ�
        float v0 = (end - start).y - gravity;

        while(percent < 1)
        {
            // �ܼ��� deltaTime�� ���ϸ� ������ 1�� �Ŀ� percent�� 1�� �Ǳ� ������
            // jumpTime �ð��� ���� �Ŀ� percent�� 1�� �ǵ��� ����
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            // �ð� ����� ���� ������Ʈ�� ��ġ(x, z) ����
            Vector3 position = Vector3.Lerp(start, end, percent);

            // �ð� ����� ���� ������Ʈ�� ��ġ(y) ����
            // ������ � : ���� ��ġ + �ʱ� �ӵ� * �ð� + �߷� * �ð� ����
            position.y = start.y + (v0 * percent) + (gravity * percent * percent);

            // ������ ����� x, y, z ��ġ ���� ���� ������Ʈ ��ġ�� ����
            transform.position = position;

            yield return null;
        }

        // NavMeshLink�� �̿��� �̵� �Ϸ�
        navMeshAgent.CompleteOffMeshLink();
        // NavMeshLink �̵��� �Ϸ�Ǿ����� �׺���̼��� �̿��� �ڵ� �̵� �ٽ� ����
        navMeshAgent.isStopped = false;
    }
}
