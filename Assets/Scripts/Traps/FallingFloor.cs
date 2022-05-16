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

    bool fall;

    private void Start()
    {
        anim.SetBool("break", false);
        myCollider2D.isTrigger = false;
        fall = false;
        timeController = 0;
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
            timeController = Time.time;
        }
    }

    public void FloorBreaked()
    {
        Destroy(this.gameObject);
    }
}
