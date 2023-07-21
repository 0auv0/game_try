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
    public PhysicsCheck physicsCheck;
    public PlayerAnimation playerAnimation;
    public CapsuleCollider2D coll;

    [Header("��������")]
    public float speed;                     //��ҵ��ٶ�
    public float jump_force;               //�����Ծ����

    public float HurtForce;                 //���˻�õ���

    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;


    [Header("״̬")]
    public bool isHurt;

    public bool isDead;
    public bool isAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();

        InputControl = new PlayerInputControl();
        InputControl.Gameplay.Jump.started += Jump;

        InputControl.Gameplay.Attack.started += PlayerAttack;

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
        CheckState();
    }

    private void FixedUpdate()      //2D����Ĺ̶�����
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
        
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
        if(physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jump_force, ForceMode2D.Impulse);
        }
        
    }
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    #region UnityEngine
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 new_dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;


        rb.AddForce(new_dir * HurtForce, ForceMode2D.Impulse);
    }

    public void GetDeath()
    {
        isDead = true;
        InputControl.Gameplay.Disable();
    }

    #endregion


    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}
