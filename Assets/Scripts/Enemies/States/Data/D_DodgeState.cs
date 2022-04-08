using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/StateData/DodgeState")]
public class D_DodgeState : ScriptableObject
{
    public float dodgeSpeed = 10;
    public float dodgeTime = 0.2f;
    public float dodgeCooltime = 2;
    public Vector2 dodgeAngle = new Vector2(1,1);
    public Vector2 initialVelocity;

    public bool dodging = false;

}
