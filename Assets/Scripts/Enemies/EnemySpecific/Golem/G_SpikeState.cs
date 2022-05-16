using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_SpikeState : SpikesState
{
    private Golem golem;
    public G_SpikeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SpikesState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
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

        golem.GrowSpikes();
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
