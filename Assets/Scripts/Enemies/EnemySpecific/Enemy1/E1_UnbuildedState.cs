using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_UnbuildedState : UnbuildedState
{
    private Enemy1 enemy;
    public E1_UnbuildedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_UnbuildedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //enemy.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        enemy.GetComponent<BoxCollider2D>().isTrigger = true;
        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void Exit()
    {
        base.Exit();

        
        enemy.GetComponent<BoxCollider2D>().isTrigger = false;
        enemy.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!enemy.GetComponent<BoxCollider2D>().isTrigger && entity.CheckGround())
        {
            enemy.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (stateData.enemyFinished)
        {
            stateData.enemyFinished = false;
            stateMachine.ChangeState(enemy.deadState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
