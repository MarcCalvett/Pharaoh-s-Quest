using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFalldownTrap : MonoBehaviour
{
    enum TrapState { FIXED, FALLING, CRACKED};

    
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    public CapsuleCollider2D cc2d;
    TrapState state;
    private float timeToFall;
    private float timeController;
    private float timeControllerDamage = 0;
    private float timeCracked = 0;
    private Vector2 originalPosition;
    private AttackDetails attackDetails;
    float minimYInArray = 0;
    float xFromMinimY = 0;    
    [SerializeField] float damage;
    [SerializeField] LayerMask whatisGround;
    [SerializeField] LayerMask whatisPlayer;

    private void Start()
    {        
        originalPosition = transform.position;
        TrapInitalState();

        //collider2D = 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = rb;
        
    }
    void TrapInitalState()
    {
        sr.enabled = true;
        //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        state = TrapState.FIXED;
        timeController = Time.time;
        timeToFall = Random.Range(3, 13);
        transform.position = originalPosition;
        anim.SetBool("fixed", true);
        anim.SetBool("cracked", false);
    }
    private void Update()
    {
        if(state == TrapState.FIXED && Time.time - timeController >= timeToFall)
        {
            state = TrapState.FALLING;
            rb.constraints = RigidbodyConstraints2D.None;
            anim.SetBool("fixed", false);
            anim.SetBool("fall", true);
            transform.position += new Vector3(0,0.1f, 0);
        }
        else if(state == TrapState.FALLING)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, whatisGround))
            {
                anim.SetBool("fall", false);
                anim.SetBool("cracked", true);
                state = TrapState.CRACKED;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                timeCracked = Time.time;

            }
        }
        else if(state == TrapState.CRACKED)
        {
            if(Time.time - timeCracked >= 2)
            {
                TrapInitalState();                
            }
        }
    }
    private void FixedUpdate()
    {
        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapCapsuleAll((Vector2)transform.position + new Vector2(cc2d.offset.x, cc2d.offset.y),
            cc2d.size, CapsuleDirection2D.Vertical, whatisPlayer);

        switch (state)
        {
            case TrapState.FALLING:
                foreach (Collider2D collider in detectedObjects)
                {
                    collider.SendMessage("Damage", attackDetails);
                }
                    state = TrapState.CRACKED;
                
                break;
            case TrapState.FIXED:
                if(Time.time - timeControllerDamage >= 1)
                {
                    foreach (Collider2D collider in detectedObjects)
                    {
                        collider.SendMessage("Damage", attackDetails);                        
                    }
                    timeControllerDamage = Time.time;
                }
                

                break;
            default:
                break;
        }
    }
    
    void SpriteOFF()
    {
        sr.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * 1.1f);
        
    }
}
