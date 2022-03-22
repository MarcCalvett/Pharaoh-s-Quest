using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barraDeVida;
    public Image barraDamage;
    private float updateTime;
    public FloatValue playerHealth;    
    public IntValue playerLives;

    // Start is called before the first frame update
    void Start()
    {
        barraDeVida.fillAmount = 1;
        barraDamage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();
    }

    private void UpdateBars()
    {
        playerHealth.RuntimeValue = Mathf.Clamp(playerHealth.RuntimeValue, 0, playerHealth.initialValue);

        if (barraDeVida.fillAmount == 0)
        {
            playerLives.RuntimeValue--;
            if (playerLives.RuntimeValue > -1)
            {
                playerHealth.RuntimeValue = playerHealth.initialValue;
                barraDeVida.fillAmount = 1;
                barraDamage.fillAmount = 1;

            }
            else
            {
                Debug.Log("GAME OVER");
            }
        }

        barraDeVida.fillAmount = playerHealth.RuntimeValue / 100;

        if (barraDamage.fillAmount > barraDeVida.fillAmount && Time.time > updateTime + 0.02f)
        {
            barraDamage.fillAmount -= 0.02f;
            updateTime = Time.time;
        }

        
    }
}
