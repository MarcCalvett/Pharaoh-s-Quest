using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemCamera : MonoBehaviour
{
    public PlayerScript player;
    public bool CanSeeLand;
    private Vector3 auxiliar;
    public bool playerInRange;
    public Vector2 leftRightLimits;
    public float cameraYPosition;

    // Start is called before the first frame update
    void Start()
    {
        auxiliar.x = leftRightLimits.x;
        auxiliar.y = cameraYPosition;
        auxiliar.z = -10f;

        transform.position = auxiliar;
    }
    //-8.67
    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerScript>().spaWindSword)
        {
            //if(player.transform.position.x >= leftRightLimits.x && player.transform.position.x <= leftRightLimits.y)
            //{
                if(player.transform.position.x >= -8.67f)
                {
                    auxiliar.x = leftRightLimits.y;
                    auxiliar.y = cameraYPosition;
                    auxiliar.z = -10f;

                    transform.position = auxiliar;
                }
                else
                {
                    auxiliar.x = leftRightLimits.x;
                    auxiliar.y = cameraYPosition;
                    auxiliar.z = -10f;

                    transform.position = auxiliar;
                }
                //Vector3 position = transform.position;
                //position.x = player.transform.position.x;
                //position.y = cameraYPosition;

                //transform.position = position;
            //}            
            
        }


    }
}
