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

    private void FixedUpdate()
    {
      DetectEnemyAndAlly();
    }

    private void DetectEnemyAndAlly()
    {
      Vector3 origin = rayPoint.position;
      Vector3 direction = transform.forward;
      RaycastHit hit;

      if (Physics.Raycast(origin, direction, out hit, rayDistance, _enemyController.EnemyHandler.EnemyLayer))
      {
        if (_enemyController.StateMachine.CurrentState is EnemyFightState) return;

        _enemyController.StateMachine.ChangeState(new EnemyFightState(_enemyController));
      }
      else if (Physics.Raycast(origin, direction, out hit, rayDistance, allyLayer))
      {
        if (_enemyController.StateMachine.CurrentState is EnemyIdleState) return;

        _enemyController.StateMachine.ChangeState(new EnemyIdleState(_enemyController));
      }
      else
      {
        if (_enemyController.StateMachine.CurrentState is EnemyMovementState) return;

        _enemyController.StateMachine.ChangeState(new EnemyMovementState(_enemyController));
      }
    }
  }
}