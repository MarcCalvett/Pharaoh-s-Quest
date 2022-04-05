using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUnbuildedStateData", menuName = "Data/StateData/UnbuildedState")]
public class D_UnbuildedState : ScriptableObject
{
    public float enterTimeState;
    public float timeBuilding = 15f;
    public bool enemyFinished = false;
}
