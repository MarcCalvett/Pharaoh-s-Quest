﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ThrowPunch : ThrowPunchState
{
    private Golem golem;

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
    }

    public override void Exit()
    {
        base.Exit();

        golem.timeArmController = Time.time;
       // golem.explosioPunyAudio.Play();
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