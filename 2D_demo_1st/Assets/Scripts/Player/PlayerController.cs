using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*// Start is called before the first frame update
    void Start()
    {
        
    }*/

    public PlayerInputControl InputControl; //�����������
    public Vector2 inputValue;              //�������ֵ
    private Rigidbody2D rb;                 //��ҵ�2D������

    [Header("��������")]
    public float speed;                     //��ҵ��ٶ�
    public float jump_force;               //�����Ծ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        InputControl = new PlayerInputControl();
        InputControl.Gameplay.Jump.started += Jump;
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }

    private void Update()
    {
        inputValue = InputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()      //2D����Ĺ̶�����
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(inputValue.x * speed * Time.deltaTime, rb.velocity.y );

        //���﷭ת
        int faceDir = (int) transform.localScale.x;
        if(inputValue.x > 0)
        {
            faceDir = 1;
        }
        if(inputValue.x < 0)
        {
            faceDir = -1;
        }

        transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("JUMP!!!");
        rb.AddForce(transform.up * jump_force, ForceMode2D.Impulse);
    }
}
