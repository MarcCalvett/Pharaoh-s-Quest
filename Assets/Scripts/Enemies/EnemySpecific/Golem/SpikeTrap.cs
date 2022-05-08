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

    bool detectPlayer;
    float timeController;
    float startTime;

    private void Start()
    {
        position = Position.RAISING;
        timeController = Time.time;
        detectPlayer = false;

        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        switch (position)
        {
            case Position.RAISING:
                Raise();
                break;
            case Position.TOP:
                SpikesUp();
                break;
            case Position.BOTTOM:
                Destroy(this.gameObject);
                break;
            case Position.BURYING:
                Bury();
                break;
        }
    }

    void Raise()
    {
        detectPlayer = false;

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
        detectPlayer = false;

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
            detectPlayer = false;
            position = Position.BURYING;
        }
        else
        {
            detectPlayer = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Time.time - timeController >= 1 && detectPlayer)
        {
            timeController = Time.time;

            collision.rigidbody.SendMessage("Damage", attackDetails);
        }
    }
}
