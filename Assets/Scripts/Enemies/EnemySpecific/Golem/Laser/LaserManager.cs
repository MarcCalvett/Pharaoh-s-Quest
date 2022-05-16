using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField]
    GameObject laserBeam;
    private void Start()
    {
        laserBeam.SetActive(false);
    }

    void LaserOn()
    {
        laserBeam.SetActive(true);
    }
    void LaserOff()
    {
        laserBeam.SetActive(false);
    }
}
