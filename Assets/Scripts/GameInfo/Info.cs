using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField]
    BoolValue[] boolInfo;
    [SerializeField]
    FloatValue[] floatInfo;
    [SerializeField]
    IntValue[] intInfo;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
