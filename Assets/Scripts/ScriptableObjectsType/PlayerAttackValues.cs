using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "newPlayerAttackValues", menuName = "Data/Player/AttackValues")]
public class PlayerAttackValues : ScriptableObject
{
    public float defaultAttackRadius = 0.5f;
    public float defaultWindAttackRadius = 0.5f;
    public float specialAttackRadius = 1.5f;
    public float specialWindAttackRadius = 1.5f;

    public float defaultAttackDamage = 10f;
    public float defaultWindAttackDamage = 8f;
    public float specialAttackDamage = 25f;
    public float specialWindAttackDamage = 50f;

    public int specialAttackStep = 0;

    public LayerMask whatIsEnemy;
}
