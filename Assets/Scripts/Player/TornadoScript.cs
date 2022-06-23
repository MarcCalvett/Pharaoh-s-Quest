using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public float speed;
    public float damage;
    private Rigidbody2D Rigidbody2D;
    [SerializeField] CapsuleCollider2D colliderForDamage;
    [SerializeField] LayerMask whatIsEnemy;
    private Vector3 direction;
    private InformationMessageSource infoMessage;
    List<Collider2D> colliders = new List<Collider2D>();
    [SerializeField]
    AudioSource tornadoSound;
    [SerializeField]
    BoolValue gamePaused;
    [SerializeField]
    FloatValue effectsVolume;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        tornadoSound.volume = effectsVolume.RuntimeValue * 1f;

        if (tornadoSound.isPlaying && gamePaused.RuntimeValue)
        {
            tornadoSound.Pause();
        }
        if (!tornadoSound.isPlaying && !gamePaused.RuntimeValue && tornadoSound.time != 0f)
        {
            tornadoSound.UnPause();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ApplyDamage();
    }
    public void SetDirection(Vector3 _direction)
    {
        direction = _direction;
        
    }
    public void DestroyTornado()
    {
        Destroy(gameObject);
    }
    public void SetParameters()
    {
        if(Rigidbody2D.velocity.x == 0)
        {
            Vector3 auxiliar = transform.localScale;
            transform.localScale = new Vector3(auxiliar.x * direction.x, auxiliar.y, auxiliar.z);
            Rigidbody2D.velocity = direction * speed;
            Debug.Log(direction);
            Debug.Log(Rigidbody2D.velocity);
        }
        
    }
    void ApplyDamage()
    {

        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapCapsuleAll((Vector2)transform.position + new Vector2(colliderForDamage.offset.x, colliderForDamage.offset.y), colliderForDamage.size, CapsuleDirection2D.Vertical, whatIsEnemy);

        foreach(Collider2D collider in detectedObjects)
        {
            if (colliders.Contains(collider) == false)
            {
                infoMessage.damage = damage;
                infoMessage.position = transform.position;
                infoMessage.hoop = false;
                infoMessage.objectTag = this.gameObject.tag;
                collider.SendMessage("Damage", infoMessage);

                colliders.Add(collider);
            }
        }
    }    

}
