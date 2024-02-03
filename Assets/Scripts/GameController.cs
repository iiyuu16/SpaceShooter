using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public AudioSource explosionSFX;
    public AudioSource hitSFX;
    public AudioSource gameOverSFX;
    public GameObject gameOverScreen;
    public GameObject gamePauseScreen;
    private bool isPaused = false;

    void Awake()
    {
        if(gameController == null)
        {
            gameController = this;
        }
        else if(gameController != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void PlaySFX1()
    {
        explosionSFX.Play();
    }

    public void PlaySFX2()
    {
        hitSFX.Play();
    }

    public void GameOver()
    {
        EnemySpawner.enemySpawner.gameObject.SetActive(false);
        PlayerController.playerController.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverSFX.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        if(gamePauseScreen != null)
        {
            gamePauseScreen.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (gamePauseScreen != null)
        {
            gamePauseScreen.SetActive(false);
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
            ResumeGame();
    }

}
