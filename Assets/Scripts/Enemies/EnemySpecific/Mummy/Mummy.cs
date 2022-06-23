using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mummy : Entity
{
    public BoolValue imAlive;
    public GameObject Head;
    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject Body;

    //[HideInInspector]
    //public Vector3 HeadPosTracker;
    //[HideInInspector]
    //public Vector3 RightHandPosTracker;
    //[HideInInspector]
    //public Vector3 LeftHandPosTracker;
    //[HideInInspector]
    //public Vector3 BodyPosTracker;

    public M_ClappHandsState clappHandsState { get; private set; }
    public M_IddleState iddleState { get; private set; }
    public M_AwakingState awakingState { get; private set; }
    public M_SleepingState sleepingState { get; private set; }
    public M_CombinedAttackState combinedAttack { get; private set; }
    public M_BouncingBodyState bouncingBodyState { get; private set; }


    [SerializeField]
    private D_ClappHandsState clappHandsStateData;
    [SerializeField]
    public D_IddleState iddleStateData;
    [SerializeField]
    private D_AwakingState awakingStateData;
    [SerializeField]
    private D_SleepingStateData sleepingStateData;
    [SerializeField]
    private D_CombinateAttackStateData combinateAttackStateData;
    [SerializeField]
    private D_BouncingBodyStateData bouncingBodyStateData;

    [SerializeField]
    public FloatValue health;
    [SerializeField]
    public GameObject healthBar;

    [HideInInspector]
    public AttackDetails attackDetails;

    private float deathTimeCtr;
    private bool deathCount;

    

    public AudioSource awakeSound;
    public AudioSource DarkMagicAwaking;


    public override void Start()
    {
        base.Start();

        clappHandsState = new M_ClappHandsState(this, stateMachine, "clap", clappHandsStateData, this);
        iddleState = new M_IddleState(this, stateMachine, "idle", iddleStateData, this);
        awakingState = new M_AwakingState(this, stateMachine, "awaking", awakingStateData, this);
        sleepingState = new M_SleepingState(this, stateMachine, "sleeping", sleepingStateData, this);
        combinedAttack = new M_CombinedAttackState(this, stateMachine, "combined", combinateAttackStateData, this);
        bouncingBodyState = new M_BouncingBodyState(this, stateMachine, "bouncing", bouncingBodyStateData, this);

        deathTimeCtr = 0;

        stateMachine.Initialize(sleepingState);

        currentHealth = health.initialValue;

        deathCount = false;



    }

    public override void Update()
    {
        base.Update();

        awakeSound.volume = effectsVolume.RuntimeValue * 1f;
        DarkMagicAwaking.volume = effectsVolume.RuntimeValue;

        Debug.Log(Time.time - deathTimeCtr);
        if(Time.time - deathTimeCtr >= 3 && deathCount)
        {
            SceneManager.LoadScene("Credits");
        }
        health.RuntimeValue = currentHealth;

        if (stateMachine.currentState != sleepingState)
        {
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }

         if (deathCount && Time.time - deathTimeCtr >= 3f)
        {
            
            //this.gameObject.SetActive(false);
        }

        if (currentHealth <= 0)
        {
            if(!deathCount)
            {
                deathTimeCtr = Time.time;
                Head.SendMessage("Death");
                LeftHand.SendMessage("Death");
                RightHand.SendMessage("Death");
                Body.SendMessage("Death");
                deathTimeCtr = Time.time;
                deathCount = true;
                //SceneManager.LoadScene("Credits");
            }
           
            
        }

        if (awakeSound.isPlaying && gamePaused.RuntimeValue)
        {
            awakeSound.Pause();
        }
        if (!awakeSound.isPlaying && !gamePaused.RuntimeValue && awakeSound.time != 0f)
        {
            awakeSound.UnPause();
        }

        if (DarkMagicAwaking.isPlaying && gamePaused.RuntimeValue)
        {
            DarkMagicAwaking.Pause();
        }
        if (!DarkMagicAwaking.isPlaying && !gamePaused.RuntimeValue && DarkMagicAwaking.time != 0f)
        {
            DarkMagicAwaking.UnPause();
        }


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void SetVelocity(float velocity)
    {
        base.SetVelocity(velocity);
    }

    public override bool CheckWall()
    {
        return base.CheckWall();
    }

    public override bool CheckLedge()
    {
        return base.CheckLedge();
    }

    public override bool CheckPlayerInMinAgroRange()
    {
        return base.CheckPlayerInMinAgroRange();
    }

    public override bool CheckPlayerInMaxAgroRange()
    {
        return base.CheckPlayerInMaxAgroRange();
    }

    public override bool CheckPlayerInCloseRangeAction()
    {
        return base.CheckPlayerInCloseRangeAction();
    }

    public override bool CheckGround()
    {
        return base.CheckGround();
    }

    public override void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        base.SetVelocity(velocity, angle, direction);
    }

    public override Vector2 RetSetVelocity(float velocity, Vector2 angle, int direction)
    {
        return base.RetSetVelocity(velocity, angle, direction);
    }

    public override void DamageHop(float velocity)
    {
        base.DamageHop(velocity);
    }

    public override void Damage(InformationMessageSource informationMessage)
    {
        base.Damage(informationMessage);
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    public override void BuildingEnded()
    {
        base.BuildingEnded();
    }

    public override void Stun()
    {
        base.Stun();
    }

    public override void StopStun()
    {
        base.StopStun();
    }

    // Start is called before the first frame update

}
