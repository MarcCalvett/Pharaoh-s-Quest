using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    //public GameObject pauseMenu;
    public GameObject[] menuParts;
    public bool isPaused;
    public KeyCode pauseKey;
    public GameObject[] UI;
    [SerializeField]
    BoolValue gamePaused;

    void Start()
    {
        //pauseMenu.SetActive(false);
        foreach (GameObject part in menuParts)
        {
            part.SetActive(false);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        gamePaused.RuntimeValue = true;

        //pauseMenu.SetActive(true);
        foreach (GameObject part in menuParts)
        {
            part.SetActive(true);
        }
        foreach (GameObject part in UI)
        {
            part.SetActive(false);
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        gamePaused.RuntimeValue = false;

        //pauseMenu.SetActive(false);
        foreach (GameObject part in menuParts)
        {
            part.SetActive(false);
        }
        foreach (GameObject part in UI)
        {
            part.SetActive(true);
        }
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;        
        StartCoroutine(BackToMenu());
        //gamePaused.RuntimeValue = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator BackToMenu()
    {        
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
