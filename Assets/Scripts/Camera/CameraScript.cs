using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerScript player;
    public bool CanSeeLand;
    private Vector3 auxiliar;
    public bool playerInRange;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerScript>().spaWindSword)
        {
            Vector3 position = transform.position;
            position.x = player.transform.position.x;
            position.y = player.transform.position.y;

            transform.position = position;
        }    
                

    }

}
