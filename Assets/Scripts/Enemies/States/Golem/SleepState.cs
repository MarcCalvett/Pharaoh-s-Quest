using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : State
{
    private D_SleepState stateData;

    public SleepState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SleepState stateData) : base(entity, stateMachine, animBoolName)
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
