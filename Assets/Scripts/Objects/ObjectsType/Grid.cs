using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private BoolValue swordsTaken;
    [SerializeField]
    private BoolValue golemAlive;
    [SerializeField]
    private Vector3 BlockPosition;
    [SerializeField]
    private Vector3 passPosition;
    [SerializeField]
    SpriteRenderer[] sprites;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;
    [SerializeField]
    AudioSource gridFallSound;
    bool soundDone;


    void Start()
    {
        transform.position = passPosition;

        //foreach (SpriteRenderer sprite in sprites)
        //{
        //    sprite.sortingOrder = -1;
        //}
        //sprites[sprites.Length - 1].sortingOrder = 1;
        //sprites[sprites.Length - 2].sortingOrder = 1;
        soundDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        gridFallSound.volume = effectsVolume.RuntimeValue * 1f;


        if(!swordsTaken || !golemAlive.RuntimeValue)
        {
            transform.position = passPosition;
            //foreach (SpriteRenderer sprite in sprites)
            //{
            //    sprite.sortingOrder = -1;
            //}
            //sprites[sprites.Length - 1].sortingOrder = 1;
            //sprites[sprites.Length - 2].sortingOrder = 1;

        }
        if(swordsTaken.RuntimeValue && golemAlive.RuntimeValue)
        {
            if (!soundDone)
            {
                gridFallSound.Play();
                soundDone = true;
            }
            transform.position = BlockPosition;
            //foreach (SpriteRenderer sprite in sprites)
            //{
            //    sprite.sortingOrder = 1;

            //}
            //sprites[0].sortingOrder = 1;
            //sprites[sprites.Length - 1].sortingOrder = 1;

        }

        if(gridFallSound.isPlaying && gamePaused.RuntimeValue)
        {
            gridFallSound.Pause();
        }
        if(!gridFallSound.isPlaying && !gamePaused.RuntimeValue && gridFallSound.time != 0)
        {
            gridFallSound.UnPause();
        }
    }
}
