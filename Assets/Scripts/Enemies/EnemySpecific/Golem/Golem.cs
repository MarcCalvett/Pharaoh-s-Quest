using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Entity
{
    [SerializeField]
    protected BoolValue swordsTaken;
    [SerializeField]
    protected BoolValue imAlive;

    public G_BeamState beamState { get; private set; }
    public G_ProtectState protectState { get; private set; }
    public G_SpikeState spikesState { get; private set; }
    public G_ThrowPunch throwPunchState { get; private set; }
    public G_WaitState waitState { get; private set; }
    public G_SleepState sleepState { get; private set; }
    public G_AwakeState awakeState { get; private set; }

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

    public override void Start()
    {
        base.Start();

        beamState = new G_BeamState(this, stateMachine, "beam", beamStateData, this);
        waitState = new G_WaitState(this, stateMachine, "wait", waitStateData, this);
        protectState = new G_ProtectState(this, stateMachine, "protected", protectStateData, this);
        spikesState = new G_SpikeState(this, stateMachine, "spikes", spikesStateData, this);
        throwPunchState = new G_ThrowPunch(this, stateMachine, "punch", throwPunchStateData, this);
        awakeState = new G_AwakeState(this, stateMachine, "awaked", awakeStateData, this);
        sleepState = new G_SleepState(this, stateMachine, "sleep", sleepStateData, this);

        stateMachine.Initialize(sleepState);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void Update()
    {
        base.Update();

        if(currentHealth <= 0)
        {
            imAlive.RuntimeValue = false;
            //GO Dead
        }


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

    

    
}
