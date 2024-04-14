using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [field: SerializeField] public float MovementSpeed { get; private set; } = 1.5f;

    [Header("Enemy detection params")]
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask allyLayer;

    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    public void DetectEnemyAndAlly()
    {
      RaycastHit hit;

      if (RaycastForLayer(_enemyController.EnemyHandler.EnemyLayer, out hit))
      {
        ChangeStateBasedOnCurrentState(new EnemyFightState(_enemyController));
      }
      else if (RaycastForLayer(allyLayer, out hit))
      {
        ChangeStateBasedOnCurrentState(new EnemyIdleState(_enemyController));
      }
      else
      {
        ChangeStateBasedOnCurrentState(new EnemyMovementState(_enemyController));
      }
    }

    private bool RaycastForLayer(LayerMask layerMask, out RaycastHit hit)
    {
      Vector3 origin = rayPoint.position;
      Vector3 direction = transform.forward;

      if (Physics.Raycast(origin, direction, out hit, rayDistance, layerMask))
      {
        return true;
      }

      return false;
    }

    private void ChangeStateBasedOnCurrentState(EnemyBaseState newState)
    {
      if (!(_enemyController.StateMachine.CurrentState.GetType() == newState.GetType()))
      {
        _enemyController.StateMachine.ChangeState(newState);
      }
    }
  }
}