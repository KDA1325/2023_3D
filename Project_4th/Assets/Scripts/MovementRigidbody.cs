using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRigidbody : MonoBehaviour
{
    private Rigidbody rigid;
    private float moveSpeed = 5.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Left, a키를 누르면 -1 / Right, d키를 누르면 +1 / 아무 키도 누르지 않으면 0
        float x = Input.GetAxisRaw("Horizontal");

        // Down, s키를 누르면 -1 / Up, w키를 누르면 +1 / 아무 키도 누르지 않으면 0
        float z = Input.GetAxisRaw("Vertical");

        // 속력 = 이동 방향 * 이동 속도
        rigid.velocity = new Vector3(x, 0, z) * moveSpeed;

        
    }

}
