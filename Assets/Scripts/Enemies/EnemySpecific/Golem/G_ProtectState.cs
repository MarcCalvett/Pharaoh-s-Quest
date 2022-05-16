using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ProtectState : ProtectState
{
    private Golem golem;

    public G_ProtectState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ProtectState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
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

        if(!entity.CheckPlayerInCloseRangeAction() && entity.anim.GetBool("goDefenseLoop"))
        {
            golem.OutProtLoop();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
