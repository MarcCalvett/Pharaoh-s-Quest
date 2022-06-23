using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MummyHealthBar : MonoBehaviour
{
    public Image barraDeVida;
    public Image barraDamage;
    private float updateTime;
    public FloatValue MummyHealth;


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
        MummyHealth.RuntimeValue = Mathf.Clamp(MummyHealth.RuntimeValue, 0, MummyHealth.initialValue);

        if (barraDeVida.fillAmount == 0)
        {
            Destroy(this.gameObject);
        }

        barraDeVida.fillAmount = MummyHealth.RuntimeValue / 200;

        if (barraDamage.fillAmount > barraDeVida.fillAmount && Time.time > updateTime + 0.02f)
        {
            barraDamage.fillAmount -= 0.02f;
            updateTime = Time.time;
        }


    }
}
