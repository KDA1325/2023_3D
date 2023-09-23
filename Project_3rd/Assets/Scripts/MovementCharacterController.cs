using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MovementCharacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f; // 이동 속도

    [SerializeField]
    private float gravity = -9.81f; // 중력 계수

    [SerializeField]
    private float jumpForce = 3.0f; // 뛰어오르는 힘
    private Vector3 moveDirection = Vector3.zero; // 이동 방향
    private Animator animator;

    [SerializeField]
    private Transform mainCamera;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // 키 입력으로 x, z축 이동 방향 설정
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(x != 0 || z != 0)
        {
            //animator.Play("Run");
            animator.SetFloat("moveSpeed", 1);
        }
        else
        {
            //animator.Play("Idle");
            animator.SetFloat("moveSpeed", 0);
        }
;
        // moveDirection = new Vector3(x, moveDirection.y, z);
        Vector3 dir = mainCamera.rotation * new Vector3(x, 0, z);
        moveDirection = new Vector3(dir.x, moveDirection.y, dir.z);

        // Space 키를 눌렀을 때 플레이어가 바닥에 있으면 점프
        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
        
        // 플레이어가 땅을 밟고 있지 않으면
        // y축 이동방향에 gravity * Time.deltaTime을 더해줌
        if(characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // CharacterController에 정의되엉 있는 Move() 메소드를 이용해 이동
        // 매개변수에 프레임 당 이동 거리 정보 적용 (이동 방향 * 이동 속도 * Time.deltaTime)
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // 현재 카메라가 바라보고 있는 전방 방향을 보도록 설정
        transform.rotation = Quaternion.Euler(0, mainCamera.eulerAngles.y, 0);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log($"{hit.gameObject.name} 오브젝트와 충돌");
    }
}
