using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BouncingBodyState : BouncingBodyState
{
    Mummy mummy;
    private float timeBouncingCtr;
    public M_BouncingBodyState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_BouncingBodyStateData stateData, Mummy mummy) : base(entity, stateMachine, animBoolName, stateData)
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

        mummy.Head.GetComponent<Animator>().SetBool("bouncing", true);
        mummy.LeftHand.GetComponent<Animator>().SetBool("bouncing", true);
        mummy.RightHand.GetComponent<Animator>().SetBool("bouncing", true);
        mummy.Body.GetComponent<Animator>().SetBool("bouncing", true);

        timeBouncingCtr = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        mummy.Head.GetComponent<Animator>().SetBool("bouncing", false);
        mummy.LeftHand.GetComponent<Animator>().SetBool("bouncing", false);
        mummy.RightHand.GetComponent<Animator>().SetBool("bouncing", false);
        mummy.Body.GetComponent<Animator>().SetBool("bouncing", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - timeBouncingCtr >= 5.5f)
        {
            stateMachine.ChangeState(mummy.iddleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
