using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_SleepState : SleepState
{
    private Golem golem;

    public G_SleepState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SleepState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
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
