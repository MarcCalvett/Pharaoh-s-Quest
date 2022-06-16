using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyDamage : MonoBehaviour
{
    private bool applyDamage;
    private float lastTimeDamaged;
    private AttackDetails attackDetails;
    [SerializeField]
    private float hitDamage;
    [SerializeField]
    BoolValue playerDashing;
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;

    private void Start()
    {
        applyDamage = false;
        lastTimeDamaged = 0;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (applyDamage && collision.gameObject.CompareTag("Player") && Time.time - lastTimeDamaged >= 1f && !playerDashing.RuntimeValue)
        {
            attackDetails.damageAmount = hitDamage;
            attackDetails.knockbackForce = new Vector2(0, 0);
            attackDetails.position = this.transform.position;
            attackDetails.type = TypeDamage.TEMPORAL;
            attackDetails.whoHitted = this.GetComponent<Rigidbody2D>();

            collision.attachedRigidbody.SendMessage("Damage", attackDetails);
            lastTimeDamaged = Time.time;

            Debug.Log(playerDashing.RuntimeValue);
        }
    }    
    void ActivateDamage()
    {
        applyDamage = true;
    }
    void DOntActivateDamage()
    {
        applyDamage = false;
    }
    void ActivateTrigger()
    {
        this.GetComponent<Collider2D>().isTrigger = true;

    }
    void DontActivateTrigger()
    {
        this.GetComponent<Collider2D>().isTrigger = false;
    }

    void Death()
    {
        Vector3 aux = this.transform.position;
        aux.z = -9.5f;
        GameObject.Instantiate(deathBloodParticle, aux, deathBloodParticle.transform.rotation);
        GameObject.Instantiate(deathChunkParticle, aux, deathChunkParticle.transform.rotation);
        this.gameObject.SetActive(false);
    }
}
