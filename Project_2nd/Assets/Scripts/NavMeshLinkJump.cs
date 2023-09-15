using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshLinkJump : MonoBehaviour
{
    [SerializeField]
    private int navMeshArea = 2; // 적용할 구역 (Jump)
    [SerializeField]
    private float jumpSpeed = 10; // 점프 속도
    [SerializeField]
    private float gravity = -9.81f; // 중력
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            // IsOnJump() 메소드의 반환 값이 true일 때까지 반복 호출
            yield return new WaitUntil(() => IsOnJump());

            // 점프 행동
            yield return StartCoroutine(JumpTo());
        }
    }

    private bool IsOnJump()
    {
        // 현재 에이전트(navMeshAgent) 위치가 NavMeshLink 위에 있는지?
        if(navMeshAgent.isOnOffMeshLink)
        {
            // 현재 위치에 있는 NavMeshLink가 (NavMeshLink 컴포넌트를 적용해서) 수동으로 생성했거,
            // area 정보와 navMeshArea에 설정된 "Jump"와 같으면
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
        // 네비게이션을 이용한 자동 이동을 잠시 중단
        navMeshAgent.isStopped = true;

        // 현재 위치에 있는 NavMeshLink의 시작/종료 위치
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        // 뛰어서 이동하는 시간 설정
        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currentTime = 0;
        float percent = 0;
        // y 방향의 초기 속도
        float v0 = (end - start).y - gravity;

        while(percent < 1)
        {
            // 단순히 deltaTime을 더하면 무조건 1초 후에 percent가 1이 되기 때문에
            // jumpTime 시간이 지난 후에 percent가 1이 되도록 설정
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            // 시간 경과에 따라 오브젝트의 위치(x, z) 설정
            Vector3 position = Vector3.Lerp(start, end, percent);

            // 시간 경과에 따라 오브젝트의 위치(y) 설정
            // 포물선 운동 : 시작 위치 + 초기 속도 * 시간 + 중력 * 시간 제곱
            position.y = start.y + (v0 * percent) + (gravity * percent * percent);

            // 위에서 계산한 x, y, z 위치 값을 실제 오브젝트 위치에 대입
            transform.position = position;

            yield return null;
        }

        // NavMeshLink를 이용한 이동 완료
        navMeshAgent.CompleteOffMeshLink();
        // NavMeshLink 이동이 완료되었으니 네비게이션을 이용한 자동 이동 다시 시작
        navMeshAgent.isStopped = false;
    }
}
