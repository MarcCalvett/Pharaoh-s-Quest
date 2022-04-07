using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState{ get; private set; }
    public E2_MeleeAtackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_StunState stunState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_StunState stunStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    private Vector3 posBeforeKnockback;

    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new E2_MeleeAtackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);

        posBeforeKnockback.Set(0, 0, 0);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(InformationMessageSource informationMessage)
    {
        base.Damage(informationMessage);
        posBeforeKnockback = this.gameObject.transform.position;

        if (healthOut)
        {
            isDead = true;            
        }
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if(!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Update()
    {
        base.Update();

        rb.rotation = 0;

        if (rb.bodyType == RigidbodyType2D.Kinematic && !CheckLedge())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;                                  //Si li estem aplicant knockback q pasi a dynamic
        }


        if (rb.bodyType == RigidbodyType2D.Dynamic && rb.velocity.y == 0)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;          //Si esta en repos en y que pasi a kinematic (cas de que se li acaba de aplicar un knockback)
        }
        else if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;  //Knockback vertical nomes
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None; //Si no esta en knockback que pugui moures en x
        }

        if (stateMachine.currentState != stunState && this.gameObject.GetComponent<Renderer>().material.color != originalColor)
        {
            this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        }
        if (stateMachine.currentState != stunState && StunStars.GetComponent<Renderer>().enabled)
        {
            StunStars.GetComponent<Renderer>().enabled = false;
        }
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
