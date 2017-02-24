using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {
    public GameObject menu;
    public GameObject settings;
    public void OnStartGame()
    {
        //Application.LoadLevel(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OnSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
    public void OnMenu()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }
    public void OnExit()
    {
        Application.Quit();
    }


}

