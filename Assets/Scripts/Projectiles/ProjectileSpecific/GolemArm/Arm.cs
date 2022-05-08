using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : Projectile
{
    [SerializeField]
    GameObject explosion;
    
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float damage;
    [SerializeField]
    private FloatValue playerXPos;
    [SerializeField]
    private FloatValue playerYPos;
    [SerializeField]
    private float lifeTime;
    private float timeController = 0;

    float directionX;
    float directionY;
    Vector2 direction;

    protected override void Start()
    {
        base.Start();

        timeController = Time.time;
        if (Time.time - timeController < 1)
        {
            direction.Set(-1, 1);
        }
        else
        {
            direction.Set(playerXPos.RuntimeValue - transform.position.x, playerYPos.RuntimeValue - transform.position.y);
        }
        //direction = player.transform.position - transform.position;
        direction = direction.normalized;
               
        rb.velocity = direction * velocity;

        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = rb;

        //Vector3 aux = transform.localScale;
        //aux.x = Mathf.Abs(aux.x);
        ////aux.x = -aux.x;
        //transform.localScale = aux;

    }
    protected override void Update()
    {
        base.Update();

        attackDetails.position = transform.position;

        

        if(Time.time - timeController < 1)
        {
            direction.Set(-1, 1);
        }
        else
        {
            direction.Set(playerXPos.RuntimeValue - transform.position.x, playerYPos.RuntimeValue - transform.position.y);
        }
        
        //direction = player.transform.position - transform.position;
        direction = direction.normalized;

        rb.velocity = direction * velocity;

        

        if (Time.time - timeController >= lifeTime || destroy)
        {            
            Destroy(this.gameObject);            
            GameObject fireExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        }
       

        
        
    }
    

}
