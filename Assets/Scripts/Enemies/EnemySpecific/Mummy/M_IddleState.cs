using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IddleState : IddleState
{
    Mummy mummy;
    private float timeInIddleCtr;
    bool quitting;

    public M_IddleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IddleState stateData, Mummy mummy) : base(entity, stateMachine, animBoolName, stateData)
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

        mummy.Head.GetComponent<Animator>().SetBool("idle", true);
        //mummy.Head.transform.position = mummy.HeadPosTracker;
        mummy.LeftHand.GetComponent<Animator>().SetBool("idle", true);
        mummy.RightHand.GetComponent<Animator>().SetBool("idle", true);
        mummy.Body.GetComponent<Animator>().SetBool("idle", true);
        //mummy.Body.transform.position = new Vector3(0.003f, -0.67f, 0);

        mummy.Head.GetComponent<ToDamageMommy>().susurrosIdle.Play();
        //mummy.Body.GetComponent<Animator>().enabled = true;
        quitting = false;
        timeInIddleCtr = Time.time;
        //timeToTimingAttackCtr = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        mummy.Head.GetComponent<Animator>().SetBool("idle", false);
        mummy.LeftHand.GetComponent<Animator>().SetBool("idle", false);
        mummy.RightHand.GetComponent<Animator>().SetBool("idle", false);
        mummy.Body.GetComponent<Animator>().SetBool("idle", false);
        quitting = true;
        mummy.Head.GetComponent<ToDamageMommy>().susurrosIdle.Pause();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(Time.time - timeInIddleCtr >= 3f && Time.time - stateData.timeToTimerAttackController.RuntimeValue >= 20f)
        {
            //stateMachine.ChangeState(mummy.combinedAttack);
            stateMachine.ChangeState(mummy.bouncingBodyState);
            stateData.timeToTimerAttackController.RuntimeValue = Time.time;
            timeInIddleCtr = Time.time;
        }
        else if(Time.time - timeInIddleCtr >= 3f)
        {
            if (mummy.currentHealth <= 75) 
            {
                stateMachine.ChangeState(mummy.combinedAttack);
            }
            else
            {
                stateMachine.ChangeState(mummy.clappHandsState);
            }
            
            timeInIddleCtr = Time.time;
        }
        if(mummy.Head.GetComponent<ToDamageMommy>().susurrosIdle.isPlaying && mummy.Head.GetComponent<ToDamageMommy>().gamePaused.RuntimeValue)
        {
            mummy.Head.GetComponent<ToDamageMommy>().susurrosIdle.Pause();
        }
        if (!mummy.Head.GetComponent<ToDamageMommy>().susurrosIdle.isPlaying && !mummy.Head.GetComponent<ToDamageMommy>().gamePaused.RuntimeValue && !quitting)
        {
            mummy.Head.GetComponent<ToDamageMommy>().susurrosIdle.UnPause();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
