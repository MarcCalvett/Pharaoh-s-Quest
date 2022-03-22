using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public IntValue heartContainers;
    public FloatValue playerCurrentHealth;
    
    // Start is called before the first frame update
    void Update()
    {
        UpdateHearts();
    }

    public void InitHearts()
    {


        for (int i = 0; i <= heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);

        }

    }
    public void UpdateHearts()
    {        
        for (int i = 0; i <= heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);

            if(i > heartContainers.RuntimeValue)
            {
                 hearts[i].sprite = emptyHeart;
            }            
            else
            {
                hearts[i].sprite = fullHeart;
            }          

        }       
    }
}
