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

    private Vector3 posBeforeKnockback;
    private float velocityY;
    private float velocityYPast;

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

        posBeforeKnockback.Set(0, 0, 0);

        velocityY = rb.velocity.y;

        stateMachine.Initialize(moveState);
    }
    public override void Update()
    {
        base.Update();

        Debug.Log(stateMachine.currentState);


        velocityYPast = velocityY;
        velocityY = rb.velocity.y;

        //Debug.Log(stateMachine.currentState);
        //Debug.Log(stateMachine.currentState);

        rb.rotation = 0;
        
        

        if ((velocityYPast == velocityY || velocityYPast == rb.velocity.y) && rb.bodyType == RigidbodyType2D.Dynamic && CheckLedge())   //Solucio bug quedarse quiet a dynamic despres del knockback
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            //if(stateMachine.currentState == unbuildedState)
            //{
            //    rb.bodyType = RigidbodyType2D.Dynamic;
            //    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            //    GetComponent<BoxCollider2D>().isTrigger = true;
            //}
        }

        if (rb.bodyType == RigidbodyType2D.Kinematic && !CheckLedge())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;                                  //Si li estem aplicant knockback q pasi a dynamic
        }


        if(rb.bodyType == RigidbodyType2D.Dynamic && rb.velocity.y == 0 && stateMachine.currentState != unbuildedState)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;          //Si esta en repos en y que pasi a kinematic (cas de que se li acaba de aplicar un knockback)
        }
        else if(rb.bodyType == RigidbodyType2D.Dynamic)
        {            

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;  //Knockback vertical nomes
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None; //Si no esta en knockback que pugui moures en x
            if(stateMachine.currentState == unbuildedState)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }

        if(stateMachine.currentState != stunState && this.gameObject.GetComponent<Renderer>().material.color != originalColor)
        {
            this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        }
        if (stateMachine.currentState != stunState && StunStars.GetComponent<Renderer>().enabled){
            StunStars.GetComponent<Renderer>().enabled = false;
        }

        if (stateMachine.currentState == unbuildedState)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            if(CheckGround())
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
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


        posBeforeKnockback = this.gameObject.transform.position;

        if (healthOut)
        {
            isDead = true;

            stateMachine.ChangeState(unbuildedState);
            healthOut = false;
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
        stateMachine.ChangeState(moveState);
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

        
        stateMachine.ChangeState(stunState);
    }
    public override void StopStun()
    {
        base.StopStun();

        anim.SetBool("stun", false);

        this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        if (currentHealth > 0)
            stateMachine.ChangeState(moveState);
        else
            stateMachine.ChangeState(unbuildedState);

    }
}
