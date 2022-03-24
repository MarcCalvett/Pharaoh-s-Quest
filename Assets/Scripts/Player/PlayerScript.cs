﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    private float lastDash;
    private float scrollSpeedBoost;
    private float lastTreure;
    private float dashSpeedBoost;
    private float timeSwitchSword;
    private Vector3 rememberPositionForSpaw;
    private Vector3 rememberOriginalPositionForSpaw;
    private Vector2 rememberGravity;
    //private Vector3 rememberPositionForSpaw2;
    //private Vector3 rememberPositionForSpaw3;
    public FloatValue playerHealth;
    public FloatValue playerStamina;
    public IntValue playerLives;
    private int cancelMovement;    
    private bool attacking;
    private bool dodging;
    private bool windSwordInHand;
    private bool windSwordTaken;
    private bool spaNormalSword;
    private bool attackingWind;
    private bool dashing;
    public bool spaWindSword;
    public BoolValue rechargingStamina;
    private float recoverStaminaTime;
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
        dashing = false;
        cancelMovement = 1;
        scrollSpeedBoost = 2;
        dashSpeedBoost = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!rechargingStamina.RuntimeValue)
        {
            if (animator.GetBool("rechargingLoop"))
            {
                animator.SetBool("rechargingLoop", false);
            }
            CheckInputs();
            UpdateAnimations();
        }
        else
        {
            WaitForStamina();
        }           
       
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal*speed*cancelMovement, Rigidbody2D.velocity.y);        
    }    

    [ContextMenu("Take Damage")]
    private void TakeDamage()
    {
        playerHealth.RuntimeValue -= 35;
    }
    [ContextMenu("Spend Stamina")]
    private void TakeStamina()
    {
        playerStamina.RuntimeValue -= 25;
    }
    private void WaitForStamina()
    {
        if (!animator.GetBool("recharging") && !animator.GetBool("rechargingLoop"))
        {
            animator.SetBool("recharging", true);
        }
        animator.SetBool("idle", false);
        animator.SetBool("scroll", false);
        animator.SetBool("dash", false);
        horizontal = 0;
        if (grounded)
        {
            animator.SetBool("jump", false);
            animator.SetBool("fall", false);
        }
    }
    private void CheckInputs()
    {
        if (animator.GetBool("defaultAttack") == false && animator.GetBool("scroll") == false && animator.GetBool("dash") == false)
        {
            if (!spaWindSword)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if(Time.time > recoverStaminaTime + 0.02f)
                    {
                        playerStamina.RuntimeValue -= 0.02f;
                        recoverStaminaTime = Time.time;
                    }
                    horizontal = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (Time.time > recoverStaminaTime + 0.02f)
                    {
                        playerStamina.RuntimeValue -= 0.02f;
                        recoverStaminaTime = Time.time;
                    }
                    horizontal = 1;
                }
                else
                {
                    horizontal = 0;
                }
            }
            
        }        
        if (Input.GetKey(KeyCode.Space) && grounded && Time.time > lastJump + 0.5 && animator.GetBool("scroll") == false && cancelMovement != 0 && !spaWindSword && animator.GetBool("dash") == false)
        {
            playerStamina.RuntimeValue -= 15;
            if (playerStamina.RuntimeValue > 0)
            {
                Jump();
            }            
            lastJump = Time.time;
        }        
        if (Input.GetKey(KeyCode.F) && Time.time > lastParry + 0.9f && grounded && animator.GetBool("scroll") == false && animator.GetBool("dash") == false)
        {
            playerStamina.RuntimeValue -= 20;
            if (playerStamina.RuntimeValue > 0)
            {
                animator.SetBool("parry", true);
                cancelMovement = 0;
            }            
            lastParry = Time.time;
        }
        if (Input.GetKey(KeyCode.S) && grounded && animator.GetBool("running")){
            if (!windSwordInHand && Time.time > lastScroll + 0.7f)
            {
                playerStamina.RuntimeValue -= 8;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("scroll", true);
                    animator.SetBool("running", false);
                    speed *= scrollSpeedBoost;
                }                
                lastScroll = Time.time;
            }
            else if(windSwordInHand && Time.time >lastDash + 0.7)
            {
                playerStamina.RuntimeValue -= 15;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("dash", true);
                    animator.SetBool("running", false);
                    dashing = true;
                    speed *= dashSpeedBoost;
                    rememberGravity = Physics2D.gravity;
                    Physics2D.gravity *= 0;
                }                
                lastDash = Time.time;
            }
           
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
            if (Input.GetKey(KeyCode.E) && Time.time > lastDefaultAttack + 1.2 && grounded && animator.GetBool("scroll") == false && animator.GetBool("dash") == false)
            {
                playerStamina.RuntimeValue -= 20;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("defaultAttack", true);
                    cancelMovement = 0;
                }                
                lastDefaultAttack = Time.time;
            }
            if(Input.GetKey(KeyCode.R) && Time.time > lastSPANS + 1.4 && grounded && animator.GetBool("scroll") == false && animator.GetBool("dash") == false)
            {
                playerStamina.RuntimeValue -= 35;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("spaNS", true);
                    cancelMovement = 0;
                }                
                lastSPANS = Time.time;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E) && Time.time > lastDefaultAttackWind + 1.6 && grounded && animator.GetBool("scroll") == false && animator.GetBool("dash") == false)
            {
                playerStamina.RuntimeValue -= 25;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("defaultAttackWind", true);
                    cancelMovement = 0;
                }                
                lastDefaultAttackWind = Time.time;
            }
            if (Input.GetKey(KeyCode.R) && Time.time > lastSPAWS + 1.4 && grounded && animator.GetBool("scroll") == false && animator.GetBool("dash") == false)
            {
                playerStamina.RuntimeValue -= 45;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("spaWS", true);
                    horizontal = 0;
                    rememberPositionForSpaw = transform.position;
                }                               
                lastSPAWS = Time.time;                
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
        else if(!spaWindSword && !dashing)
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
            animator.SetBool("dash", false);
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
        Physics2D.gravity = rememberGravity;        
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
        rememberGravity = Physics2D.gravity;
        Physics2D.gravity *= 0;        
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
        speed /= scrollSpeedBoost;
        horizontal = 0;
    }
    public void EndDash()
    {
        dashing = false;
        speed /= dashSpeedBoost;
        horizontal = 0;
        Physics2D.gravity = rememberGravity;
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
    public void RechargeLoop()
    {
        animator.SetBool("rechargingLoop", true);
        animator.SetBool("recharging", false);
    }
    
}