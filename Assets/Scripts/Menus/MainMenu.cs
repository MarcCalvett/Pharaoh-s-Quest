using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource button;
    [SerializeField]
    FloatValue effectsVolume;
    [SerializeField]
    BoolValue gamePaused;

    private void Start()
    {
        gamePaused.RuntimeValue = false;
    }
    private void Update()
    {
        button.volume = effectsVolume.RuntimeValue * 1f;
    }
    public void ExitButton()
    {
        StartCoroutine(QuitFunction());        
    }
    private IEnumerator QuitFunction()
    {
        Debug.Log("THANK YOU FOR PLAYING, SEE YOU SOON!");
        button.Play();
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    public void StartGame()
    {
        StartCoroutine(PlayFunction());        
    }
    private IEnumerator PlayFunction()
    {
        button.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("DesertProject");
    }
}
