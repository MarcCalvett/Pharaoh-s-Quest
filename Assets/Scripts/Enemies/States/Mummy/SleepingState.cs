using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : State
{
    D_SleepingStateData stateData;
    public SleepingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SleepingStateData stateData) : base(entity, stateMachine, animBoolName)
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
