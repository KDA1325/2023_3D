using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    /// <summary>
    /// 다른 오브젝트와 충돌하는 순간 1회 호출
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name} 오브젝트와 충돌 시작");
    }

    /// <summary>
    /// 다른 오브젝트와 충돌하고 있는 동안 매 프레임 호출
    /// </summary>
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name} 오브젝트와 충돌 중...");
    }

    /// <summary>
    /// 다른 오브젝트와의 충돌이 종료되는 순간 1회 호출
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name} 오브젝트와 충돌 종료"); 
    }
}