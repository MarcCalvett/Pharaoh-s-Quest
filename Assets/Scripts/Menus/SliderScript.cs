using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private FloatValue Volume;
    // Start is called before the first frame update
    void Start()
    {
        _slider.value = Volume.RuntimeValue;
        _slider.onValueChanged.AddListener((v) => {
        Volume.RuntimeValue = v;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
