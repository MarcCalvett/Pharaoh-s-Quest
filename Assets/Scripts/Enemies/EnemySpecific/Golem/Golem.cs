using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Entity
{
    [SerializeField]
    protected BoolValue swordsTaken;
    [SerializeField]
    protected BoolValue imAlive;
    

    public override void Start()
    {
        base.Start();

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
