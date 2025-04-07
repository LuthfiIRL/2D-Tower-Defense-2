using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemiesPrefabs;    

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;
    [SerializeField] private int levelTransitionWave = 5;       

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // enemies per second
    private bool isSpawning = false;
    public GameOver gameOver;
    

    private void Awake()
    {        
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning || gameOver.gameOverUI.activeSelf) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f/eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }                        
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        if (!gameOver.gameOverUI.activeSelf)
        {
            isSpawning = true;
            enemiesLeftToSpawn = EnemiesPerWave();
            eps = EnemiesPerSecond();            
        }        
    }

    private void EndWave()
    {
        Debug.Log("current wave = "+ currentWave);
        Debug.Log("level transition wave = "+ levelTransitionWave);

        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;        

        if (currentWave == levelTransitionWave)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogWarning("No more scene to laod");
            }
        }
        else
        {
            StartCoroutine(StartWave());
        }        
        
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
    

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemiesPrefabs.Length);
        GameObject prefabToSpawn = enemiesPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}
