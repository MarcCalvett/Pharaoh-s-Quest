using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image barraDeStamina;
    public Image barraGasto;
    private float updateTime;
    public FloatValue playerStamina;
    public BoolValue rechargingStamina;
    private float recoverTime;
   


    // Start is called before the first frame update
    void Start()
    {
        barraDeStamina.fillAmount = 1;
        barraGasto.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();
    }

    private void UpdateBars()
    {
        playerStamina.RuntimeValue = Mathf.Clamp(playerStamina.RuntimeValue, 0, playerStamina.initialValue);

        if (barraDeStamina.fillAmount == 0)
        {
            rechargingStamina.RuntimeValue = true;
        }
        else if(barraDeStamina.fillAmount == 1 && playerStamina.RuntimeValue == 100)
        {
            rechargingStamina.RuntimeValue = false;

        }
        else
        {
            barraDeStamina.fillAmount = playerStamina.RuntimeValue / 100;
        }

        if (rechargingStamina.RuntimeValue && Time.time > recoverTime + 0.02f)
        {
            playerStamina.RuntimeValue += 1f;
            barraDeStamina.fillAmount = playerStamina.RuntimeValue / 100;
            barraGasto.fillAmount = barraDeStamina.fillAmount;
            recoverTime = Time.time;
        }
        else if(!rechargingStamina.RuntimeValue && Time.time > recoverTime + 0.02f)
        {
            if (barraGasto.fillAmount > barraDeStamina.fillAmount && Time.time > updateTime + 0.02f)
            {
                barraGasto.fillAmount -= 0.02f;
                barraDeStamina.fillAmount = playerStamina.RuntimeValue / 100;
                updateTime = Time.time;
                recoverTime = Time.time;
            }
            else
            {
                playerStamina.RuntimeValue += 0.02f;                
                barraGasto.fillAmount = barraDeStamina.fillAmount;
                recoverTime = Time.time;
            }            
        }
        
        

    }
}
