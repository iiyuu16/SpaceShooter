using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu menuScreenController;
    public GameObject mainMenuScreen;

    void Awake()
    {
       if(menuScreenController == null)
       {
            menuScreenController = this;
       } 
       else if(menuScreenController != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SpaceShooter");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
