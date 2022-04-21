using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public int lives;
    public GameObject background;
    public GameObject gameOverText;
    public GameObject pauseMenu;

    private void Start()
    {
        lives = 3;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lives--;
            Debug.Log("Life lost!");
            Debug.Log("Lives left " + lives);
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }


    void GameOver()
    {

        background.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverText.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
