using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    
    [Header("状态")]
    public bool isGround;


    private void Update()
    {
        Check();
    }

    private void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRadius,groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
