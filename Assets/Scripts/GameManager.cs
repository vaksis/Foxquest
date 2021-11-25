using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject health;
    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu = GameObject.Find("PanelPauseMenu");
        PauseMenu.SetActive(false);

        //Instantiate(player);
        //Instantiate(enemy);

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);

        }
        else if (Time.timeScale == 0)
        {

            Time.timeScale = 1;
            PauseMenu.SetActive(false);

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
