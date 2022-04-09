using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : DodgeState
{
    private Enemy2 enemy;
    public E2_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        stateData.initialVelocity = entity.RetSetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);

        enemy.landingSpot.position = CalculateLanding(enemy.transform.position, stateData.initialVelocity, enemy.rb.gravityScale * Physics2D.gravity);

        if (CheckGroundWhereLanding())
        {
            entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
        }
        else
        {
            if (isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }

        

        

        
    }

    public override void Exit()
    {
        base.Exit();

        stateData.dodging = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        

        if (stateData.dodging && entity.rb.velocity.y <= 0 && entity.CheckGround())
        {
            stateData.dodging = false;
            
        }

        if (isDodgeOver || !stateData.dodging)
        {
            if (isPlayerInMaxAgroRange && performCloseRangeAction && entity.CheckGround()) 
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }      
            else if(isPlayerInMaxAgroRange && !performCloseRangeAction && entity.CheckGround())
            {
                stateMachine.ChangeState(enemy.rangedAttackState);
            }
            else if(entity.CheckGround())
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckGroundWhereLanding()
    {
        Vector2 beSure = new Vector2(enemy.landingSpot.position.x + 0.5f * -entity.facingDirection, enemy.landingSpot.position.y);
       return Physics2D.Raycast(beSure, Vector2.down, entity.entityData.ledgeCheckDistance + 0.3f, entity.entityData.whatIsGround);
        
    }

    
}
