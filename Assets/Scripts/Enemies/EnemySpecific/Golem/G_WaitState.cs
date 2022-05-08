using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_WaitState : WaitState
{
    private Golem golem;
    private float timeBetweenArmsShoots = 15;
    private float timeInWait = 2;
    private float timeController;
    
    public G_WaitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_WaitState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.golem = golem;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        timeController = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - timeController >= timeInWait)
        {
            if (Time.time - golem.timeArmController >= timeBetweenArmsShoots)
            {
                stateMachine.ChangeState(golem.throwPunchState);
            }
            else
            {
                if (golem.CheckPlayerInCloseRangeAction())
                {
                    if(golem.currentHealth <= golem.entityData.maxHealth * 0.25f)
                    {
                        stateMachine.ChangeState(golem.protectState);
                    }
                    else
                    {
                        stateMachine.ChangeState(golem.spikesState);
                    }
                }
                else
                {
                    stateMachine.ChangeState(golem.beamState);
                }
            }
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
