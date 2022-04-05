using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        float diferenceX=(cameraTransform.position.x - previousCameraPosition.x)
        transform.Translate(new Vector3(diferenceX, 0, 0));
        previousCameraPosition = cameraTransform.postion;
    }
}
