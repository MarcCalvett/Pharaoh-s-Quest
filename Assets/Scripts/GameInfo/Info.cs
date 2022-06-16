using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    [SerializeField]
    BoolValue[] boolInfo;
    [SerializeField]
    FloatValue[] floatInfo;
    [SerializeField]
    IntValue[] intInfo;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            foreach(FloatValue value in floatInfo)
            {
                value.RuntimeValue = value.initialValue;
            }
            foreach (BoolValue value in boolInfo)
            {
                value.RuntimeValue = value.initialValue;
            }
            foreach (IntValue value in intInfo)
            {
                value.RuntimeValue = value.initialValue;
            }
        }
    }
}
