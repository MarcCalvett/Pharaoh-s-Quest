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
    void Start()
    {
        transform.position = passPosition;

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = -1;
        }
        sprites[sprites.Length - 1].sortingOrder = 1;
        sprites[sprites.Length - 2].sortingOrder = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if(!swordsTaken || !golemAlive.RuntimeValue)
        {
            transform.position = passPosition;
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sortingOrder = -1;
            }
            sprites[sprites.Length - 1].sortingOrder = 1;
            sprites[sprites.Length - 2].sortingOrder = 1;

        }
        if(swordsTaken.RuntimeValue && golemAlive.RuntimeValue)
        {
            transform.position = BlockPosition;
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sortingOrder = 1;

            }
            sprites[0].sortingOrder = 1;
            sprites[sprites.Length-1].sortingOrder = 1;

        }
    }
}
