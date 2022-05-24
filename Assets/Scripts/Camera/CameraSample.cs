using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSample : MonoBehaviour
{
    Vector2 minSpot;
    Vector2 maxSpot;

    [SerializeField]
    GameObject player;

    
    private void FixedUpdate()
    {
        if(player.transform.position.y <= -6.9)
        {
            minSpot = new Vector2(30.79f, -1.24f);
            maxSpot = new Vector2(30.79f, -1.24f);
        }
        else
        {
            minSpot = new Vector2(-43.03f, 1);
            maxSpot = new Vector2(42.8f, 1);
        }        
        Vector3 aux;
        Vector3 nextPos = player.transform.position;
        nextPos.z = -10;

        if (player.transform.position.x > minSpot.x && player.transform.position.x < maxSpot.x && player.transform.position.y > minSpot.y && player.transform.position.y < maxSpot.y)
        {
            Vector3 aux2 = player.transform.position;
            aux2.z = -10;
            transform.position = aux2;
        }
        else
        {
            if (player.transform.position.x <= minSpot.x || player.transform.position.x >= maxSpot.x)
            {
                if (player.transform.position.x <= minSpot.x)
                {
                    aux.x = minSpot.x;
                    nextPos.x = aux.x;
                }
                else if (player.transform.position.x >= maxSpot.x)
                {
                    aux.x = maxSpot.x;
                    nextPos.x = aux.x;
                }
            }
            if (player.transform.position.y <= minSpot.y || player.transform.position.y >= maxSpot.y)
            {
                if (player.transform.position.y <= minSpot.y)
                {
                    aux.y = minSpot.y;
                    nextPos.y = aux.y;
                }
                else if (player.transform.position.y >= maxSpot.y)
                {
                    aux.y = maxSpot.y;
                    nextPos.y = aux.y;
                }
            }


            transform.position = nextPos;
        }
    }
}
