using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("��������")]

    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;

    public Vector3 facedir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        facedir = new Vector3(-transform.localScale.x, 0, 0);
    }
    private void FixedUpdate()
    {
        Move();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * facedir.x * Time.deltaTime,rb.velocity.y);
    }
}
