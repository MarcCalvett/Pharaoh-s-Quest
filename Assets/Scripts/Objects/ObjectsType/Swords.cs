using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : Interactable
{
    public BoolValue swordsTaken;
    public BoolValue cameraShake;
    public AudioSource bossMusic;
    
    void Update()
    {
        if (swordsTaken.RuntimeValue)
        {
            Destroy(this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.B) && playerInRange)
        {
            swordsTaken.RuntimeValue = true;
            cameraShake.RuntimeValue = true;
            bossMusic.Play();
            Destroy(this.gameObject);
        }
    }
    


}
