using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("Run", true);
    }

    public override void LogicUpdate()
    {
        if(currentEnemy.lostCounter <= 0)
        {
            currentEnemy.StateChange(NPCStates.Patrol);
        }
        
        if ((!currentEnemy.pc.isGround && currentEnemy.facedir.x < 0) ||
            (!currentEnemy.pc.isGround1 && currentEnemy.facedir.x > 0) ||
            (currentEnemy.pc.touchLeftWall && currentEnemy.facedir.x < 0) ||
            (currentEnemy.pc.touchRightWall && currentEnemy.facedir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.facedir.x, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("Run", false);
    }

}
