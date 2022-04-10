using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGas : MonoBehaviour
{
    [SerializeField]
    private float damagePerSecond;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float toxicityDuration;
    [SerializeField]
    private BoolValue isPlayerIntoxicated;
    [SerializeField]
    private BoolValue isPlayerDashing;

    private AttackDetails attackDetails;

    private Renderer rend;
    private Collider2D col;

    private float startTime;
    private float timeDamaging;
    private float lastEnterTime;
    private float lastTimeDamaged;


    private bool firstEnter = true;
    private bool gasOut = false;
    private bool playerOut = true;
    private bool coliderEnabled = true;

    private Collider2D playerCol;

    private void Start()
    {
        startTime = Time.time;

        //lastEnterTime = timeDamaging;

        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();

        attackDetails.damageAmount = damagePerSecond;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;       
        attackDetails.type = TypeDamage.TEMPORAL;
    }

    private void Update()
    {
        if (Time.time - startTime >= duration)
        {
            gasOut = true;

        }

        if (gasOut)
        {
            rend.enabled = false;
            //col.enabled = false;
            coliderEnabled = false;


        }

        if(coliderEnabled == false)
        {
            playerOut = true;
        }

        if (playerOut )
        {
            if (!firstEnter)
            {
                if (Time.time - lastEnterTime <= toxicityDuration)
                {
                    if (Time.time - lastTimeDamaged >= 1f)
                    {
                        DamagePlayer(playerCol);
                        lastTimeDamaged = Time.time;
                    }
                }
                else if (gasOut)
                {
                    isPlayerIntoxicated.RuntimeValue = false;
                    Destroy(gameObject);
                }
            }
            else if (gasOut)
            {
                isPlayerIntoxicated.RuntimeValue = false;
                Destroy(gameObject);
            }
            

        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCol = collision;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gasOut && coliderEnabled && !isPlayerDashing.RuntimeValue){

            isPlayerIntoxicated.RuntimeValue = true;
            playerOut = false;
            lastEnterTime = Time.time;

            if(Time.time - lastTimeDamaged >= 1f || firstEnter)
            {
                DamagePlayer(collision);
                lastTimeDamaged = Time.time;
                firstEnter = false;
            }

            

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOut = true;
    }

    private void DamagePlayer(Collider2D collision)
    {
        collision.transform.SendMessage("Damage", attackDetails);
    }
}
