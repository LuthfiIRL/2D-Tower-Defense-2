using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public EnemySpawner enemySpawner;
    

    public void GameOverLogic()
    {        
        enemySpawner.DestroyAllEnemies();
        gameOverUI.SetActive(true);

        Time.timeScale = 0f;   
        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }    
}
