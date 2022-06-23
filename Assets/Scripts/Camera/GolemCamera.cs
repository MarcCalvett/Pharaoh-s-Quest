using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GolemCamera : MonoBehaviour
{
    public PlayerScript player;
    public bool CanSeeLand;
    private Vector3 auxiliar;
    public bool playerInRange;
    public Vector2 leftRightLimits;
    public float cameraYPosition;
    
    public BoolValue cameraShakeOn;
    [SerializeField]
    AudioSource lvl2Music;
    [SerializeField]
    FloatValue musicVolume;
    [SerializeField]
    AudioSource golemMusic;
    [SerializeField]
    BoolValue paused;
    [SerializeField]
    BoolValue swordsTaken;
    [SerializeField]
    BoolValue golemAlive;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    AudioSource buttonEffect;
    [SerializeField]
    FloatValue timeLvl2Music;
    [SerializeField]
    FloatValue timeGolemMusic;
    [SerializeField]
    AudioSource earthquakeSound;
    [SerializeField]
    BoolValue lastSwordState;


    // Start is called before the first frame update
    void Start()
    {
        auxiliar.x = leftRightLimits.x;
        auxiliar.y = cameraYPosition;
        auxiliar.z = -10f;

        transform.position = auxiliar;        
    }
    
    private void OnEnable()
    {
        lvl2Music.time = timeLvl2Music.RuntimeValue;
        golemMusic.time = timeGolemMusic.RuntimeValue;
    }   
        
    
    //-8.67
    // Update is called once per frame
    void Update()
    {

        earthquakeSound.volume = effectsVolume.RuntimeValue * 1f;

        if(swordsTaken.RuntimeValue && !lastSwordState.RuntimeValue)
        {
            earthquakeSound.Play();
        }

        if (earthquakeSound.isPlaying && paused.RuntimeValue)
        {
            earthquakeSound.Pause();
        }
        if (!earthquakeSound.isPlaying && !paused.RuntimeValue && earthquakeSound.time != 0f)
        {
            earthquakeSound.UnPause();
        }

        if (golemAlive.RuntimeValue && !swordsTaken.RuntimeValue) //Viu sense espases
        {
            if (paused.RuntimeValue && lvl2Music.isPlaying)
            {
                lvl2Music.Pause();
            }
            else if (!paused.RuntimeValue && !lvl2Music.isPlaying)
            {
                lvl2Music.Play();
            }
        }
        else if(golemAlive.RuntimeValue && swordsTaken.RuntimeValue) //Viu amb espases
        {
            if (lvl2Music.isPlaying)
            {
                lvl2Music.Pause();
            }
            if (paused.RuntimeValue && golemMusic.isPlaying)
            {
                golemMusic.Pause();
            }
            else if (!paused.RuntimeValue && !golemMusic.isPlaying)
            {
                golemMusic.Play();
            }
        }
        else if (!golemAlive.RuntimeValue)
        {
            if (golemMusic.isPlaying)
            {
                golemMusic.Pause();
            }
            if (paused.RuntimeValue && lvl2Music.isPlaying)
            {
                lvl2Music.Pause();
            }
            else if (!paused.RuntimeValue && !lvl2Music.isPlaying)
            {
                lvl2Music.Play();
            }
        }

        lvl2Music.volume = 1 * musicVolume.RuntimeValue;
        golemMusic.volume = 1 * musicVolume.RuntimeValue;
        buttonEffect.volume = 1 * effectsVolume.RuntimeValue;

        timeLvl2Music.RuntimeValue = lvl2Music.time;
        timeGolemMusic.RuntimeValue = golemMusic.time;

        if (cameraShakeOn.RuntimeValue)
        {
            CameraShaker.Instance.ShakeOnce(0.5f,4f,.1f,2f);
            
        }
        else
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

        lastSwordState.RuntimeValue = swordsTaken.RuntimeValue;
    }
    public void ButtonOn()
    {
        buttonEffect.Play();
    }
    public void StopMusic()
    {
        lvl2Music.Stop();
    }
}
