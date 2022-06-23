using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEOVER : MonoBehaviour
{
    [SerializeField]
    FloatValue playerHealth;
    [SerializeField]
    IntValue playerLives;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    AudioSource winSound;
    [SerializeField]
    AudioSource defeatSound;
    [SerializeField]
    AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        winSound.volume = 1f * effectsVolume.RuntimeValue;
        defeatSound.volume = 1f * effectsVolume.RuntimeValue;
        buttonSound.volume = 1f * effectsVolume.RuntimeValue;

        if(playerHealth.RuntimeValue>0 || playerLives.RuntimeValue > 0)
        {
            winSound.Play();
        }
        else
        {
            defeatSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonOn()
    {
        buttonSound.Play();
    }
}
