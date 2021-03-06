using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    enum Position { BOTTOM, TOP, RAISING, BURYING}

    Position position;
    AttackDetails attackDetails;

    [SerializeField]
    float damage;
    [SerializeField]
    float timeUP;
    [SerializeField]
    Vector3 higherPosition;
    [SerializeField]
    Vector3 minPosition;
    [SerializeField]
    AudioSource spikesMovementEffect;
    [SerializeField]
    BoolValue gamePaused;
    [SerializeField]
    FloatValue effectsVolume;

    Collider2D player;
    
    float timeController;
    float damageTimeController;
    float startTime;

    bool playerIn;

    private void Start()
    {
        position = Position.RAISING;
        timeController = Time.time;
        playerIn = false;
        damageTimeController = 0;
        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        spikesMovementEffect.volume = 1f * effectsVolume.RuntimeValue;

        if (spikesMovementEffect.isPlaying && gamePaused.RuntimeValue)
        {
            spikesMovementEffect.Pause();
        }
        if (!spikesMovementEffect.isPlaying && !gamePaused.RuntimeValue && spikesMovementEffect.time != 0f)
        {
            spikesMovementEffect.UnPause();
        }

        if (playerIn)
            DamagePlayer();
        switch (position)
        {
            case Position.RAISING:
                Raise();
                if(!spikesMovementEffect.isPlaying && !gamePaused.RuntimeValue)
                {
                    spikesMovementEffect.Play();
                }
                break;
            case Position.TOP:
                SpikesUp();
                if (spikesMovementEffect.isPlaying)
                {
                    spikesMovementEffect.Pause();
                    spikesMovementEffect.time = 0;
                }
                break;
            case Position.BOTTOM:
                Destroy(this.gameObject);
                break;
            case Position.BURYING:
                Bury();
                if (!spikesMovementEffect.isPlaying && !gamePaused.RuntimeValue)
                {
                    spikesMovementEffect.Play();
                }
                break;
        }
    }

    void Raise()
    {
        

        if (Time.time - timeController >= 0.1f)
        {
            timeController = Time.time;

            Vector3 aux = transform.position;
            aux.y += 0.1f;
            transform.position = aux;

            if(transform.position.y >= higherPosition.y)
            {
                if (transform.position.y > higherPosition.y)
                {
                    transform.position = higherPosition;
                }

                position = Position.TOP;
                startTime = Time.time;
            }
        }
    }
    void Bury()
    {
        

        if (Time.time - timeController >= 0.1f)
        {
            timeController = Time.time;

            Vector3 aux = transform.position;
            aux.y -= 0.1f;
            transform.position = aux;

            if (transform.position.y <= minPosition.y)
            {
                position = Position.BOTTOM;
            }
        }
    }    
    void SpikesUp()
    {
        if(Time.time - startTime >= timeUP)
        {
           
            position = Position.BURYING;
        }        
    }

    void DamagePlayer()
    {
        if (Time.time - damageTimeController >= 0.1f)
        {
            damageTimeController = Time.time;
            player.attachedRigidbody.SendMessage("Damage", attackDetails);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = true;
            player = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerIn = false;
    }

}
