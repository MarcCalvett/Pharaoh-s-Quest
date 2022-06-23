using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ThrowPunch : ThrowPunchState
{
    private Golem golem;
    float timeToLaunch;
    bool launched;

    public G_ThrowPunch(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ThrowPunchState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
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
        timeToLaunch = Time.time;
        
        launched = false;
    }

    public override void Exit()
    {
        base.Exit();

        golem.timeArmController = Time.time;
        golem.missileLaunchAudio.Pause();
        golem.missileLaunchAudio.time = 0;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time-timeToLaunch>=0.5f && !launched)
        {
            golem.missileLaunchAudio.Play();
            launched = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
