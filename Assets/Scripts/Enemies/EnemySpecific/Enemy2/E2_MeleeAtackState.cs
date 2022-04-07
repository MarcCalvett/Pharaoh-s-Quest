using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAtackState : MeleeAttackState
{
    private Enemy2 enemy;
    public E2_MeleeAtackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
        stateData.imWhoHitted = entity.rb;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Debug.Log(isAnimationFinished);
        if (isAnimationFinished)
        {
            
            if (isPlayerInMinAgroRange)
            {
                
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        isAnimationFinished = false;
    }
}
