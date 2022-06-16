using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoor : Interactable
{
    public BoolValue swordsTaken;
    public FloatValue xRespawn;
    public FloatValue yRespawn;

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.B) && playerInRange && swordsTaken.RuntimeValue)
        {
            xRespawn.RuntimeValue = 0;
            yRespawn.RuntimeValue = -3.6f;
            SceneManager.LoadScene("Lvl3");            
        }
    }
}
