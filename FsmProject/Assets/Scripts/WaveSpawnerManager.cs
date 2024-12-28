using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawnerManager : MonoBehaviour
{
    public static WaveSpawnerManager instance;

    [Header("Enemy Type Prefabs")]
    // Define enemy prefabs or GameObjects
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;
    public GameObject enemyType4;
    public GameObject enemyType5;

    // Spawn points (e.g., empty GameObjects in your scene where enemies will spawn)
    public Transform[] spawnPoints;

    // Define wave data structure
    private List<List<GameObject>> waves = new List<List<GameObject>>();
    private int currentWaveIndex = 0;
    private int remainingEnemies = 0; // Track remaining enemies in the current wave

    // Delay between spawning each enemy
    public float spawnDelay = 1.0f;
    public float startWaveDelay;

    private PlayerEntity player;
    public TextMeshProUGUI waveText;
    public float waveDelay = 2.0f;  // Set delay in seconds between waves

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(Enum_StartWave());
    }

    IEnumerator Enum_StartWave()
    {
        yield return new WaitForSeconds(startWaveDelay);
        StartWaves();
    }

    public void StartWaves()
    {
        // Define each wave with a list of enemies
      
        waves.Add(new List<GameObject> { enemyType1, enemyType1, enemyType1 });               
        waves.Add(new List<GameObject> { enemyType1, enemyType1, enemyType1, enemyType2 });       
        waves.Add(new List<GameObject> { enemyType1, enemyType1, enemyType1, enemyType2 });                   
        waves.Add(new List<GameObject> { enemyType1, enemyType1, enemyType1, enemyType2, enemyType2 });           
        waves.Add(new List<GameObject> { enemyType1, enemyType1, enemyType1, enemyType1, enemyType2 });
        waves.Add(new List<GameObject> { enemyType3 });

        // Start the wave spawning
        StartCoroutine(SpawnWave(0.1f));
    }

    IEnumerator SpawnWave(float waveDelay)
    {

        // Check if all waves are completed
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("All waves completed!");
            StartWaves();
            yield break;
        }

        Debug.Log("Spawning Wave " + (currentWaveIndex + 1));
        waveText.text = "Round - " + (currentWaveIndex + 1).ToString();

        // Get the current wave's enemies
        List<GameObject> currentWave = waves[currentWaveIndex];

        // Set remaining enemies to the number of enemies in the current wave
        remainingEnemies = currentWave.Count;

        // Delay before starting the next wave
        yield return new WaitForSeconds(waveDelay);

        // Spawn each enemy in the wave with a delay
        for (int i = 0; i < currentWave.Count; i++)
        {
            SpawnEnemy(currentWave[i]);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        // Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the enemy at the chosen spawn point
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void EnemyKilled()
    {
        // Reduce the remaining enemies count
        remainingEnemies--;

        // If all enemies are killed, spawn the next wave
        if (remainingEnemies <= 0)
        {
            currentWaveIndex++;
            StartCoroutine(SpawnWave(waveDelay));
        }
    }
}
