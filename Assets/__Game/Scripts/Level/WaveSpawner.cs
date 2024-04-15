using Assets.__Game.Scripts.Characters.Enemy;
using EventBus;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.Level
{
  public class WaveSpawner : MonoBehaviour
  {
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int limitPerRow;

    private int _wavesPassed = 0;
    private readonly List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _previousRandomWave = -1;

    private EventBinding<EnemyDead> _enemyDeathEvent;

    private void OnEnable()
    {
      _enemyDeathEvent = new EventBinding<EnemyDead>(RemoveDeadEnemyFromList);
    }

    private void OnDisable()
    {
      _enemyDeathEvent.Remove(RemoveDeadEnemyFromList);
    }

    private void Start()
    {
      StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
      while (true)
      {
        if (_wavesPassed >= waves.Length)
        {
          yield return StartCoroutine(SpawnRandomWave());

          _wavesPassed++;

          EventBus<WaveCompleted>.Raise(new WaveCompleted { waveCount = _wavesPassed });
        }
        else
        {
          Wave currentWave = waves[_wavesPassed];

          yield return StartCoroutine(SpawnWave(currentWave));

          _wavesPassed++;

          EventBus<WaveCompleted>.Raise(new WaveCompleted { waveCount = _wavesPassed });
        }

        yield return new WaitForSeconds(timeBetweenWaves);
      }
    }

    private IEnumerator SpawnRandomWave()
    {
      int randomIndex;

      do
      {
        randomIndex = Random.Range(0, waves.Length);
      } while (randomIndex == _previousRandomWave);

      _previousRandomWave = randomIndex;

      Wave randomWave = waves[randomIndex];

      yield return StartCoroutine(SpawnWave(randomWave));
    }

    private IEnumerator SpawnWave(Wave currentWave)
    {
      foreach (WaveEnemy waveEnemy in currentWave.WaveEnemies)
      {
        for (int i = 0; i < waveEnemy.amount; i++)
        {
          if (_spawnedEnemies.Count >= limitPerRow)
            yield return StartCoroutine(WaitForEnemiesCountBelow(limitPerRow));

          SpawnEnemy(waveEnemy.Enemy);

          yield return new WaitForSeconds(currentWave.SpawnRate);
        }
      }

      yield return StartCoroutine(CheckWaveCompletion());
    }

    private void SpawnEnemy(GameObject enemy)
    {
      GameObject enemyObject = LeanPool.Spawn(enemy);
      EnemyHandler enemyHandler = enemyObject.GetComponentInChildren<EnemyHandler>();
      GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

      enemyHandler.SpawnInit(spawnPoint.transform.position, spawnPoint.transform.rotation);
      _spawnedEnemies.Add(enemyObject);
    }

    private IEnumerator WaitForEnemiesCountBelow(int targetCount)
    {
      while (_spawnedEnemies.Count >= targetCount)
        yield return null;
    }

    private IEnumerator CheckWaveCompletion()
    {
      while (_spawnedEnemies.Count > 0)
        yield return null;
    }

    private void RemoveDeadEnemyFromList(EnemyDead enemyDeathEvent)
    {
      GameObject deadEnemy = enemyDeathEvent.gameObject;

      if (_spawnedEnemies.Contains(deadEnemy))
        _spawnedEnemies.Remove(deadEnemy);
    }
  }
}