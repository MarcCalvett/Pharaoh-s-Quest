using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public GameObject tornadoPrefab;
    public float jumpForce;
    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float horizontal;    
    private float lastJump;
    private float lastParry;
    private float lastDefaultAttack;
    private float lastSPANS;
    private float lastDefaultAttackWind;
    private float lastSPAWS;
    private float lastScroll;
    private float timeSwitchSword;
    private Vector3 rememberPositionForSpaw;
    private Vector3 rememberOriginalPositionForSpaw;
    //private Vector3 rememberPositionForSpaw2;
    //private Vector3 rememberPositionForSpaw3;

    private int cancelMovement = 1;
    private int cancelGravity = 1;
    private bool attacking;
    private bool dodging;
    private bool windSwordInHand;
    private bool windSwordTaken;
    private bool spaNormalSword;
    private bool attackingWind;
    public bool spaWindSword;
    [HideInInspector]public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dodging = false;       
        windSwordInHand = false;
        windSwordTaken = true;
        attacking = false;
        spaNormalSword = false;
        attackingWind = false;
        spaWindSword = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        UpdateAnimations();       
       
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal*speed*cancelMovement, Rigidbody2D.velocity.y);
        Physics2D.gravity *= cancelGravity;
    }

    private void CheckInputs()
    {
        if (animator.GetBool("defaultAttack") == false && animator.GetBool("scroll") == false)
        {
            if (!spaWindSword)
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
            
        }        
        if (Input.GetKey(KeyCode.Space) && grounded && Time.time > lastJump + 0.5 && animator.GetBool("scroll") == false && cancelMovement != 0 && !spaWindSword)
        {
            Jump();
            lastJump = Time.time;
        }        
        if (Input.GetKey(KeyCode.F) && Time.time > lastParry + 0.9f && grounded && animator.GetBool("scroll") == false)
        {
            animator.SetBool("parry", true);
            cancelMovement = 0;
            lastParry = Time.time;
        }
        if (Input.GetKey(KeyCode.S) && grounded && animator.GetBool("running") && Time.time > lastScroll + 0.7f){
            animator.SetBool("scroll", true);
            animator.SetBool("running", false);
            lastScroll = Time.time;

        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (windSwordTaken && Time.time > timeSwitchSword + 0.5f)
            {
                if (windSwordInHand)
                {
                    windSwordInHand = false;
                }
                else
                {
                    windSwordInHand = true;
                }
                timeSwitchSword = Time.time;
            }
        }
        if (!windSwordInHand)
        {
            if (Input.GetKey(KeyCode.E) && Time.time > lastDefaultAttack + 1.2 && grounded && animator.GetBool("scroll") == false)
            {
                animator.SetBool("defaultAttack", true);
                cancelMovement = 0;
                lastDefaultAttack = Time.time;
            }
            if(Input.GetKey(KeyCode.R) && Time.time > lastSPANS + 1.4 && grounded && animator.GetBool("scroll") == false)
            {
                animator.SetBool("spaNS", true);
                cancelMovement = 0;
                lastSPANS = Time.time;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E) && Time.time > lastDefaultAttackWind + 1.6 && grounded && animator.GetBool("scroll") == false)
            {
                animator.SetBool("defaultAttackWind", true);
                cancelMovement = 0;
                lastDefaultAttackWind = Time.time;
            }
            if (Input.GetKey(KeyCode.R) && Time.time > lastSPAWS + 1.4 && grounded && animator.GetBool("scroll") == false)
            {
                animator.SetBool("spaWS", true);                
                lastSPAWS = Time.time;
                horizontal = 0;
                rememberPositionForSpaw = transform.position;
            }
        }

    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
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
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.9f, LayerMask.GetMask("Ground")))
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
        else if(!spaWindSword)
        {
            if (Rigidbody2D.velocity.y > 0)
            {
                animator.SetBool("jump", true);
                animator.SetBool("fall", false);
            }
            else if (Rigidbody2D.velocity.y < 0)
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
        attacking = false;
        cancelMovement = 1;
    }
    public void EndDefaultAttackWind()
    {
        attackingWind = false;
        cancelMovement = 1;
        ThrowTornado();
    }
    public void EndSPAttackWindSword()
    {
        cancelGravity = 1;
        spaWindSword = false;
        cancelMovement = 1;
    }
    public void EndParry()
    {
        dodging = false;
        cancelMovement = 1;
    }
    public void DefaultAttackStarted()
    {
        attacking = true;
        animator.SetBool("defaultAttack", false);
    }
    public void DefaultAttackWindStarted()
    {
        attackingWind = true;
        animator.SetBool("defaultAttackWind", false);
    }
    public void SPAttackNormalSwordStarted()
    {
        spaNormalSword = true;
        animator.SetBool("spaNS", false);
    }
    public void SPAttackWindSwordStarted()
    {
        cancelGravity = 0;
        spaWindSword = true;
        animator.SetBool("spaWS", false);
        rememberOriginalPositionForSpaw = transform.position;
    }
    public void EndSPAttackNormalSword()
    {
        spaNormalSword = false;
        cancelMovement = 1;
    }
    public void ParryStarted()
    {
        dodging = true;
        animator.SetBool("parry", false);
    }
    public void EndScroll()
    {
        horizontal = 0;
    }
    public void ThrowTornado()
    {
        Vector3 _direction;
        Vector3 origin;

        if (transform.localScale.x > 0.0f)
        {
            origin.x = 2f;
            origin.y = 0;
            origin.z = 0;
            _direction = Vector3.right;
            GameObject tornado = Instantiate(tornadoPrefab, transform.position + origin, Quaternion.identity);
            tornado.GetComponent<TornadoScript>().SetDirection(_direction);
        }
        else
        {
            origin.x = -2f;
            origin.y = 0;
            origin.z = 0;
            _direction = Vector3.left;
            GameObject tornado = Instantiate(tornadoPrefab, transform.position + origin, Quaternion.identity);
            tornado.GetComponent<TornadoScript>().SetDirection(_direction);
        }
        
        
    }    
    public void StopForSPAWS()
    {
        horizontal = 0;
    }    
    public void DashToFirstPoint()
    {
        
        if (transform.localScale.x > 0)
        {
            horizontal = 1;
        }
        else
        {
            horizontal = -1;
        }
    }
    public void SaveFirstPoint()
    {
        horizontal = 0;
        rememberPositionForSpaw = transform.position;
        if (transform.localScale.x > 0)
        {
            rememberPositionForSpaw.x = transform.position.x + 2;
        }
        else
        {
            rememberPositionForSpaw.x = transform.position.x - 2;
        }     
        
    }
    public void ReturnOrigin()
    {
        transform.position = rememberOriginalPositionForSpaw;
    }    
    public void DashToSecondPoint()
    {       

        if (transform.localScale.x > 0)
        {
            horizontal = 1;
        }
        else
        {
            horizontal = -1;
        }
    }
    public void GoSecondPoint()
    {
        Vector3 aux;
        float operationPosition;

        if (transform.localScale.x > 0)
        {                        
            operationPosition = rememberPositionForSpaw.x - rememberOriginalPositionForSpaw.x;
            aux.x = rememberOriginalPositionForSpaw.x - operationPosition;
        }
        else
        {                       
            operationPosition = rememberOriginalPositionForSpaw.x - rememberPositionForSpaw.x;
            aux.x = rememberOriginalPositionForSpaw.x + operationPosition;
        }

        aux.y = rememberOriginalPositionForSpaw.y;
        aux.z = rememberOriginalPositionForSpaw.z;        
        transform.position = aux;
    }
    public void ReturnFirstPoint()
    {        
        transform.position = rememberPositionForSpaw;
    }    
}
