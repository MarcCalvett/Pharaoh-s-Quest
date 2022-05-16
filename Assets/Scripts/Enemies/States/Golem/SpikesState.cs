using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesState : State
{
    private D_SpikesState stateData;

    public SpikesState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SpikesState stateData) : base(entity, stateMachine, animBoolName)
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
    public virtual void BackToWait()
    {

    }
}
