using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GolemHealthBar : MonoBehaviour
{
    public Image barraDeVida;
    public Image barraDamage;
    private float updateTime;
    public FloatValue GolemHealth;
    

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
        GolemHealth.RuntimeValue = Mathf.Clamp(GolemHealth.RuntimeValue, 0, GolemHealth.initialValue);

        if (barraDeVida.fillAmount == 0)
        {
            Destroy(this.gameObject);
        }

        barraDeVida.fillAmount = GolemHealth.RuntimeValue / 500;

        if (barraDamage.fillAmount > barraDeVida.fillAmount && Time.time > updateTime + 0.02f)
        {
            barraDamage.fillAmount -= 0.02f;
            updateTime = Time.time;
        }


    }
}
