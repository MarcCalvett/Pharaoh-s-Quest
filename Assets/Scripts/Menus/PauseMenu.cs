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
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
