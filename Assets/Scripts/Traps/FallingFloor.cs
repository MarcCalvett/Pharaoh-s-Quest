using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    private float timeController;

    [SerializeField]
    Animator anim;
    [SerializeField]
    BoxCollider2D myCollider2D;
    [SerializeField]
    AudioSource groundSound;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;


    bool fall;

    private void Start()
    {
        anim.SetBool("break", false);
        myCollider2D.isTrigger = false;
        fall = false;
        timeController = 0;
    }

    private void Update()
    {
        groundSound.volume = 1f * effectsVolume.RuntimeValue;

        if (groundSound.isPlaying && gamePaused.RuntimeValue)
        {
            groundSound.Pause();
        }
        if (!groundSound.isPlaying && !gamePaused.RuntimeValue && groundSound.time != 0)
        {
            groundSound.UnPause();
        }
    }
    private void FixedUpdate()
    {
        if (fall && Time.time - timeController >= 0.5f)
        {
            anim.SetBool("break", true);
            myCollider2D.isTrigger = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fall = true;
            groundSound.Play();
            timeController = Time.time;
        }
    }

    public void FloorBreaked()
    {
        Destroy(this.gameObject);
    }
}
