using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpears : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D myCollider2D;
    [SerializeField]
    float damage;
    [SerializeField]
    LayerMask whatIsPlayer;
    [SerializeField]
    AudioSource spearSound;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;

    private float timeController;
    AttackDetails attackDetails;
    bool detection;

    private void Start()
    {
        attackDetails.damageAmount = damage;
        attackDetails.knockbackForce = Vector2.zero;
        attackDetails.position = transform.position;
        attackDetails.type = TypeDamage.TEMPORAL;
        attackDetails.whoHitted = GetComponent<Rigidbody2D>();

        timeController = 0;
        detection = false;
    }

    private void Update()
    {
        spearSound.volume = 1f * effectsVolume.RuntimeValue;

        if (spearSound.isPlaying && gamePaused.RuntimeValue)
        {
            spearSound.Pause();
        }
        if (!spearSound.isPlaying && !gamePaused.RuntimeValue && spearSound.time != 0)
        {
            spearSound.UnPause();
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapBoxAll(myCollider2D.bounds.center, myCollider2D.bounds.size, 0 ,whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            if(collider.gameObject.CompareTag("Player") && detection && Time.time - timeController >= 1)
            {
                collider.SendMessage("Damage", attackDetails);
                timeController = Time.time;
                break;
            }
        }
    }

    public void DetectionOn()
    {
        detection = true;
        spearSound.Play();
    }
    public void DetectionOff()
    {
        detection = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(myCollider2D.bounds.center, myCollider2D.bounds.size);
    }
    
}
