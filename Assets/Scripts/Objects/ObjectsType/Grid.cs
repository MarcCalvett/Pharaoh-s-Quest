using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private BoolValue swordsTaken;
    [SerializeField]
    private BoolValue golemAlive;
    [SerializeField]
    private Vector3 BlockPosition;
    [SerializeField]
    private Vector3 passPosition;
    void Start()
    {
        transform.position = passPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!swordsTaken || !golemAlive.RuntimeValue)
        {
            transform.position = passPosition;
        }
        if(swordsTaken.RuntimeValue && golemAlive.RuntimeValue)
        {
            transform.position = BlockPosition;
        }
    }
}
