using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_DeadState deadState { get; private set; }
    public E1_UnbuildedState unbuildedState { get; private set; }
    public E1_StunState stunState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_UnbuildedState unbuildedStateData;
    [SerializeField]
    private D_StunState stunStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);
        unbuildedState = new E1_UnbuildedState(this, stateMachine, "unbuilded", unbuildedStateData, this);
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);

        stateMachine.Initialize(moveState);
    }
    public override void Update()
    {
        base.Update();

        //Debug.Log(stateMachine.currentState);

        if(rb.bodyType == RigidbodyType2D.Kinematic && !CheckLedge())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if(rb.bodyType == RigidbodyType2D.Dynamic && rb.velocity.y == 0)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if(rb.bodyType == RigidbodyType2D.Dynamic)
        {            
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if(stateMachine.currentState != stunState && this.gameObject.GetComponent<Renderer>().material.color != originalColor)
        {
            this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    //public override void OnCollisionExit2D(Collision2D collision)
    //{
    //    base.OnCollisionExit2D(collision);
    //}
    public override void Damage(InformationMessageSource informationMessage)
    {
        base.Damage(informationMessage);


        if (transitionToUnbuilded)
        {
            isDead = true;
            stateMachine.ChangeState(unbuildedState);
            transitionToUnbuilded = false;
        }

        if (isDead && stateMachine.currentState == unbuildedState && informationMessage.objectTag == "Tornado")
        {
            unbuildedStateData.enemyFinished = true;
        }

        

        //if (isDead && stateMachine.currentState != unbuildedState)
        //{
        //    //stateMachine.ChangeState(unbuildedState);
        //    isDead = false;
        //}



    }

    public override void BuildingEnded()
    {
        base.BuildingEnded();

        //anim.SetBool("move", true);
        //anim.SetBool("unbuilded", false);
        //stateMachine.ChangeState(cha);
        unbuildedStateData.enemyFinished = false;
        isDead = false;
        stateMachine.Initialize(moveState);
    }

    public override void Stun()
    {
        base.Stun();

        anim.SetBool("stun", true);

        float r = 0.5f;
        float g = 1f;
        float b = 0.8f;
        float a = 1f;

        Color freeze = new Color(r, g, b, a);


        this.gameObject.GetComponent<Renderer>().material.color = freeze;

        
        stateMachine.Initialize(stunState);
    }
    public override void StopStun()
    {
        base.StopStun();

        anim.SetBool("stun", false);

        this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        stateMachine.Initialize(moveState);
    }
}
