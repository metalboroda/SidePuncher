using Assets.__Game.Scripts.Characters.Enemy;
using EventBus;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.__Game.Scripts.Level
{
  public class WaveSpawner : MonoBehaviour
  {
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int limitPerRow;

    private int _waveCount;
    private readonly List<GameObject> _spawnedEnemies = new List<GameObject>();
    private EventBinding<EnemyDead> _enemyDeathEvent;
    private List<Wave> _randomWaves = new List<Wave>();

    private void OnEnable()
    {
      _randomWaves.AddRange(waves);

      _enemyDeathEvent = new EventBinding<EnemyDead>(RemoveDeadEnemyFromList);
    }

    private void OnDisable()
    {
      _enemyDeathEvent.Remove(RemoveDeadEnemyFromList);
    }

    private void Start()
    {
      StartCoroutine(DoSpawnWaves());
    }

    private IEnumerator DoSpawnWaves()
    {
      while (_randomWaves.Count > 0)
      {
        Wave currentWave = _randomWaves[Random.Range(0, _randomWaves.Count)];

        yield return StartCoroutine(SpawnWave(currentWave));

        _waveCount++;

        EventBus<WaveCompleted>.Raise(new WaveCompleted
        {
          waveCount = _waveCount
        });

        yield return new WaitForSeconds(timeBetweenWaves);

        _randomWaves.Remove(currentWave);
      }
    }

    private IEnumerator SpawnWave(Wave currentWave)
    {
      foreach (WaveEnemy waveEnemy in currentWave.WaveEnemies)
      {
        for (int i = 0; i < waveEnemy.amount; i++)
        {
          if (_spawnedEnemies.Count >= limitPerRow)
          {
            yield return StartCoroutine(WaitForEnemiesCountBelow(limitPerRow));
          }

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