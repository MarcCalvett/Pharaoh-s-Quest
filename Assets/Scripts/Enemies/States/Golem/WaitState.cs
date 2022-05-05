using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    private D_WaitState stateData;
    public WaitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_WaitState stateData) : base(entity, stateMachine, animBoolName)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
