using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string sceneToLoadName;
    [SerializeField]
    FloatValue xRespawn;
    [SerializeField]
    FloatValue yRespawn;
    [SerializeField]
    Vector2 nextRespawnWanted;
    


    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           

            xRespawn.RuntimeValue = nextRespawnWanted.x;
            yRespawn.RuntimeValue = nextRespawnWanted.y;

            SceneManager.LoadScene(sceneToLoadName, LoadSceneMode.Single);
        }
    }
}
