using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapState { FIXED, FALLING, CRACKED };

public class Rockdown : MonoBehaviour
{

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private CapsuleCollider2D cc2d;
    [SerializeField]
    private Rigidbody2D rb;
    TrapState state;
    private float timeToFall;
    private float timeController;
    private float timeControllerDamageFixed = 0;
    private float timeControllerDamageFalling = 0;
    private float timeCracked = 0;
    private Vector2 originalPosition;
    private AttackDetails attackDetails;
    [SerializeField] float damage;
    [SerializeField] LayerMask whatisGround;
    [SerializeField] LayerMask whatisPlayer;

    void Start()
    {
        originalPosition = transform.position;
        TrapInitalState();        
        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();

    }
    void TrapInitalState()
    {
        sr.enabled = true;
        //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        state = TrapState.FIXED;
        timeController = Time.time;
        timeToFall = Random.Range(3, 13);
        transform.position = originalPosition;
        anim.SetBool("fixed", true);
        anim.SetBool("cracked", false);
    }
    void TrapFallingState()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        anim.SetBool("fixed", false);
        anim.SetBool("fall", true);
        transform.position += new Vector3(0, 0.1f, 0);
    }
    void TrapCrackedState()
    {
        anim.SetBool("fall", false);
        anim.SetBool("cracked", true);
        state = TrapState.CRACKED;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        timeCracked = Time.time;
    }
    void Update()
    {
        //Debug.Log(state);
        if (state == TrapState.FIXED && Time.time - timeController >= timeToFall)
        {
            state = TrapState.FALLING;
            TrapFallingState();
        }
        else if (state == TrapState.FALLING)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, whatisGround))
            {
                TrapCrackedState();

            }
        }
        else if (state == TrapState.CRACKED)
        {
            if (Time.time - timeCracked >= 2)
            {
                TrapInitalState();
            }
        }
    }
    void FixedUpdate()
    {
        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapCapsuleAll((Vector2)transform.position + new Vector2(cc2d.offset.x, cc2d.offset.y),
            cc2d.size, CapsuleDirection2D.Vertical, 0, whatisPlayer);

        switch (state)
        {
            case TrapState.FALLING:
                foreach (Collider2D collider in detectedObjects)
                {
                    if (Time.time - timeControllerDamageFalling >= 1 && collider.gameObject.CompareTag("Player"))
                    {
                        collider.SendMessage("Damage", attackDetails);
                        timeControllerDamageFalling = Time.time;
                        TrapCrackedState();
                        break;

                    }
                }

                break;
            case TrapState.FIXED:
                foreach (Collider2D collider in detectedObjects)
                {
                    if (Time.time - timeControllerDamageFixed >= 1 && collider.gameObject.CompareTag("Player"))
                    {
                        collider.SendMessage("Damage", attackDetails);
                        timeControllerDamageFixed = Time.time;
                        break;


                    }
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
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * 1.1f);

    }
}
