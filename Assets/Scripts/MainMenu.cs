using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource BGSource;
    public AudioClip menuBGM;
    public static MainMenu menuScreenController;
    public GameObject mainMenuScreen;

    private void Start()
    {
        BGSource.clip = menuBGM;
        BGSource.Play();
    }

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
        SceneManager.LoadScene("1P_SpaceShooter");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
