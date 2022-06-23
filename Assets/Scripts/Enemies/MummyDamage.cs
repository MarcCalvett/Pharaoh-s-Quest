using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyDamage : MonoBehaviour
{
    private bool applyDamage;
    private float lastTimeDamaged;
    private AttackDetails attackDetails;
    [SerializeField]
    private float hitDamage;
    [SerializeField]
    BoolValue playerDashing;
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;
    public AudioSource arrastrarLent;
    public AudioSource arrastrarRapid;
    public AudioSource copFluix;
    public AudioSource copFort;
    public AudioSource roll;
    public AudioSource botar;
    public AudioSource darkMagicFast;
    public AudioSource dobleDarkMagic;
    public AudioSource rockDarkMagic;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;

    private void Start()
    {
        applyDamage = false;
        lastTimeDamaged = 0;
    }

    private void Update()
    {
        arrastrarLent.volume = 1f * effectsVolume.RuntimeValue;
        arrastrarRapid.volume = 1f * effectsVolume.RuntimeValue;
        copFluix.volume = 1f * effectsVolume.RuntimeValue;
        copFort.volume = 1f * effectsVolume.RuntimeValue;
        roll.volume = 1f * effectsVolume.RuntimeValue;
        botar.volume = 1f * effectsVolume.RuntimeValue;
        darkMagicFast.volume = 1f * effectsVolume.RuntimeValue;
        dobleDarkMagic.volume = 1f * effectsVolume.RuntimeValue;
        rockDarkMagic.volume = 1f * effectsVolume.RuntimeValue;

        if (arrastrarLent.isPlaying && gamePaused.RuntimeValue)
        {
            arrastrarLent.Pause();
        }
        if (!arrastrarLent.isPlaying && !gamePaused.RuntimeValue && arrastrarLent.time != 0f)
        {
            arrastrarLent.UnPause();
        }

        if (arrastrarRapid.isPlaying && gamePaused.RuntimeValue)
        {
            arrastrarRapid.Pause();
        }
        if (!arrastrarRapid.isPlaying && !gamePaused.RuntimeValue && arrastrarRapid.time != 0f)
        {
            arrastrarRapid.UnPause();
        }

        if (copFluix.isPlaying && gamePaused.RuntimeValue)
        {
            copFluix.Pause();
        }
        if (!copFluix.isPlaying && !gamePaused.RuntimeValue && copFluix.time != 0f)
        {
            copFluix.UnPause();
        }

        if (copFort.isPlaying && gamePaused.RuntimeValue)
        {
            copFort.Pause();
        }
        if (!copFort.isPlaying && !gamePaused.RuntimeValue && copFort.time != 0f)
        {
            copFort.UnPause();
        }

        if (roll.isPlaying && gamePaused.RuntimeValue)
        {
            roll.Pause();
        }
        if (!roll.isPlaying && !gamePaused.RuntimeValue && roll.time != 0f)
        {
            roll.UnPause();
        }

        if (botar.isPlaying && gamePaused.RuntimeValue)
        {
            botar.Pause();
        }
        if (!botar.isPlaying && !gamePaused.RuntimeValue && botar.time != 0f)
        {
            botar.UnPause();
        }

        if (darkMagicFast.isPlaying && gamePaused.RuntimeValue)
        {
            darkMagicFast.Pause();
        }
        if (!darkMagicFast.isPlaying && !gamePaused.RuntimeValue && darkMagicFast.time != 0f)
        {
            darkMagicFast.UnPause();
        }

        if (dobleDarkMagic.isPlaying && gamePaused.RuntimeValue)
        {
            dobleDarkMagic.Pause();
        }
        if (!dobleDarkMagic.isPlaying && !gamePaused.RuntimeValue && dobleDarkMagic.time != 0f)
        {
            dobleDarkMagic.UnPause();
        }

        if (rockDarkMagic.isPlaying && gamePaused.RuntimeValue)
        {
            rockDarkMagic.Pause();
        }
        if (!rockDarkMagic.isPlaying && !gamePaused.RuntimeValue && rockDarkMagic.time != 0f)
        {
            rockDarkMagic.UnPause();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (applyDamage && collision.gameObject.CompareTag("Player") && Time.time - lastTimeDamaged >= 1f && !playerDashing.RuntimeValue)
        {
            attackDetails.damageAmount = hitDamage;
            attackDetails.knockbackForce = new Vector2(0, 0);
            attackDetails.position = this.transform.position;
            attackDetails.type = TypeDamage.TEMPORAL;
            attackDetails.whoHitted = this.GetComponent<Rigidbody2D>();

            collision.attachedRigidbody.SendMessage("Damage", attackDetails);
            lastTimeDamaged = Time.time;

            Debug.Log(playerDashing.RuntimeValue);
        }
    }    
    void ActivateDamage()
    {
        applyDamage = true;
    }
    void DOntActivateDamage()
    {
        applyDamage = false;
    }
    void ActivateTrigger()
    {
        this.GetComponent<Collider2D>().isTrigger = true;

    }
    void DontActivateTrigger()
    {
        this.GetComponent<Collider2D>().isTrigger = false;
    }

    void Death()
    {
        Vector3 aux = this.transform.position;
        aux.z = -9.5f;
        GameObject.Instantiate(deathBloodParticle, aux, deathBloodParticle.transform.rotation);
        GameObject.Instantiate(deathChunkParticle, aux, deathChunkParticle.transform.rotation);
        this.gameObject.SetActive(false);
    }

    void PlayArrossegarLent()
    {
        arrastrarLent.Play();
    }
    void PauseArrossegarLent()
    {
        arrastrarLent.Pause();
        arrastrarLent.time = 0;
    }

    void PlayArrossegarRapid()
    {
        arrastrarRapid.Play();
    }
    void PauseArrossegarRapid()
    {
        arrastrarRapid.Pause();
        arrastrarRapid.time = 0;
    }

    void PlayCopFluix()
    {
        copFluix.Play();
    }
    void PauseCopFluix()
    {
        copFluix.Pause();
        copFluix.time = 0;
    }

    void PlayCopFort()
    {
        copFort.Play();
    }
    void PauseCopFort()
    {
        copFort.Pause();
        copFort.time = 0;
    }

    void PlayBotar()
    {
        botar.Play();
    }
    void PauseBotar()
    {
        botar.Pause();
        botar.time = 0;
    }

    void PlayRoll()
    {
        roll.Play();
    }
    void PauseRoll()
    {
        roll.Pause();
        roll.time = 0;
    }

    void PlayDobleDM()
    {
        dobleDarkMagic.Play();
    }
    void PlayFastDM()
    {
        darkMagicFast.Play();
    }
    void PlayDMrock()
    {
        rockDarkMagic.Play();
    }
}
