using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject levelSelection;

    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        levelSelection.SetActive(true);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}
