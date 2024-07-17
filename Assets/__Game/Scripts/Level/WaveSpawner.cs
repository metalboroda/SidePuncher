using Assets.__Game.Scripts.EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static Assets.__Game.Scripts.EventBus.EventStructs;

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
    private bool _spawningActive = true;
    private Coroutine _spawningCoroutine;

    private EventBinding<PlayerDead> _playerDeadEvent;
    private EventBinding<EnemyDead> _enemyDeathEvent;

    private void OnEnable() {
      _playerDeadEvent = new EventBinding<PlayerDead>(OnPlayerDead);
      _enemyDeathEvent = new EventBinding<EnemyDead>(RemoveDeadEnemyFromList);
    }

    private void OnDisable() {
      _playerDeadEvent.Remove(OnPlayerDead);
      _enemyDeathEvent.Remove(RemoveDeadEnemyFromList);
    }

    private void Start() {
      _spawningCoroutine = StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves() {
      while (true) {
        if (!_spawningActive) {
          yield return new WaitUntil(() => _spawningActive);
        }

        if (_wavesPassed >= waves.Length) {
          yield return StartCoroutine(SpawnRandomWave());
          _wavesPassed++;

          EventBus<WaveCompleted>.Raise(new WaveCompleted { WaveCount = _wavesPassed });
        }
        else {
          Wave currentWave = waves[_wavesPassed];
          yield return StartCoroutine(SpawnWave(currentWave));
          _wavesPassed++;

          EventBus<WaveCompleted>.Raise(new WaveCompleted { WaveCount = _wavesPassed });
        }

        yield return new WaitForSeconds(timeBetweenWaves);
      }
    }

    private IEnumerator SpawnRandomWave() {
      int randomIndex;
      do {
        randomIndex = Random.Range(0, waves.Length);
      } while (randomIndex == _previousRandomWave);

      _previousRandomWave = randomIndex;
      Wave randomWave = waves[randomIndex];

      yield return StartCoroutine(SpawnWave(randomWave));
    }

    private IEnumerator SpawnWave(Wave currentWave) {
      foreach (WaveEnemy waveEnemy in currentWave.WaveEnemies) {
        for (int i = 0; i < waveEnemy.amount; i++) {
          if (!_spawningActive) {
            yield return new WaitUntil(() => _spawningActive);
          }

          if (_spawnedEnemies.Count >= limitPerRow) {
            yield return StartCoroutine(WaitForEnemiesCountBelow(limitPerRow));
          }

          yield return StartCoroutine(SpawnEnemyAsync(waveEnemy.Enemy));
          yield return new WaitForSeconds(currentWave.SpawnRate);
        }
      }

      yield return StartCoroutine(CheckWaveCompletion());
    }

    private IEnumerator SpawnEnemyAsync(GameObject enemy) {
      var enemyHandle = Addressables.LoadAssetAsync<GameObject>($"Prefabs/Characters/{enemy.name}");

      yield return enemyHandle;

      GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
      GameObject enemyObject = Instantiate(enemyHandle.Result, spawnPoint.transform.position, spawnPoint.transform.rotation);

      _spawnedEnemies.Add(enemyObject);
    }

    private IEnumerator WaitForEnemiesCountBelow(int targetCount) {
      while (_spawnedEnemies.Count >= targetCount) {
        yield return null;
      }
    }

    private IEnumerator CheckWaveCompletion() {
      while (_spawnedEnemies.Count > 0) {
        yield return null;
      }
    }

    private void RemoveDeadEnemyFromList(EnemyDead enemyDeathEvent) {
      GameObject deadEnemy = enemyDeathEvent.GameObject;
      if (_spawnedEnemies.Contains(deadEnemy)) {
        _spawnedEnemies.Remove(deadEnemy);
      }
    }

    private void OnPlayerDead() {
      EventBus<WaveCompleted>.Raise(new WaveCompleted { WaveCount = _wavesPassed });

      StopSpawning();
    }

    public void StopSpawning() {
      _spawningActive = false;
    }

    public void ResumeSpawning() {
      _spawningActive = true;
    }
  }
}