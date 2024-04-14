using UnityEngine;
using Zenject;
using System.Collections;
using Assets.__Game.Scripts.PoolManager;

namespace Assets.__Game.Scripts.Level
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private GameObject enemyPrefab;

    [Space]
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private int spawnLimit = 10;

    [Space]
    [SerializeField] private Quaternion spawnRotation;

    [Inject] private readonly ObjectPoolManagerDI _objectPoolManagerDI;

    private Coroutine spawnCoroutine;
    private int enemiesSpawned = 0;

    private void Awake()
    {
      _objectPoolManagerDI.InitializePool(enemyPrefab, spawnLimit);
    }

    private void Start()
    {
      StartSpawning();
    }

    private void StartSpawning()
    {
      if (spawnCoroutine != null)
        StopCoroutine(spawnCoroutine);

      spawnCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
      while (enemiesSpawned < spawnLimit)
      {
        yield return new WaitForSeconds(spawnRate);

        SpawnEnemy();

        enemiesSpawned++;
      }
    }

    private void SpawnEnemy()
    {
      _objectPoolManagerDI.Spawn(enemyPrefab, transform.position, spawnRotation);
    }

    public void PauseSpawning()
    {
      if (spawnCoroutine != null)
        StopCoroutine(spawnCoroutine);
    }

    public void ResumeSpawning()
    {
      StartSpawning();
    }
  }
}