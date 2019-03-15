using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    private bool bOptionsIsActive = false;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Set4Player()
    {
        PlayerPrefs.SetInt("Is4Player", 1);
        PlayerPrefs.Save();
    }

    public void Set2Player()
    {
        PlayerPrefs.SetInt("Is4Player", 0);
        PlayerPrefs.Save();
    }
}
