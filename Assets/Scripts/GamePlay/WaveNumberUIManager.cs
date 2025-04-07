using TMPro;
using UnityEngine;

public class WaveNumberUIManager : MonoBehaviour
{
    public TMP_Text waveNumberText;
    public EnemySpawner enemySpawner;

    private void Start()
    {
        if (waveNumberText == null)
        {
            Debug.LogError("WaveNumberText not assigned in the inspector.");
        }

        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner not assigned in the inspector.");
        }
    }

    private void Update()
    {
        UpdateWaveNumber();
    }

    private void UpdateWaveNumber()
    {
        if (waveNumberText != null && enemySpawner != null)
        {
            waveNumberText.text = enemySpawner.GetCurrentWave().ToString();
        }
    }
}
