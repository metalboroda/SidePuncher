using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Components;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyMovementHandler : MonoBehaviour
  {
    [SerializeField] private float movementSpeed = 1.5f;

    [Header("Enemy detection params")]
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask allyLayer;

    private bool _canMove = true;
    private bool _enemyFound = false;

    private MovementComponent _movementComponent;
    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
      _movementComponent = new MovementComponent();
    }

    private void Update()
    {
      _movementComponent.MoveForward(_canMove, movementSpeed, transform);
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
        if(_enemyFound  == true) return;

        _enemyController.StateMachine.ChangeState(new EnemyFightState(_enemyController));
        _canMove = false;
        _enemyFound = true;
      }
      else if (Physics.Raycast(origin, direction, out hit, rayDistance, allyLayer))
      {
        _canMove = false;
      }
      else
      {
        _canMove = true;
      }
    }
  }
}