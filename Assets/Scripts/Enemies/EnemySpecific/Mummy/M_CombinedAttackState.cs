using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_CombinedAttackState : CombinedAttackState
{
    Mummy mummy;
    float timeInCombinedCtr;
    public M_CombinedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_CombinateAttackStateData stateData, Mummy mummy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.mummy = mummy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        mummy.Head.GetComponent<Animator>().SetBool("combined", true);
        mummy.LeftHand.GetComponent<Animator>().SetBool("combined", true);
        mummy.RightHand.GetComponent<Animator>().SetBool("combined", true);
        mummy.Body.GetComponent<Animator>().SetBool("combined", true);

        timeInCombinedCtr = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        mummy.Head.GetComponent<Animator>().SetBool("combined", false);
        mummy.LeftHand.GetComponent<Animator>().SetBool("combined", false);
        mummy.RightHand.GetComponent<Animator>().SetBool("combined", false);
        mummy.Body.GetComponent<Animator>().SetBool("combined", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time - timeInCombinedCtr >= 6.42f)
        {
            stateMachine.ChangeState(mummy.iddleState);
            timeInCombinedCtr = Time.time;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
