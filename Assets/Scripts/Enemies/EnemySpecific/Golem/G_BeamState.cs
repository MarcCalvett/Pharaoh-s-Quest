using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_BeamState : BeamState
{
    private Golem golem;
    public G_BeamState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_BeamState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
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
        golem.laserAudio.Play();
    }

    public override void Exit()
    {
        base.Exit();
        golem.laserAudio.Pause();
        golem.laserAudio.time = 0;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void LaserON()
    {
        base.LaserON();
      //  golem.laserAudio.Play();
    }
    public override void LaserOFF()
    {
        base.LaserOFF();
       // golem.laserAudio.Stop();
    }
}
