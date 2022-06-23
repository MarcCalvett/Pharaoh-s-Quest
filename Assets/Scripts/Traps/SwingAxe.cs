using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{
    AttackDetails attackDetails;
    float timeRotateController;
    float currentAngle;
    float lastTimeDamaged;    
    float angleVariation;
    int timesSwinged = 0;

    [SerializeField]
    CircleCollider2D myCollider2d;
    [SerializeField]
    float damage;
    [SerializeField]
    float limitAngle;
    [SerializeField]
    LayerMask whatisPlayer;
    [SerializeField]
    AudioSource axeSound;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;


    void Start()
    {
        currentAngle = limitAngle;
        timeRotateController = 0;
        angleVariation = 2f;
        lastTimeDamaged = 0;

        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        axeSound.volume = 1f * effectsVolume.RuntimeValue;
        if (currentAngle >= 82)
        {
            currentAngle = 82;
            timesSwinged++;
        }
        else if(currentAngle <= -82)
        {
            currentAngle = -82;
            timesSwinged++;
        }

        if(timesSwinged % 2 == 0)
        {
            angleVariation = Mathf.Abs(angleVariation);
        }
        else
        {
            angleVariation = -Mathf.Abs(angleVariation);
        }
        if (axeSound.isPlaying && gamePaused.RuntimeValue)
        {
            axeSound.Pause();
        }
        if (!axeSound.isPlaying && !gamePaused.RuntimeValue && axeSound.time != 0)
        {
            axeSound.UnPause();
        }
        if(currentAngle == 82 || currentAngle == -82)
        {
            axeSound.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time - timeRotateController >= 0.01f)
        {
            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);            
            timeRotateController = Time.time;
        }
        ApplyDamage();
        Rotate();
    }
    void Rotate()
    {
        currentAngle += angleVariation;
    }
    void ApplyDamage()
    {
        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapCircleAll(myCollider2d.bounds.center, myCollider2d.bounds.extents.x, whatisPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider.gameObject.CompareTag("Player") && Time.time - lastTimeDamaged >= 1f)
            {
                collider.SendMessage("Damage", attackDetails);
                lastTimeDamaged = Time.time;
                break;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(myCollider2d.bounds.center, myCollider2d.bounds.extents.x);
    }
}
