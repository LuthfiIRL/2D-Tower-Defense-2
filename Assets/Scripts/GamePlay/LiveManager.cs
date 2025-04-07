using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveManager : MonoBehaviour
{
    public int maxLives = 4;
    public int currentLives;
    private bool isDead;

    public TextMeshProUGUI livesNumber;

    public GameOver gameOver;

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();        
    }

    public void ReduceLives(int amount)
    {
        currentLives -= amount;
        if (currentLives < 0)
        {
            currentLives = 0;
        }
        UpdateLivesUI();        

        if (currentLives <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("GAME OVER");
            gameOver.GameOverLogic();
        }

    }

    private void UpdateLivesUI()
    {
        livesNumber.text = currentLives.ToString();
    }
    
}

