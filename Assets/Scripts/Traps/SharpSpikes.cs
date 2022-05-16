using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpSpikes : MonoBehaviour
{
    AttackDetails attackDetails;

    [SerializeField]
    float damage;
    [SerializeField]
    BoxCollider2D myCollider;
    [SerializeField]
    Vector3 respawnPositionPlayer;
    

    private void Start()
    {
        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("Damage", attackDetails);
            collision.gameObject.transform.position = respawnPositionPlayer;
        }
    }

}
