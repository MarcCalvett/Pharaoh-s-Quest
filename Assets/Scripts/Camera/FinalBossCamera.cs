using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCamera : MonoBehaviour
{
    [SerializeField]
    AudioSource bossMusic;
    [SerializeField]
    FloatValue musicVolume;
    [SerializeField]
    BoolValue paused;
    [SerializeField]
    FloatValue timeBossMusic;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    AudioSource buttonEffect;

    // Start is called before the first frame update
    void Start()
    {
        bossMusic.time = timeBossMusic.RuntimeValue;        
    }

    // Update is called once per frame
    void Update()
    {
        bossMusic.volume = 1 * musicVolume.RuntimeValue;
        buttonEffect.volume = 1 * effectsVolume.RuntimeValue;

        if (paused.RuntimeValue && bossMusic.isPlaying)
        {
            bossMusic.Pause();
        }
        else if (!paused.RuntimeValue && !bossMusic.isPlaying)
        {
            bossMusic.Play();
        }

        timeBossMusic.RuntimeValue = bossMusic.time;
        
    }
    public void ButtonOn()
    {
        buttonEffect.Play();
    }
    public void StopMusic()
    {
        bossMusic.Stop();
    }
}
