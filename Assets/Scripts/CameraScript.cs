using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = player.transform.position.x;
        if(transform.position.y < player.transform.position.y - 2.95f)
        {
            position.y = player.transform.position.y + 2.95f;
        }        
        transform.position = position;
    }
}

//if (transform.position.y > player.transform.position.y + 2.95f)
//{
//    position.y = player.transform.position.y - 2.95f;
//}