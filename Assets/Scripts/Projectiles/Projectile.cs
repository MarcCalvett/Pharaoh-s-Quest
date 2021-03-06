using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    protected Rigidbody2D rb;
    protected bool destroy;

    private bool isGravityOn;
    protected bool hasHitGround;
    protected bool hasHitPlayer;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;
    [SerializeField]
    private BoolValue playerDashing;

    [SerializeField]
    private bool remoteVelocity;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!remoteVelocity)
        {
            rb.gravityScale = 0.0f;
            rb.velocity = transform.right * speed;
        }        

        isGravityOn = false;

        xStartPos = transform.position.x;

        
    }

    protected virtual void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;
            if (isGravityOn || remoteVelocity)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
            }
        }
        
    }

    protected virtual void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            
            

            if (damageHit && !playerDashing.RuntimeValue)
            {
                hasHitPlayer = true;
                damageHit.transform.SendMessage("Damage", attackDetails);
                destroy = true;
                //Destroy(gameObject);
                
            }

            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn && !remoteVelocity)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }

        
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
        attackDetails.type = TypeDamage.TEMPORAL;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }

}
