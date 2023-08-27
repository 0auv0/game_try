using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    [HideInInspector] public PhysicsCheck pc;

    [Header("基本参数")]

    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;

    public Vector3 facedir;

    public Transform attacker;
    public float HurtForce;
    [Header("计时器")]

    public float waitTime;
    public float waitCounter;
    public bool isWait;
    public float lostTime;
    public float lostCounter;

    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 boxSize;
    public float distance;
    public LayerMask Layer;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
        waitCounter = waitTime;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        facedir = new Vector3(-transform.localScale.x, 0, 0);

        /*if(pc.touchLeftWall && facedir.x < 0 || pc.touchRightWall && facedir.x > 0)
        {
            isWait = true;
            anim.SetBool("Walk", false);
        }*/
        currentState.LogicUpdate();
        TimeCount();
        
    }
    private void FixedUpdate()
    {
        if (!isWait && !isHurt && !isDead)
        {
            Move();
        }
        currentState.PhysicsUpdate();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * facedir.x * Time.deltaTime,rb.velocity.y);
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }

    public void TimeCount()
    {
        if (isWait)
        {
            waitCounter -= Time.deltaTime;
            if(waitCounter < 0)
            {
                isWait = false;
                waitCounter = waitTime;
                transform.localScale = new Vector3(facedir.x, 1, 1);
            }
        }

        if (!FoundPlayer())
        {
            if(lostCounter >= 0)
            {
                lostCounter -= Time.deltaTime;
            }
        }
        else
        {
            lostCounter = lostTime;
        }
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset,boxSize,0,facedir,distance,Layer);
    }

    public void StateChange(NPCStates state)
    {
        var nextState = state switch
        {
            NPCStates.Patrol => patrolState,
            NPCStates.Chase => chaseState,
            _ => null
        };

        currentState.OnExit();
        currentState = nextState;
        currentState.OnEnter(this);

    }

    #region 事件方法
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //转向
        if(attackTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //击退
        isHurt = true;
        isWait = false;
        anim.SetTrigger("Hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * HurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDead()
    {
        gameObject.layer = 2;
        anim.SetBool("Dead",true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(distance * -transform.localScale.x,0),0.2f);
    }
    #endregion
}
