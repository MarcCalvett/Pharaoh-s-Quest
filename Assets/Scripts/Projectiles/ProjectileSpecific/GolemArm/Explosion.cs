using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool detectDamage;
    bool damageApplied;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private LayerMask whatIsPlayer;

    AttackDetails attackDetails;

    private void Start()
    {
        detectDamage = true;
        damageApplied = false;

        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = new Vector2(0,0);
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        attackDetails.position = transform.position;

        Collider2D damageHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsPlayer);

        if (damageHit && !damageApplied && detectDamage)
        {
            damageApplied = true;            
            damageHit.transform.SendMessage("Damage", attackDetails);          

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
    public void StopDamaging()
    {
        detectDamage = false;
    }
    public void EndExplosion()
    {
        Destroy(this.gameObject);
    }
}
