using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [field: SerializeField] public float MovementSpeed { get; private set; } = 1.5f;

    [Header("Enemy detection params")]
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    public LayerMask AllyLayer;

    public void RaycastAndState(LayerMask layerMask, Action trueAction = null, Action falseACtion = null)
    {
      Vector3 origin = rayPoint.position;
      Vector3 direction = transform.forward;
      RaycastHit hit;

      if (Physics.Raycast(origin, direction, out hit, rayDistance, layerMask))
      {
        trueAction?.Invoke();
      }
      else
      {
        falseACtion?.Invoke();
      }
    }
  }
}