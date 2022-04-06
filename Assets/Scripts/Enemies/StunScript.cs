using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunScript : MonoBehaviour
{
    private Animator anim;
    private GameObject enemy;
    private Renderer rend;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();

        rend.enabled = false;
    }
    public void Stuned(GameObject enemy)
    {
        rend.enabled = true;
        this.enemy = enemy;
        anim.SetBool("startAnimation", true);

    }
    public void NoStuned()
    {
        rend.enabled = false;
        anim.SetBool("startAnimation", false);
        enemy.SendMessage("StopStun");
        
    }
}
