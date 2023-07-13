using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    
    [Header("״̬")]
    public bool isGround;


    private void Update()
    {
        Check();
    }

    private void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRadius,groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
