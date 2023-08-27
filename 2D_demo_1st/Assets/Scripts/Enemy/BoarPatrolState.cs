using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    public override void LogicUpdate()
    {

        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.StateChange(NPCStates.Chase);
        }


        //Debug.Log(currentEnemy.pc.isGround);

        if ((!currentEnemy.pc.isGround && currentEnemy.facedir.x < 0 ) ||
            (!currentEnemy.pc.isGround1 && currentEnemy.facedir.x > 0) ||
            (currentEnemy.pc.touchLeftWall && currentEnemy.facedir.x < 0) || 
            (currentEnemy.pc.touchRightWall && currentEnemy.facedir.x > 0))
        {
            currentEnemy.isWait = true;
            currentEnemy.anim.SetBool("Walk", false);
        }
        else
        {
            currentEnemy.isWait = false;
            currentEnemy.anim.SetBool("Walk", true);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("Walk", false);
    }
}
