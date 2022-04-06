using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;

    private Transfrom cameraTransform;
    private Vector3 previousCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
<<<<<<< Updated upstream
        float diferenceX = (cameraTransform.position.x - previousCameraPosition.x);
        transform.Translate(new Vector3(diferenceX, 0, 0));
=======
        float xIncrease = (cameraTransform.position.x - previousCameraPosition.x)*parallaxMultiplier;
        transform.Translate(new Vector3(xIncrease, 0, 0));
>>>>>>> Stashed changes
        previousCameraPosition = cameraTransform.position;
    }
}
