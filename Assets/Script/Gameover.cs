using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameObject gameOverPanel;
    

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverPanel.SetActive(false);
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        Manager.Instance.RespawnPlayer(gameObject);
        gameOverPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
