using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    float dashTime;

    public GameObject tornadoPrefab;
    public float jumpForce;
    public float speed;
    [SerializeField]
    private Collider2D colliderForDashes;
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
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;
    public Vector3 spawner;
    private Vector3 rememberPositionForSpaw;
    private Vector3 rememberOriginalPositionForSpaw;
    private Vector2 knockBackVelocities;
    private float direccionEmpuje;
    private float rememberGravity;
    private AttackDetails attackDetails;
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
    private bool dashingS;    
    public bool spaWindSword;
    private bool applyingScrollBoost;
    public BoolValue intoxicated;
    public BoolValue rechargingStamina;
    public BoolValue imAttacking;
    public BoolValue applyKnockBack;
    public BoolValue dashing2;
    [SerializeField]
    public BoolValue swordsTaken;
    private int aplicator;
    private int _aplicator;
    public Vector2 knockBack;
    private float recoverStaminaTime;
    private InformationMessageSource infoMessage;    
    [SerializeField]
    private FloatValue playerXPos;
    [SerializeField]
    private FloatValue playerYPos;
    [HideInInspector]public bool grounded;
    [HideInInspector] Collider2D collider;
    [SerializeField] private PlayerAttackValues values;
    [SerializeField]private GameObject defaultAttack;
    [SerializeField] private GameObject defaultAttackWind;
    [SerializeField] private GameObject specialAttack;
    [SerializeField] private GameObject[] specialWindAttack;
    [SerializeField] FloatValue xRespawn;
    [SerializeField] FloatValue yRespawn;
    [SerializeField] AudioSource weakAttackSound;
    [SerializeField] AudioSource strongAttackSound;
    [SerializeField] AudioSource windAttackSound;
    [SerializeField] FloatValue effectsVolume;
    [SerializeField] BoolValue gamePaused;
    [SerializeField] AudioSource parrySound;
    [SerializeField] AudioSource semiParrySound;
    [SerializeField] AudioSource desertWalkSound;
    [SerializeField] AudioSource interiorWalkSound;
    [SerializeField] AudioSource rollingAudio;
    [SerializeField] AudioSource dashingAudio;
    [SerializeField] AudioSource takeDamage;
    [SerializeField] AudioSource takeDamagePoison;
    [SerializeField] AudioSource playerJump;
  
    

    private float deathCtr;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        dodging = false;       
        windSwordInHand = false;
        windSwordTaken = swordsTaken.RuntimeValue;
        attacking = false;        
        spaNormalSword = false;
        attackingWind = false;
        spaWindSword = false;
        dashing = false;
        dashingS = false;
        applyingScrollBoost = false;
        cancelMovement = 1;
        scrollSpeedBoost = 1.5f;
        dashSpeedBoost = 3;
        knockBackVelocities.Set(100, 2);      
        intoxicated.RuntimeValue = intoxicated.initialValue;
        deathCtr = 0;
        

        spawner = new Vector3(xRespawn.RuntimeValue, yRespawn.RuntimeValue, 0);
        transform.position = spawner;
    }   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(horizontal);
        //Debug.Log(grounded);
        Debug.Log(intoxicated.RuntimeValue);

        windSwordTaken = swordsTaken.RuntimeValue;

        interiorWalkSound.volume = 1f * effectsVolume.RuntimeValue;
        desertWalkSound.volume = 1f * effectsVolume.RuntimeValue;
        rollingAudio.volume = 1f * effectsVolume.RuntimeValue;
        dashingAudio.volume = 1f * effectsVolume.RuntimeValue;
        takeDamage.volume = 1f * effectsVolume.RuntimeValue;
        takeDamagePoison.volume = 1f * effectsVolume.RuntimeValue;
        playerJump.volume = 1f * effectsVolume.RuntimeValue;

        if (playerJump.isPlaying && gamePaused.RuntimeValue)
        {
            playerJump.Pause();
        }
        if (playerJump.time != 0 && !playerJump.isPlaying && !gamePaused.RuntimeValue)
        {
            playerJump.UnPause();
        }

        if (takeDamagePoison.isPlaying && gamePaused.RuntimeValue)
        {
            takeDamagePoison.Pause();
        }
        if (takeDamagePoison.time != 0 && !takeDamagePoison.isPlaying && !gamePaused.RuntimeValue)
        {
            takeDamagePoison.UnPause();
        }

        if (takeDamage.isPlaying && gamePaused.RuntimeValue)
        {
            takeDamage.Pause();
        }
        if(takeDamage.time != 0 && !takeDamage.isPlaying && !gamePaused.RuntimeValue)
        {
            takeDamage.UnPause();
        }

        if (horizontal != 0 && grounded && !dashing && !applyingScrollBoost)
        {
            if(SceneManager.GetActiveScene().name == "DesertProject" && !desertWalkSound.isPlaying && !gamePaused.RuntimeValue)
            {
                desertWalkSound.Play();
            }
            if ((SceneManager.GetActiveScene().name == "Lvl3" || SceneManager.GetActiveScene().name == "Lvl2" || SceneManager.GetActiveScene().name == "Lvl2.1") && !interiorWalkSound.isPlaying && !gamePaused.RuntimeValue)
            {
                interiorWalkSound.Play();
            }
        }
        else
        {
            if (desertWalkSound.isPlaying)
            {
                desertWalkSound.Pause();
                //desertWalkSound.time = 0;
            }

            if (interiorWalkSound.isPlaying)
            {
                interiorWalkSound.Pause();
                //interiorWalkSound.time = 0;
            }

        }

        if(!rollingAudio.isPlaying && !gamePaused.RuntimeValue && applyingScrollBoost)
        {
            rollingAudio.Play();
        }
        if(gamePaused.RuntimeValue && rollingAudio.isPlaying)
        {
            rollingAudio.Pause();            
        }

        if (!dashingAudio.isPlaying && !gamePaused.RuntimeValue && dashing2.RuntimeValue)
        {
            dashingAudio.Play();
        }
        if (gamePaused.RuntimeValue && dashingAudio.isPlaying)
        {
            dashingAudio.Pause();            
        }

        if (desertWalkSound.isPlaying && gamePaused.RuntimeValue)
        {
            desertWalkSound.Pause();
        }
        

        if (interiorWalkSound.isPlaying && gamePaused.RuntimeValue)
        {
            interiorWalkSound.Pause();
        }
        

        weakAttackSound.volume = effectsVolume.RuntimeValue * 1f;

        if (weakAttackSound.isPlaying && gamePaused.RuntimeValue)
        {
            weakAttackSound.Pause();
        }
        if (!weakAttackSound.isPlaying && !gamePaused.RuntimeValue && weakAttackSound.time != 0f)
        {
            weakAttackSound.UnPause();
        }

        strongAttackSound.volume = effectsVolume.RuntimeValue * 1f;

        if (strongAttackSound.isPlaying && gamePaused.RuntimeValue)
        {
            strongAttackSound.Pause();
        }
        if (!strongAttackSound.isPlaying && !gamePaused.RuntimeValue && strongAttackSound.time != 0f)
        {
            strongAttackSound.UnPause();
        }

        windAttackSound.volume = effectsVolume.RuntimeValue * 1f;

        if (windAttackSound.isPlaying && gamePaused.RuntimeValue)
        {
            windAttackSound.Pause();
        }
        if (!windAttackSound.isPlaying && !gamePaused.RuntimeValue && windAttackSound.time != 0f)
        {
            windAttackSound.UnPause();
        }

        parrySound.volume = effectsVolume.RuntimeValue * 1f;

        if (parrySound.isPlaying && gamePaused.RuntimeValue)
        {
            parrySound.Pause();
        }
        if (!parrySound.isPlaying && !gamePaused.RuntimeValue && parrySound.time != 0f)
        {
            parrySound.UnPause();
        }

        semiParrySound.volume = effectsVolume.RuntimeValue * 1f;

        if (semiParrySound.isPlaying && gamePaused.RuntimeValue)
        {
            semiParrySound.Pause();
        }
        if (!semiParrySound.isPlaying && !gamePaused.RuntimeValue && semiParrySound.time != 0f)
        {
            semiParrySound.UnPause();
        }

        if (playerHealth.RuntimeValue <= 0 && playerLives.RuntimeValue <= 0)
        {
            if(deathCtr == 0)
            {
                Vector3 aux = this.transform.position;
                aux.z = -9.5f;
                GameObject.Instantiate(deathBloodParticle, aux, deathBloodParticle.transform.rotation);
                GameObject.Instantiate(deathChunkParticle, aux, deathChunkParticle.transform.rotation);
                this.GetComponent<SpriteRenderer>().enabled = false;
                deathCtr = Time.time;
            }
            else if (Time.time - deathCtr >= 4f)
            {
                SceneManager.LoadScene("Credits");
                this.gameObject.SetActive(false);
            }
           
        }  

        

        //Debug.Log(Rigidbody2D.gravityScale);
        if (intoxicated.RuntimeValue)
        {
            IntoxicatedColor();
        }
        else
        {
            OriginalColor();
        }

        if(!attacking && !spaNormalSword && !spaWindSword)
        {
            cancelMovement = 1;
        }

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

        if(dashingS)
        CheckCollision();

        playerXPos.RuntimeValue = this.transform.position.x;
        playerYPos.RuntimeValue = this.transform.position.y;

        //if (knockBack.x != 0 && Rigidbody2D.gravityScale == 0)
        //{
        //    collider.isTrigger = false;
        //    Rigidbody2D.gravityScale = rememberGravity;
        //}
        //if (grounded)
        //{

        //}
        if (!intoxicated.RuntimeValue)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
    private void FixedUpdate()
    {
        if(knockBack.x != 0 && !collider.isTrigger && Rigidbody2D.gravityScale != 0)
        {
            cancelMovement = 1;
            
            if(Rigidbody2D.velocity.y == 0)
            {
                knockBack.Set(0, 0);
            }
            Rigidbody2D.velocity = this.Rigidbody2D.velocity;

            //Debug.Log("SI");

        }
        else
        {
            if (!grounded)
                CheckCollisionInAir();
            Rigidbody2D.velocity = new Vector2(horizontal * speed * cancelMovement, Rigidbody2D.velocity.y);
            //Rigidbody2D.AddForceAtPosition(knockBack, transform.position);      
            //Debug.Log("NO");

        }

        //{
        //    knockBack.Set(0, 0);
        //}    

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
                        playerStamina.RuntimeValue -= 0;
                        recoverStaminaTime = Time.time;
                    }
                    horizontal = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (Time.time > recoverStaminaTime + 0.02f)
                    {
                        playerStamina.RuntimeValue -= 0;
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
            playerStamina.RuntimeValue -= 0;
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
        if (Input.GetKey(KeyCode.S) && grounded && animator.GetBool("running") && knockBack.x == 0){
            if (!windSwordInHand && Time.time > lastScroll + 0.7f)
            {
                playerStamina.RuntimeValue -= 8;
                if (playerStamina.RuntimeValue > 0)
                {
                    animator.SetBool("scroll", true);
                    animator.SetBool("running", false);

                    if (!applyingScrollBoost)
                    {
                        speed *= scrollSpeedBoost;
                        applyingScrollBoost = true;
                    }
                    
                }                
                lastScroll = Time.time;
            }
            else if(windSwordInHand && Time.time >lastDash + 0.7)
            {
                playerStamina.RuntimeValue -= 15;
                if (playerStamina.RuntimeValue > 0)
                {
                    dashTime = Time.time;
                    animator.SetBool("dash", true);
                    animator.SetBool("running", false);
                    dashing = true;
                    dashingS = true;
                    dashing2.RuntimeValue = true;
                    
                    speed *= dashSpeedBoost;
                    this.Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                    //rememberGravity = Physics2D.gravity;
                    rememberGravity = this.Rigidbody2D.gravityScale;
                    collider.isTrigger = true;
                    //Physics2D.gravity *= 0;
                    this.Rigidbody2D.gravityScale *= 0;
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
        playerJump.Play();
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
        Vector3 auxiliar;
        auxiliar = transform.localScale;

        Vector3 auxiliar2;
        auxiliar2.x = GetComponent<BoxCollider2D>().bounds.min.x;
        auxiliar2.y = transform.position.y;
        auxiliar2.z = transform.position.z;

        Vector3 auxiliar3;
        auxiliar3.x = GetComponent<BoxCollider2D>().bounds.max.x;
        auxiliar3.y = transform.position.y;
        auxiliar3.z = transform.position.z;

        //Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(Mathf.Sign(auxiliar.x) * 0.86f, 0, 0), Vector3.down * 0.9f, Color.red);
        Debug.DrawRay(auxiliar2, Vector3.down * 0.9f, Color.red);
        Debug.DrawRay(auxiliar3, Vector3.down * 0.9f, Color.red);


        if (Physics2D.Raycast(auxiliar2, Vector3.down, 0.9f, LayerMask.GetMask("Ground")) || Physics2D.Raycast(auxiliar3, Vector3.down, 0.9f, LayerMask.GetMask("Ground")))
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
        imAttacking.RuntimeValue = false;
        cancelMovement = 1;
    }
    public void EndDefaultAttackWind()
    {
        attackingWind = false;
        //imAttacking.RuntimeValue = false;
        cancelMovement = 1;
        ThrowTornado();
    }
    public void EndSPAttackWindSword()
    {
        //Physics2D.gravity = rememberGravity;
        this.Rigidbody2D.gravityScale = rememberGravity;
        collider.isTrigger = false;
        spaWindSword = false;
        dashing = false;
        dashing2.RuntimeValue = false;
        imAttacking.RuntimeValue = false;
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
        imAttacking.RuntimeValue = true;
        cancelMovement = 0;
        animator.SetBool("defaultAttack", false);
        weakAttackSound.Play();
    }
    public void DefaultAttackWindStarted()
    {
        attackingWind = true;
        //imAttacking.RuntimeValue = true;
        animator.SetBool("defaultAttackWind", false);
    }
    public void SPAttackNormalSwordStarted()
    {
        spaNormalSword = true;
        imAttacking.RuntimeValue = true;
        animator.SetBool("spaNS", false);
        cancelMovement = 0;
        strongAttackSound.Play();
    }
    public void SPAttackWindSwordStarted()
    {
        //rememberGravity = Physics2D.gravity;
        //Physics2D.gravity *= 0;        
        rememberGravity = this.Rigidbody2D.gravityScale;
        collider.isTrigger = true;
        this.Rigidbody2D.gravityScale *= 0;
        spaWindSword = true;
        dashing = true;
        dashing2.RuntimeValue = true;
        imAttacking.RuntimeValue = true;
        animator.SetBool("spaWS", false);
        rememberOriginalPositionForSpaw = transform.position;
        cancelMovement = 0;
    }
    public void EndSPAttackNormalSword()
    {
        spaNormalSword = false;
        imAttacking.RuntimeValue = false;
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
        applyingScrollBoost = false;
        horizontal = 0;
    }
    public void EndDash()
    {
        //Debug.Log(Time.time - dashTime);
        dashing = false;
        dashingS = false;
        dashing2.RuntimeValue = false;
        speed /= dashSpeedBoost;
        collider.isTrigger = false;
        horizontal = 0;
        this.Rigidbody2D.gravityScale = rememberGravity;
        this.Rigidbody2D.constraints = RigidbodyConstraints2D.None;
        this.Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

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
        //dashingAudio.time = 0;
        //dashingAudio.Play();
        transform.position = rememberOriginalPositionForSpaw;

    }    
    public void DashToSecondPoint()
    {
        dashingAudio.time = 0;
        dashingAudio.Play();
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
        dashingAudio.time = 0;
        dashingAudio.Play();
        transform.position = rememberPositionForSpaw;
    }    
    public void RechargeLoop()
    {
        animator.SetBool("rechargingLoop", true);
        animator.SetBool("recharging", false);
    }

    public void Damage(AttackDetails attackDetails)
    {
        if(attackDetails.type == TypeDamage.TEMPORAL || !dashing)
        {
            
            if (dodging && attackDetails.type == TypeDamage.NORMAL)
            {
                if (Time.time - lastParry <= 0.2f)
                {
                    //Debug.Log("Parry");
                    attackDetails.whoHitted.SendMessage("Stun");
                    parrySound.Play();
                }
                else
                {
                    //Debug.Log("DamageMitigated");
                    playerHealth.RuntimeValue -= attackDetails.damageAmount / 2;
                    semiParrySound.Play();
                }
            }
            else
            {
                playerHealth.RuntimeValue -= attackDetails.damageAmount;
                if (!intoxicated.RuntimeValue)
                {
                    takeDamage.Play();  
                }
                else
                {
                    takeDamagePoison.Play();
                }
                if (attackDetails.type == TypeDamage.NORMAL && !collider.isTrigger)
                {
                    if (this.Rigidbody2D.gravityScale == 0)
                    {
                        this.Rigidbody2D.gravityScale = rememberGravity;
                    }
                    knockBack.Set(attackDetails.knockbackForce.x * -Mathf.Sign(attackDetails.position.x - transform.position.x), attackDetails.knockbackForce.y);
                    Rigidbody2D.velocity = knockBack;
                    
                }

            }
        }
        
       

    }    

    private void TriggerDefaultAttack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(defaultAttack.transform.position, values.defaultAttackRadius, values.whatIsEnemy);

        
        infoMessage.damage = values.defaultAttackDamage;
        infoMessage.position = defaultAttack.transform.position;
        infoMessage.hoop = true;
        infoMessage.objectTag = this.gameObject.tag;

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
            collider.transform.SendMessage("Damage",infoMessage);
            
        }
    }
    private void TriggerDefaultWindAttack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(defaultAttackWind.transform.position, values.defaultWindAttackRadius, values.whatIsEnemy);

        infoMessage.damage = values.defaultWindAttackDamage;
        infoMessage.position = defaultAttackWind.transform.position;
        infoMessage.hoop = true;
        infoMessage.objectTag = this.gameObject.tag;

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
            collider.transform.SendMessage("Damage", infoMessage);
        }
    }
    private void TriggerSpecialAttack()
    {
        

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(specialAttack.transform.position, values.specialAttackRadius, values.whatIsEnemy);

        infoMessage.damage = values.specialAttackDamage;
        infoMessage.position = specialAttack.transform.position;
        infoMessage.hoop = true;
        infoMessage.objectTag = this.gameObject.tag;

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
            collider.transform.SendMessage("Damage", infoMessage);
        }

    }
    private void TriggerSpecialWindAttack()
    {
        windAttackSound.Play();
        Collider2D[] detectedObjects;
        switch (values.specialAttackStep)
        {
            case 0:
                detectedObjects = Physics2D.OverlapCircleAll(specialWindAttack[0].transform.position, values.defaultAttackRadius, values.whatIsEnemy);

                infoMessage.damage = values.specialWindAttackDamage;
                infoMessage.position = specialWindAttack[0].transform.position;
                infoMessage.hoop = false;
                infoMessage.objectTag = this.gameObject.tag;

                foreach (Collider2D collider in detectedObjects)
                {
                    collider.transform.SendMessage("Damage", infoMessage);
                }
                values.specialAttackStep++;
                break;
            case 1:
                detectedObjects = Physics2D.OverlapCircleAll(specialWindAttack[1].transform.position, values.defaultAttackRadius, values.whatIsEnemy);

                infoMessage.damage = values.specialWindAttackDamage;
                infoMessage.position = specialWindAttack[1].transform.position;
                infoMessage.hoop = false;
                infoMessage.objectTag = this.gameObject.tag;

                foreach (Collider2D collider in detectedObjects)
                {
                    collider.transform.SendMessage("Damage", infoMessage);
                }
                values.specialAttackStep++;
                break;
            case 2:
                detectedObjects = Physics2D.OverlapCircleAll(specialWindAttack[2].transform.position, values.defaultAttackRadius, values.whatIsEnemy);

                infoMessage.damage = values.specialWindAttackDamage;
                infoMessage.position = specialWindAttack[2].transform.position;
                infoMessage.hoop = true;
                infoMessage.objectTag = this.gameObject.tag;

                foreach (Collider2D collider in detectedObjects)
                {
                    //collider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                    collider.transform.SendMessage("Damage", infoMessage);
                }
                values.specialAttackStep = 0;
                break;
            default:
                values.specialAttackStep = 0;
                break;
        }      
        
    }
    public void OnDrawGizmos()
    {
        
        //Gizmos.DrawWireSphere(defaultAttack.transform.position, values.defaultAttackRadius);
        //Gizmos.DrawWireSphere(specialAttack.transform.position, values.specialAttackRadius);
        //Gizmos.DrawWireSphere(specialWindAttack[0].transform.position, values.specialWindAttackRadius);
        //Gizmos.DrawWireSphere(specialWindAttack[1].transform.position, values.specialWindAttackRadius);
        Gizmos.DrawWireSphere(specialWindAttack[2].transform.position, values.specialWindAttackRadius);        
        Gizmos.DrawCube(colliderForDashes.bounds.center + new Vector3(0.3f, 0, 0) * Mathf.Sign(transform.localScale.x), colliderForDashes.bounds.size - new Vector3(0, 0.2f));

    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            applyKnockBack.RuntimeValue = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            applyKnockBack.RuntimeValue = true;
        }
    }
    void CheckCollision()
    {
        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapBoxAll(colliderForDashes.bounds.center + new Vector3(0.3f, 0, 0) * Mathf.Sign(transform.localScale.x), colliderForDashes.bounds.size - new Vector3(0, 0.2f), 0);

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider.gameObject.CompareTag("MapLimit"))
            {
                Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;                
                break;
            }
        }
    }
    void CheckCollisionInAir()
    {
        Collider2D[] detectedObjects;
        detectedObjects = Physics2D.OverlapBoxAll(colliderForDashes.bounds.center + new Vector3(0.3f, 0, 0) * Mathf.Sign(transform.localScale.x), colliderForDashes.bounds.size, 0);

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider.gameObject.CompareTag("MapLimit"))
            {
                horizontal = 0;
                break;
            }
            else if (collider.gameObject.CompareTag("Enemy") && collider.attachedRigidbody.bodyType == RigidbodyType2D.Kinematic)
            {
                horizontal = 0;
                break;
            }
        }
    }
    private void IntoxicatedColor()
    {
        float r =0f;
        float g = 0.7f;
        float b = 0f;
        float a = 1f;

        Color intoxicated = new Color(r, g, b, a);


        this.gameObject.GetComponent<SpriteRenderer>().color = intoxicated;
    }
    public void OriginalColor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        
    }    
}
