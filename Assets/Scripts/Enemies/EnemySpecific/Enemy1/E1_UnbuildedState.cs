using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_UnbuildedState : UnbuildedState
{
    private Enemy1 enemy;
    public E1_UnbuildedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_UnbuildedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateData.enemyFinished)
        {
            stateData.enemyFinished = false;
            stateMachine.ChangeState(enemy.deadState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
