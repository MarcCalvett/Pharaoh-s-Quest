using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Entity
{
    [SerializeField]
    private GameObject laser;
    
    public BoolValue swordsTaken;
    
    public BoolValue imAlive;

    public BoolValue cameraShake;

    public float timeShakeDuration;

    public float timeArmController;

    public bool firstLoop = true;

    public G_BeamState beamState { get; private set; }
    public G_ProtectState protectState { get; private set; }
    public G_SpikeState spikesState { get; private set; }
    public G_ThrowPunch throwPunchState { get; private set; }
    public G_WaitState waitState { get; private set; }
    public G_SleepState sleepState { get; private set; }
    public G_AwakeState awakeState { get; private set; }
    public G_DeadState deadState { get; private set; }

    [SerializeField]
    private D_BeamState beamStateData;
    [SerializeField]
    private D_ProtectState protectStateData;
    [SerializeField]
    private D_SpikesState spikesStateData;
    [SerializeField]
    private D_ThrowPunchState throwPunchStateData;
    [SerializeField]
    private D_WaitState waitStateData;
    [SerializeField]
    private D_SleepState sleepStateData;
    [SerializeField]
    private D_AwakeState awakeStateData;
    [SerializeField]
    private D_DeadState deadStateData;    

    [SerializeField]
    BoolValue laserOn;
    [SerializeField]
    private GameObject ArmMissile;
    public GameObject spikes;
    [SerializeField]
    FloatValue laserDirection;
    [SerializeField]
    FloatValue health;
    [SerializeField]
    public GameObject healthBar;


    public AudioSource laserAudio;
    public AudioSource protectAudio;  
    public AudioSource despertarAudio;
    public AudioSource golemIdleAudio;
    public AudioSource missileLaunchAudio;
    //public AudioSource spikesGrowthAudio;



    public override void Start()
    {
        base.Start();

        laser.SetActive(false);

        beamState = new G_BeamState(this, stateMachine, "beam", beamStateData, this);
        waitState = new G_WaitState(this, stateMachine, "wait", waitStateData, this);
        protectState = new G_ProtectState(this, stateMachine, "protected", protectStateData, this);
        spikesState = new G_SpikeState(this, stateMachine, "spikes", spikesStateData, this);
        throwPunchState = new G_ThrowPunch(this, stateMachine, "punch", throwPunchStateData, this);
        awakeState = new G_AwakeState(this, stateMachine, "awaked", awakeStateData, this);
        sleepState = new G_SleepState(this, stateMachine, "Sleep", sleepStateData, this);
        deadState = new G_DeadState(this, stateMachine, "dead", deadStateData, this);


        if (imAlive.RuntimeValue)
        {
            stateMachine.Initialize(sleepState);
        }
        else
        {
            Destroy(this.gameObject);
        }
        currentHealth = health.initialValue;


        Flip();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void Update()
    {
        base.Update();

        Debug.Log(stateMachine.currentState);

        despertarAudio.volume = effectsVolume.RuntimeValue * 1f;

        if (despertarAudio.isPlaying && gamePaused.RuntimeValue)
        {
            despertarAudio.Pause();
        }
        if (!despertarAudio.isPlaying && !gamePaused.RuntimeValue && despertarAudio.time != 0f)
        {
            despertarAudio.UnPause();
        }

        golemIdleAudio.volume = effectsVolume.RuntimeValue * 1f;

        if (golemIdleAudio.isPlaying && gamePaused.RuntimeValue)
        {
            golemIdleAudio.Pause();
        }
        if (!golemIdleAudio.isPlaying && !gamePaused.RuntimeValue && golemIdleAudio.time != 0f)
        {
            golemIdleAudio.UnPause();
        }

        laserAudio.volume = effectsVolume.RuntimeValue * 1f;

        if (laserAudio.isPlaying && gamePaused.RuntimeValue)
        {
            laserAudio.Pause();
        }
        if (!laserAudio.isPlaying && !gamePaused.RuntimeValue && laserAudio.time != 0f)
        {
            laserAudio.UnPause();
        }

        missileLaunchAudio.volume = effectsVolume.RuntimeValue * 1f;

        if (missileLaunchAudio.isPlaying && gamePaused.RuntimeValue)
        {
            missileLaunchAudio.Pause();
        }
        if (!missileLaunchAudio.isPlaying && !gamePaused.RuntimeValue && missileLaunchAudio.time != 0f)
        {
            missileLaunchAudio.UnPause();
        }

        protectAudio.volume = effectsVolume.RuntimeValue * 1f;

        if (protectAudio.isPlaying && gamePaused.RuntimeValue)
        {
            protectAudio.Pause();
        }
        if (!protectAudio.isPlaying && !gamePaused.RuntimeValue && protectAudio.time != 0f)
        {
            protectAudio.UnPause();
        }

        if (currentHealth <= 0)
        {
            imAlive.RuntimeValue = false;
            stateMachine.ChangeState(deadState);
        }

        if(stateMachine.currentState == protectState)
        {
            imProtected = true;
        }
        else
        {
            imProtected = false;
        }

        if (swordsTaken.RuntimeValue)
        {
            imSleep = false;
            
            healthBar.SetActive(true);
        }
        else
        {
            imSleep = true;
        }

        if(stateMachine.currentState == deadState && laserOn.RuntimeValue)
        {
            laserOn.RuntimeValue = false;
        }
        //Debug.Log(CheckPlayerInCloseRangeAction());

        health.RuntimeValue = currentHealth;

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void BuildingEnded()
    {
        base.BuildingEnded();
    }    

    public override bool CheckPlayerInCloseRangeAction()
    {
        return base.CheckPlayerInCloseRangeAction();
    }

    public override bool CheckPlayerInMaxAgroRange()
    {
        return base.CheckPlayerInMaxAgroRange();
    }

    public override bool CheckPlayerInMinAgroRange()
    {
        return base.CheckPlayerInMinAgroRange();
    }

    public override bool CheckWall()
    {
        return base.CheckWall();
    }

    public override void Damage(InformationMessageSource informationMessage)
    {
        base.Damage(informationMessage);
        //hitAudio.Play();
    }

    public override void DamageHop(float velocity)
    {
        base.DamageHop(velocity);
    }    

    public override void Flip()
    {
        base.Flip();
    }   

    public override Vector2 RetSetVelocity(float velocity, Vector2 angle, int direction)
    {
        return base.RetSetVelocity(velocity, angle, direction);
    }    

    public override void StopStun()
    {
        base.StopStun();
    }

    public override void Stun()
    {
        base.Stun();
    }
    public void Awaked()
    {
        stateMachine.ChangeState(waitState);
    }
    public void ShootArmMissile()
    {
        Vector3 armOrigin = new Vector3(4f, -0.87f, 0);
        GameObject Arm = Instantiate(ArmMissile, armOrigin, Quaternion.identity);
            
    }
    public void BackToWait()
    {       

        stateMachine.ChangeState(waitState);
    }
    public void GoProtLoop()
    {
        anim.SetBool("goDefenseLoop", true);
    }
    public void OutProtLoop()
    {
        anim.SetBool("goDefenseLoop", false);

    }
    public void GrowSpikes()
    {
        Vector3 spikesOrigin = new Vector3(3.51f, -3.27f, 0);
        GameObject spikesTrap = Instantiate(spikes, spikesOrigin, Quaternion.identity);
    }
    public void ShootLaser()
    {
        laserDirection.RuntimeValue = laserDirection.initialValue;
        laser.SetActive(true);
        laserOn.RuntimeValue = true;
        
    }
    public void EndLaser()
    {
        laser.SetActive(false);
        laserOn.RuntimeValue = false;
        
    }



}
