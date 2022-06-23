using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    AudioSource menuMusic;
    [SerializeField]
    FloatValue musicVolume;
    [SerializeField]
    AudioSource buttonEffect;
    [SerializeField]
    FloatValue effectsVolume;

    // Update is called once per frame
    void Update()
    {
        menuMusic.volume = 1 * musicVolume.RuntimeValue;
        buttonEffect.volume = 1 * effectsVolume.RuntimeValue;
    }
    public void ButtonEffectOn()
    {
        buttonEffect.Play();
    }
    public void StopMusic()
    {
        menuMusic.Stop();
    }
}
