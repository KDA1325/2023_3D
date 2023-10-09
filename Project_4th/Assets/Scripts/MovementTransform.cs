using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTransform : MonoBehaviour
{
    private float moveSpeed = 3.0f;
    private Vector3 moveDirection;

    private void Awake()
    {
        moveSpeed = 5.0f;
        moveDirection = Vector3.right;
    }

    //private void Awake()
    //{
    //    // 이동/회전/크기를 제어하는 "Transform" 컴포넌트를 조작해 오브젝트 이동
    //    // 새로운 위치 = 현재 위치 + (방향 * 속도)
    //    // 1. transform.position = transform.position + new Vector3(1, 0, 0) * 1;
    //    // 2. A = A + B를 A += B와 같이 단축
    //    //    transform.position += new Vector3(1, 0, 0) * 1;
    //    // 3. 1 단위의 방향을 나타낼 때는
    //    //    new Vector3(1, 0, 0) 대신 유니티에서 제공하는 Vector3.right와 같이 사용
    //    transform.position += Vector3.right * 1;
    //}

    private void Update()
    {
        // Left, a키를 누르면 -1 / Right, d키를 누르면 +1 / 아무 키도 누르지 않으면 0
        float x = Input.GetAxisRaw("Horizontal");

        // Down, s키를 누르면 -1 / Up, w키를 누르면 +1 / 아무 키도 누르지 않으면 0
        float z = Input.GetAxisRaw("Vertical");

        // 이동방향 설정
        moveDirection = new Vector3(x, 0, z);

        //// 매 프레임마다 Update()가 시작될 때 이동방향을 (0, 0, 0)으로 설정
        //moveDirection = Vector3.zero; // new Vector3(0, 0, 0)
        
        //// Up/Down/Left/Right 방향키를 눌렀을 때 이동방향(moveDirection) 설정
        //if(Input.GetKey(KeyCode.UpArrow))
        //{
        //    moveDirection += Vector3.forward;
        //}
        //else if(Input.GetKey(KeyCode.DownArrow))
        //{
        //    moveDirection += Vector3.back;
        //}
        //else if(Input.GetKey(KeyCode.LeftArrow))
        //{
        //    moveDirection += Vector3.left;
        //}
        //else if(Input.GetKey(KeyCode.RightArrow))
        //{
        //    moveDirection += Vector3.right;
        //}

        //transform.position += Vector3.right * 1 * Time.deltaTime;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
