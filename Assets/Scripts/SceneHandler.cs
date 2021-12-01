using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    //==========Load new level============
    public void StartGame()
    {
        SceneManager.LoadScene("World1-1");
    }

    public void SecondLevel()
    {
        SceneManager.LoadScene("World1-2");
    }

    //==========Application shutdown============
    public void ClickExit()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    }
}
