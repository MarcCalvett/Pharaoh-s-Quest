using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public float speed;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private float horizontal;
    private bool grounded;
    private float lastJump;
    private float lastDefaultAttack;
    private float lastScroll;
    private int cancelMovement = 1; 
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        UpdateAnimations();       
       
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal*speed*cancelMovement, rigidbody2D.velocity.y);
    }

    private void CheckInputs()
    {
        if (animator.GetBool("defaultAttack") == false && animator.GetBool("scroll") == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;                 
            }
            else
            {
                horizontal = 0;                
            }
        }        

        if (Input.GetKey(KeyCode.Space) && grounded && Time.time > lastJump + 0.5)
        {
            Jump();
            lastJump = Time.time;
        }
        if (Input.GetKey(KeyCode.E) && Time.time > lastDefaultAttack + 1.2 && grounded)
        {
            animator.SetBool("defaultAttack", true);
            cancelMovement = 0;
            lastDefaultAttack = Time.time;
        }        
        if(Input.GetKey(KeyCode.S) && grounded && animator.GetBool("running") && Time.time > lastScroll + 0.7f){
            animator.SetBool("scroll", true);
            animator.SetBool("running", false);
            lastScroll = Time.time;

        }
    }
    private void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void Orientation()
    {
        Vector3 auxiliar;
        if (horizontal < 0.0f && transform.localScale.x > 0.0f)
        {
            auxiliar = transform.localScale;
            transform.localScale = new Vector3(-auxiliar.x, auxiliar.y, auxiliar.z);
        }
        else if (horizontal > 0.0f && transform.localScale.x < 0.0f)
        {
            auxiliar = transform.localScale;
            transform.localScale = new Vector3(-auxiliar.x, auxiliar.y, auxiliar.z);
        }
    }

    private void CheckLanding()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.9f))
        {
            grounded = true;

        }
        else
        {
            grounded = false;
        }
    }
    private void UpdateAnimations()
    {
        Orientation();
        CheckLanding();

        animator.SetBool("running", horizontal != 0.0f && grounded);
        animator.SetBool("idle", horizontal == 0.0f && grounded);



        if (grounded)
        {
            animator.SetBool("jump", false);
            animator.SetBool("fall", false);
        }
        else
        {
            if (rigidbody2D.velocity.y > 0)
            {
                animator.SetBool("jump", true);
                animator.SetBool("fall", false);
            }
            else if (rigidbody2D.velocity.y < 0)
            {
                animator.SetBool("fall", true);
                animator.SetBool("jump", false);
            }
        }

        if (animator.GetBool("idle"))
        {
            animator.SetBool("scroll", false);
        }
    }
    public void EndDefaultAttack()
    {        
        cancelMovement = 1;
    }
    public void DefaultAttackBegun()
    {
        animator.SetBool("defaultAttack", false);
    }
    public void EndScroll()
    {
        horizontal = 0;
    }


}
