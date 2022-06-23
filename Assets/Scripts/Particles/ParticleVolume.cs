using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleVolume : MonoBehaviour
{
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;
    [SerializeField]
    AudioSource bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bloodEffect.volume = 1f * effectsVolume.RuntimeValue;

        if(bloodEffect.isPlaying && gamePaused.RuntimeValue)
        {
            bloodEffect.Pause();
        }

        if(!bloodEffect.isPlaying && !gamePaused.RuntimeValue && bloodEffect.time != 0)
        {
            bloodEffect.UnPause();
        }
    }
}
