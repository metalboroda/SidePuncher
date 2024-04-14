using Assets.__Game.Scripts.Characters.Enemy;
using EventBus;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.__Game.Scripts.Level
{
  public class SpawnersManager : MonoBehaviour
  {
    [SerializeField] private GameObject regularEnemy;
    [SerializeField] private GameObject toughEnemy;

    [Header("Param's")]
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;
    [SerializeField] private float toughEnemyPeriod;
    [SerializeField] private int rowSpawnLimit;
    [SerializeField] private int overallSpawnLimit;

    [Space]
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    private readonly List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _spawnCount;
    private Coroutine _spawnRoutine;

    private EventBinding<EnemyDeathEvent> _enemyDeathEvent;

    private void OnEnable()
    {
      _enemyDeathEvent = new EventBinding<EnemyDeathEvent>(RemoveSpawnedEnemy);
    }

    private void OnDisable()
    {
      _enemyDeathEvent.Remove(RemoveSpawnedEnemy);
    }

    private void Start()
    {
      SpawnEnemies();
    }

    private void SpawnEnemies()
    {
      _spawnRoutine = StartCoroutine(DoSpawnEnemies());
    }

    private IEnumerator DoSpawnEnemies()
    {
      while (_spawnCount < overallSpawnLimit)
      {
        float spawnDelay = Random.Range(minSpawnRate, maxSpawnRate);

        yield return new WaitForSeconds(spawnDelay);

        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject currentEnemyToSpawn = _spawnCount % toughEnemyPeriod == 0 ? toughEnemy : regularEnemy;
        GameObject newEnemy = InstantiateEnemy(currentEnemyToSpawn, spawnPoint);

        _spawnedEnemies.Add(newEnemy);
        _spawnCount++;

        if (_spawnCount % rowSpawnLimit == 0)
          StopSpawn();
      }
    }

    private void StopSpawn()
    {
      if (_spawnRoutine != null)
        StopCoroutine(_spawnRoutine);
    }

    private GameObject InstantiateEnemy(GameObject enemyToSpawn, GameObject spawnPoint)
    {
      EnemyHandler newEnemy = LeanPool.Spawn(enemyToSpawn).GetComponentInChildren<EnemyHandler>();

      newEnemy.SpawnInit(spawnPoint.transform.position, spawnPoint.transform.rotation);

      return newEnemy.transform.root.gameObject;
    }

    private void RemoveSpawnedEnemy(EnemyDeathEvent enemyDeathEvent)
    {
      GameObject objectToRemove = enemyDeathEvent.gameObject;

      if (_spawnedEnemies.Contains(objectToRemove))
      {
        _spawnedEnemies.Remove(objectToRemove);

        if (_spawnedEnemies.Count % rowSpawnLimit == 0)
          SpawnEnemies();
      }
    }
  }
}