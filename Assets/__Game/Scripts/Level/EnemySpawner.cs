using Assets.__Game.Scripts.PoolManager;
using UnityEngine;
using Zenject;

namespace Assets.__Game.Scripts.Level
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private GameObject enemyPrefab;

    [Inject] private readonly ObjectPoolManager _objectPoolManager;

    private void Awake()
    {
      _objectPoolManager.InitializePool(enemyPrefab, 5);
    }

    private void Start()
    {
      _objectPoolManager.Spawn(enemyPrefab, transform.position, transform.rotation);
    }
  }
}