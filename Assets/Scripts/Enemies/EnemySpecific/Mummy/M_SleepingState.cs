using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SleepingState : SleepingState
{
    Mummy mummy;
    private bool waitBeforeWakeUp;
    private float timeToWakeUpController;
    public M_SleepingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SleepingStateData stateData, Mummy mummy) : base(entity, stateMachine, animBoolName, stateData)
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
        Vector3 aux;

        mummy.Head.GetComponent<Animator>().SetBool("sleeping", true);
            aux.x = -1.754f;
            aux.y = -0.887f;
            aux.z = 0f;
            mummy.Head.transform.position = aux;


        mummy.LeftHand.GetComponent<Animator>().SetBool("sleeping", true);
            aux.x = -0.397f;
            aux.y = 0.02f;
            aux.z = 0f;
            mummy.LeftHand.transform.position = aux;

        mummy.RightHand.GetComponent<Animator>().SetBool("sleeping", true);
            aux.x = 1.545f;
            aux.y = 0.009f;
            aux.z = 0f;
            mummy.RightHand.transform.position = aux;
        
        mummy.Body.GetComponent<Animator>().SetBool("sleeping", true);
            aux.x = 1.705f;
            aux.y = -0.731f;
            aux.z = 0f;
            mummy.Body.transform.position = aux;

        timeToWakeUpController = Time.time;
        waitBeforeWakeUp = true;
    }

    public override void Exit()
    {
        base.Exit();

        mummy.Head.GetComponent<Animator>().SetBool("sleeping", false);
        mummy.LeftHand.GetComponent<Animator>().SetBool("sleeping", false);
        mummy.RightHand.GetComponent<Animator>().SetBool("sleeping", false);
        mummy.Body.GetComponent<Animator>().SetBool("sleeping", false);

        //Debug.Log("SI");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (waitBeforeWakeUp && Time.time - timeToWakeUpController >= 3)
        {
            stateMachine.ChangeState(mummy.awakingState);
            waitBeforeWakeUp = false;
            timeToWakeUpController = Time.time;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
