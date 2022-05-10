using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;
    public int facingDirection { get; private set; }
    public BoolValue playerAttacking;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }

    public bool imSleep, imProtected;

    public Transform landingSpot;

    [SerializeField]
    protected GameObject StunStars;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform checkGround;
    [SerializeField]
    private Transform playerCheck;
    

    public float currentHealth;

    private int lastDamageDirection;

    private Vector2 velocityWorkSpace;



    protected Color originalColor;

    protected bool isDead = false;
    protected bool healthOut = false;


    public virtual void Start()
    {
        
        currentHealth = entityData.maxHealth;

        facingDirection = 1;
        //aliveGO = transform.Find("Alive").gameObject;
        aliveGO = this.gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();

        originalColor = this.gameObject.GetComponent<Renderer>().material.color;

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();       
       

        //anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }
    public virtual bool CheckWall( )
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        Vector3 fixposition;
        fixposition.x = ledgeCheck.position.x + facingDirection * 0.8f;
        fixposition.y = ledgeCheck.position.y;
        fixposition.z = ledgeCheck.position.z;

        return Physics2D.Raycast(fixposition, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(checkGround.position, 0.2f, entityData.whatIsGround);
    }
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkSpace;
    }
    public virtual Vector2 RetSetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        return velocityWorkSpace;
    }
    public virtual void DamageHop(float velocity)
    {
        
        velocityWorkSpace.Set(0, velocity);
        rb.velocity = velocityWorkSpace;

    }
    public virtual void Damage(InformationMessageSource informationMessage)
    {
        if (!isDead && !imSleep && !imProtected)
        {
            currentHealth -= informationMessage.damage;

            if (informationMessage.hoop)
            {
                DamageHop(entityData.damageHopSpeed);
            }

            Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            if (informationMessage.position.x > aliveGO.transform.position.x)
            {
                lastDamageDirection = -1;
            }
            else
            {
                lastDamageDirection = 1;
            }
        }        

        if (currentHealth <= 0)
        {

            healthOut = true;            
                        
        }
    }
    public virtual void Flip()
    {
        //Vector3 auxiliar;
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
        //auxiliar = transform.localScale;
        //aliveGO.transform.localScale = new Vector3(auxiliar.x *= -1, auxiliar.y, auxiliar.z);
    }
    public virtual void OnDrawGizmos()
    {
        Vector3 fixposition;
        fixposition.x = ledgeCheck.position.x + facingDirection * 0.8f;
        fixposition.y = ledgeCheck.position.y;
        fixposition.z = ledgeCheck.position.z;

        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(fixposition, fixposition + (Vector3)(Vector2.down  * entityData.ledgeCheckDistance));


        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(checkGround.position, 0.2f);
        Vector3 beSure = new Vector2(landingSpot.position.x + 0.5f * -facingDirection, landingSpot.position.y);
        Gizmos.DrawLine(beSure, beSure + (Vector3)(Vector2.down * (entityData.ledgeCheckDistance+ 0.3f)));
        Vector3 aux = playerCheck.position;
        aux.x += entityData.closeRangeActionDistance;
        Gizmos.DrawLine(playerCheck.position, aux);

    }
    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            if (!playerAttacking.RuntimeValue)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                
            }

        }
    }
    public virtual void BuildingEnded()
    {
        
        currentHealth = entityData.maxHealth;
        
        anim.SetBool("unbuilded", false);
        
    }
    public virtual void Stun()
    {        
        StunStars.SendMessage("Stuned", this.gameObject);
    }
    public virtual void StopStun()
    {
        
    }
    
}

