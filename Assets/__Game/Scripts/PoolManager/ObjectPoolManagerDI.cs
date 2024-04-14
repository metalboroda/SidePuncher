using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Threading.Tasks;
using System.Linq;

namespace Assets.__Game.Scripts.PoolManager
{
  public class ObjectPoolManagerDI
  {
    [Inject] private readonly DiContainer _container;

    private Dictionary<GameObject, List<GameObject>> _pools = new Dictionary<GameObject, List<GameObject>>();

    public void InitializePool(GameObject prefab, int quantity)
    {
      if (_pools.ContainsKey(prefab) == false)
      {
        _pools[prefab] = new List<GameObject>();

        for (int i = 0; i < quantity; i++)
        {
          GameObject obj = _container.InstantiatePrefab(prefab);

          obj.SetActive(false);
          _pools[prefab].Add(obj);
        }
      }
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
      if (_pools.ContainsKey(prefab) == false)
      {
        InitializePool(prefab, 10);
      }

      List<GameObject> pool = _pools[prefab];
      GameObject obj = pool.Find(go => !go.activeSelf);

      if (obj == null)
      {
        obj = _container.InstantiatePrefab(prefab, position, rotation, parent);
        pool.Add(obj);
      }
      else
      {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
      }

      obj.SetActive(true);

      IPoolable[] poolables = obj.GetComponents<IPoolable>();
      IPoolable[] poolablesInChildren = obj.GetComponentsInChildren<IPoolable>();

      foreach (IPoolable poolable in poolablesInChildren.Concat(poolables))
      {
        poolable.OnSpawned();
      }

      return obj;
    }

    public async void Despawn(GameObject obj, float delay = 0)
    {
      await Task.Delay((int)(delay * 1000));

      obj.SetActive(false);

      IPoolable[] poolables = obj.GetComponents<IPoolable>();
      IPoolable[] poolablesInChildren = obj.GetComponentsInChildren<IPoolable>();

      foreach (IPoolable poolable in poolablesInChildren.Concat(poolables))
      {
        poolable.OnDespawned();
      }
    }
  }
}