using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.__Game.Scripts.PoolManager
{
  public class ObjectPoolManager
  {
    [Inject] private readonly DiContainer _container;

    private Dictionary<GameObject, List<GameObject>> _pools = new Dictionary<GameObject, List<GameObject>>();

    public void InitializePool(GameObject prefab, int quantity)
    {
      if (!_pools.ContainsKey(prefab))
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
      if (!_pools.ContainsKey(prefab))
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

      IPoolable poolable = obj.GetComponent<IPoolable>();

      if (poolable != null)
        poolable.OnSpawned();

      return obj;
    }

    public void Despawn(GameObject obj)
    {
      obj.SetActive(false);

      IPoolable poolable = obj.GetComponent<IPoolable>();

      if (poolable != null)
        poolable.OnDespawned();
    }
  }
}