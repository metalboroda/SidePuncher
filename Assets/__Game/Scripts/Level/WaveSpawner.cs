using System.Collections;
using UnityEngine;
using Lean.Pool;
using UnityEngine.AddressableAssets;
using Assets.__Game.Scripts.Characters.Enemy;
using EventBus;
using System.Collections.Generic;

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

          EventBus<WaveCompleted>.Raise(new WaveCompleted { WaveCount = _wavesPassed });
        }
        else
        {
          Wave currentWave = waves[_wavesPassed];

          yield return StartCoroutine(SpawnWave(currentWave));

          _wavesPassed++;

          EventBus<WaveCompleted>.Raise(new WaveCompleted { WaveCount = _wavesPassed });
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

          yield return StartCoroutine(SpawnEnemyAsync(waveEnemy.Enemy));
          yield return new WaitForSeconds(currentWave.SpawnRate);
        }
      }

      yield return StartCoroutine(CheckWaveCompletion());
    }

    private IEnumerator SpawnEnemyAsync(GameObject enemy)
    {
      var enemyHandle = Addressables.LoadAssetAsync<GameObject>($"Prefabs/Characters/{enemy.name}");

      yield return enemyHandle;

      GameObject enemyObject = LeanPool.Spawn(enemyHandle.Result);
      EnemySpawnInitializer enemyInit = enemyObject.GetComponentInChildren<EnemySpawnInitializer>();
      GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

      enemyInit.SpawnInit(spawnPoint.transform.position, spawnPoint.transform.rotation);
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
      GameObject deadEnemy = enemyDeathEvent.GameObject;

      if (_spawnedEnemies.Contains(deadEnemy))
        _spawnedEnemies.Remove(deadEnemy);
    }
  }
}