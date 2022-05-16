using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2script : MonoBehaviour
{
    Vector2 minSpot;
    Vector2 maxSpot;

    [SerializeField]
    GameObject player;

    private void Start()
    {
        minSpot = new Vector2(3.84f, 6.73f);
        maxSpot = new Vector2(3.84f, 6.73f);

        transform.position = minSpot;
    }
    private void FixedUpdate()
    {
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
            if(player.transform.position.x <= minSpot.x || player.transform.position.x >= maxSpot.x)
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
