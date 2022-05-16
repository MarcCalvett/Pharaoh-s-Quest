using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_SleepState : SleepState
{
    private Golem golem;
    private float timeController = 0;    
    private bool firstEnter = true;
    float timeTransition = 0;
    float transition = 1f;
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

        if (golem.swordsTaken.RuntimeValue && golem.cameraShake.RuntimeValue && firstEnter)
        {
            timeController = Time.time;
            firstEnter = false;
        }

        if(Time.time - timeController >= golem.timeShakeDuration && timeController != 0)
        {
            golem.cameraShake.RuntimeValue = false;
            if(timeTransition == 0)
            {
                timeTransition = Time.time;
            }
            if (Time.time - timeTransition >= transition)
            {
                stateMachine.ChangeState(golem.awakeState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
