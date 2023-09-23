using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MovementCharacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f; // �̵� �ӵ�

    [SerializeField]
    private float gravity = -9.81f; // �߷� ���

    [SerializeField]
    private float jumpForce = 3.0f; // �پ������ ��
    private Vector3 moveDirection = Vector3.zero; // �̵� ����
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
        // Ű �Է����� x, z�� �̵� ���� ����
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

        // Space Ű�� ������ �� �÷��̾ �ٴڿ� ������ ����
        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
        
        // �÷��̾ ���� ��� ���� ������
        // y�� �̵����⿡ gravity * Time.deltaTime�� ������
        if(characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // CharacterController�� ���ǵǾ� �ִ� Move() �޼ҵ带 �̿��� �̵�
        // �Ű������� ������ �� �̵� �Ÿ� ���� ���� (�̵� ���� * �̵� �ӵ� * Time.deltaTime)
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // ���� ī�޶� �ٶ󺸰� �ִ� ���� ������ ������ ����
        transform.rotation = Quaternion.Euler(0, mainCamera.eulerAngles.y, 0);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log($"{hit.gameObject.name} ������Ʈ�� �浹");
    }
}
