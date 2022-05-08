using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera laserCamera;
    [SerializeField]
    private Camera normalCamera;
    [SerializeField]
    BoolValue LaserOn;
    // Start is called before the first frame update
    void Start()
    {
        if (LaserOn.RuntimeValue)
        {
            laserCamera.gameObject.SetActive(true);
            normalCamera.gameObject.SetActive(false);
        }
        else
        {
            laserCamera.gameObject.SetActive(false);
            normalCamera.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LaserOn.RuntimeValue)
        {
            laserCamera.gameObject.SetActive(true);
            normalCamera.gameObject.SetActive(false);
        }
        else
        {
            laserCamera.gameObject.SetActive(false);
            normalCamera.gameObject.SetActive(true);
        }
    }
}
