using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ClappHandsState : ClapHandsState
{
    private Mummy mummy;
    private float timeClapCtr;

    public M_ClappHandsState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ClappHandsState stateData, Mummy mummy) : base(entity, stateMachine, animBoolName, stateData)
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

        mummy.Head.GetComponent<Animator>().SetBool("clap", true);
        mummy.LeftHand.GetComponent<Animator>().SetBool("clap", true);
        mummy.RightHand.GetComponent<Animator>().SetBool("clap", true);
        mummy.Body.GetComponent<Animator>().SetBool("clap", true);

        

        timeClapCtr = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        mummy.Head.GetComponent<Animator>().SetBool("clap", false);
        mummy.LeftHand.GetComponent<Animator>().SetBool("clap", false);
        mummy.RightHand.GetComponent<Animator>().SetBool("clap", false);
        mummy.Body.GetComponent<Animator>().SetBool("clap", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - timeClapCtr >= 5.5f)
        {
            stateMachine.ChangeState(mummy.iddleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
