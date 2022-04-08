using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGrounded = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

           
    }

    public override void Exit()
    {
        base.Exit();

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.dodgeTime)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected Vector2 CalculateLanding(Vector2 position, Vector2 velocity, Vector2 acceleration)
    {
        Vector2 result = new Vector2(0,position.y);

        float timeToLand = -2 * velocity.y / acceleration.y;

        result.x = position.x + velocity.x * timeToLand + 0.5f * acceleration.x * Mathf.Pow(timeToLand, 2);
        
        return result;
    }
    
}   
