﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;
    protected GameObject projectile;
    protected Projectile projectileScript;
    
    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        if(stateData.firstShoot.RuntimeValue || Time.time - stateData.lastShoot.RuntimeValue >= stateData.coolDown)
        {
            if (entity.CheckGround())
            {
                projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
                projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);

                stateData.firstShoot.RuntimeValue = false;
                stateData.lastShoot.RuntimeValue = Time.time;
            }
            
        }
        
    }
}
