using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyScript : MonoBehaviour
{
    [SerializeField]
    Sprite flatHand;
    [SerializeField]
    Sprite fistHand;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = flatHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpriteToFlatHand()
    {
        GetComponent<SpriteRenderer>().sprite = flatHand;
    }
    void SpriteToFistHand()
    {
        GetComponent<SpriteRenderer>().sprite = fistHand;
    }
}
