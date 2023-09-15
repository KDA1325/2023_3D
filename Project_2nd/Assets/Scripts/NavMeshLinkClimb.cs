using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshLinkClimb : MonoBehaviour
{
    [SerializeField]
    private int navMeshArea = 3; // 적용할 구역(Climb)
    [SerializeField]
    private float climbSpeed = 1.5f; // 오르내리는 이동 속도
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            // IsOnClimb() 메소드의 반환 값이 true일 때까지 반복 호출
            yield return new WaitUntil(() => IsOnClimb());

            // 올라가거나 내려오는 행동 (현재는 단순히 Start <-> end 위치로 이동
            yield return StartCoroutine(ClimbOrDescend());
        }
    }

    private bool IsOnClimb()
    {
        // 현재 에이전트(navMeshAgent) 위치가 NavMeshLink 위에 있는지?
        if(navMeshAgent.isOnOffMeshLink)
        {
            // 현재 위치에 있는 NavMeshLink가 (NavMeshLink 컴포넌트를 적용해서) 수동으로 생성했고,
            // area 정보와 navMeshArea에 설정된 "Climb"과 같으면
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
        // 네비게이션을 이용한 자동 이동을 잠시 중단
        navMeshAgent.isStopped = true;

        // 현재 위치에 있는 NavMeshLink의 시작/종료 위치
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        // 오르내리는 이동 시간 설정
        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed;
        float currentTime = 0;
        float percent = 0;

        while(percent < 1)
        {
            // 단순히 deltaTime을 더하면 무조건 1초 후에 percent가 1이 되기 때문에
            // climbTime 시간이 지난 후에 percent가 1이 되도록 설정
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;

            // 시간 경과에 따라 오브젝트의 위치 설정
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        // NavMeshLink를 이용한 이동 완료
        navMeshAgent.CompleteOffMeshLink();
        // NavMeshLink 이동이 완료되었으니 네비게이션을 이용한 자동 이동 다시 시작
        navMeshAgent.isStopped = false;
    }
}
