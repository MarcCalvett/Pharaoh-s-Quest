using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamState : State
{
    D_BeamState stateData;
    public BeamState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_BeamState stateData) : base(entity, stateMachine, animBoolName)
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

    public virtual void LaserON()
    {
        Debug.Log("LaserON");
        
    }

    public virtual void LaserOFF()
    {
        Debug.Log("LaserOFF");
    }

    public virtual void BackToWait()
    {       
    }


}
