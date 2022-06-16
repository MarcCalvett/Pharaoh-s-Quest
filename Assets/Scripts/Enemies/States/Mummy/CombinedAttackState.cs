using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedAttackState : State
{
    private D_CombinateAttackStateData stateData;
    public CombinedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_CombinateAttackStateData stateData) : base(entity, stateMachine, animBoolName)
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
