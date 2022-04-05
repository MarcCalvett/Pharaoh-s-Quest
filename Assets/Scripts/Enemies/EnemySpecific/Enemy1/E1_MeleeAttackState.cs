﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
    private Enemy1 enemy;

    public E1_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

        }
        else if (!entity.anim.GetBool("move") && entity.anim.GetBool("meleeAttack") && !entity.anim.GetBool("playerDetected"))
        {
            //entity.anim.SetBool("move", false);
            //entity.anim.SetBool("meleeAttack", false);
            entity.anim.SetBool("playerDetected", true);
            //stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if(entity.anim.GetBool("playerDetected") && entity.anim.GetBool("meleeAttack"))
        {
            entity.anim.SetBool("playerDetected", false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
