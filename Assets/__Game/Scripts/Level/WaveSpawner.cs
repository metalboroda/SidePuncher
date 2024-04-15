using Assets.__Game.Scripts.Characters.Enemy;
using EventBus;
using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.__Game.Scripts.Level
{
  public class WaveSpawner : MonoBehaviour
  {
    public event Action WaveCompleted;

    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int limitPerRow;

    private int _waveCount;
    private readonly List<GameObject> _spawnedEnemies = new List<GameObject>();
    private EventBinding<EnemyDeathEvent> _enemyDeathEvent;

    private void OnEnable()
    {
      _enemyDeathEvent = new EventBinding<EnemyDeathEvent>(RemoveDeadEnemyFromList);

      StartCoroutine(SpawnWaves());
    }

    private void OnDisable()
    {
      _enemyDeathEvent.Remove(RemoveDeadEnemyFromList);
    }

    private IEnumerator SpawnWaves()
    {
      while (_waveCount < waves.Length)
      {
        yield return StartCoroutine(SpawnWave());

        _waveCount++;

        WaveCompleted?.Invoke();

        yield return new WaitForSeconds(timeBetweenWaves);
      }
    }

    private IEnumerator SpawnWave()
    {
      Wave currentWave = waves[_waveCount];

      int[] enemiesSpawned = new int[2];

      while (enemiesSpawned[0] < currentWave.regularEnemyPerWave || enemiesSpawned[1] < currentWave.toughEnemyPerWave)
      {
        if (_spawnedEnemies.Count >= limitPerRow)
          yield return StartCoroutine(WaitForEnemiesCountBelow(limitPerRow));

        SpawnEnemy(currentWave, ref enemiesSpawned);

        yield return new WaitForSeconds(currentWave.spawnRate);
      }

      yield return StartCoroutine(CheckWaveCompletion());
    }

    private void SpawnEnemy(Wave wave, ref int[] enemiesSpawned)
    {
      int enemyType = (enemiesSpawned[0] < wave.regularEnemyPerWave) ? 0 : 1;
      GameObject enemyObject = LeanPool.Spawn((enemyType == 0) ? wave.regularEnemy : wave.toughEnemy);
      EnemyHandler enemyHandler = enemyObject.GetComponentInChildren<EnemyHandler>();
      GameObject spawnPoint = wave.spawnPoints[Random.Range(0, wave.spawnPoints.Length)];

      enemyHandler.SpawnInit(spawnPoint.transform.position, spawnPoint.transform.rotation);
      _spawnedEnemies.Add(enemyObject);
      enemiesSpawned[enemyType]++;
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

    private void RemoveDeadEnemyFromList(EnemyDeathEvent enemyDeathEvent)
    {
      GameObject deadEnemy = enemyDeathEvent.gameObject;

      if (_spawnedEnemies.Contains(deadEnemy))
        _spawnedEnemies.Remove(deadEnemy);
    }
  }
}