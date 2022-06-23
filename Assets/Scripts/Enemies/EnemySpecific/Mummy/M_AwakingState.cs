using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_AwakingState : AwakinngState
{
    private Mummy mummy;
    private float timeAwakingCtr;
    public M_AwakingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_AwakingState stateData, Mummy mummy) : base(entity, stateMachine, animBoolName, stateData)
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

        

        mummy.Head.GetComponent<Animator>().SetBool("awaking", true);
        mummy.LeftHand.GetComponent<Animator>().SetBool("awaking", true);
        mummy.RightHand.GetComponent<Animator>().SetBool("awaking", true);
        mummy.Body.GetComponent<Animator>().SetBool("awaking", true);

        timeAwakingCtr = Time.time;

        mummy.awakeSound.Play();
        mummy.DarkMagicAwaking.Play();
    }

    public override void Exit()
    {
        base.Exit();

        mummy.Head.GetComponent<Animator>().SetBool("awaking", false);
        //mummy.HeadPosTracker = mummy.Head.transform.position;
        //mummy.RightHand.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //mummy.Body.GetComponent<Animator>().enabled = false;
        //mummy.Body.transform.position = new Vector3(0, 0, 0);
        mummy.LeftHand.GetComponent<Animator>().SetBool("awaking", false);
        mummy.RightHand.GetComponent<Animator>().SetBool("awaking", false);
        mummy.Body.GetComponent<Animator>().SetBool("awaking", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Debug.Log(mummy.Head.transform.position);
        if (Time.time - timeAwakingCtr >= 1.5f)
        {
            stateMachine.ChangeState(mummy.iddleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    void ChangeToIdle()
    {
        stateMachine.ChangeState(mummy.iddleState);
    }
}
