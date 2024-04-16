using Assets.__Game.Scripts.Characters.Enemy;
using EventBus;
using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace Assets.__Game.Scripts.Level
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private GameObject standardEnemy;
    [SerializeField] private GameObject heavyEnemy;

    [Space]
    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnRate = 4f;
    [SerializeField] private int heavySpawnPeriod = 10;
    [SerializeField] private int spawnLimit = 10;

    private Coroutine spawnCoroutine;
    private int enemiesSpawned = 0;

    private EventBinding<PlayerDead> _onPlayerDeathEvent;

    private void OnEnable()
    {
      _onPlayerDeathEvent = new EventBinding<PlayerDead>(PauseSpawning);
    }

    private void OnDisable()
    {
      _onPlayerDeathEvent.Remove(PauseSpawning);
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
        float randDelay = Random.Range(minSpawnRate, maxSpawnRate);

        yield return new WaitForSeconds(randDelay);

        SpawnEnemy();

        enemiesSpawned++;
      }
    }

    private void SpawnEnemy()
    {
      GameObject enemyPrefab = standardEnemy;

      if (enemiesSpawned > 0 && enemiesSpawned % heavySpawnPeriod == 0)
      {
        enemyPrefab = heavyEnemy;
      }

      var spawnedEnemy = LeanPool.Spawn(enemyPrefab).GetComponentInChildren<EnemySpawnInitializer>();

      spawnedEnemy.SpawnInit(transform.position, transform.rotation);

      enemiesSpawned++;
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