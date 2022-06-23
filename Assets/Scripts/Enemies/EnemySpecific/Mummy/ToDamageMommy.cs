using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDamageMommy : MonoBehaviour
{
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;
    public GameObject damageReceiver;   
    public AudioSource susurrosIdle;
    
    public FloatValue effectsVolume;
    
    public BoolValue gamePaused;

    private void Update()
    {
        susurrosIdle.volume = 1f * effectsVolume.RuntimeValue;
    }
    public void Damage(InformationMessageSource informationMessage)
    {
        damageReceiver.GetComponent<Mummy>().Damage(informationMessage);
    }
    void Death()
    {
        Vector3 aux = this.transform.position;
        aux.z = -9.5f;
        GameObject.Instantiate(deathBloodParticle, aux, deathBloodParticle.transform.rotation);
        GameObject.Instantiate(deathChunkParticle, aux, deathChunkParticle.transform.rotation);
        this.gameObject.SetActive(false);
    }
}
