using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Vector2 position;
    public float damageAmount;
    public Vector2 knockbackForce;
    public Rigidbody2D whoHitted;
    public TypeDamage type;
}
