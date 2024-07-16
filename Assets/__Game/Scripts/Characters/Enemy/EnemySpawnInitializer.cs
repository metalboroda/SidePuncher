using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemySpawnInitializer : MonoBehaviour
  {
    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    public void SpawnInit(Vector3 position, Quaternion rotation)
    {
      _enemyController.FiniteStateMachine.ChangeStateWithDelay(new EnemyMovementState(_enemyController), 0.1f, this);
      _enemyController.EnemyHandler.SwitchModelVisibility(false);
      transform.position = position;
      transform.rotation = rotation;
      _enemyController.CharacterPuppetHandler.DisableRagdoll();
      _enemyController.EnemyHandler.CapsuleCollider.enabled = true;
      _enemyController.EnemyHandler.CurrentHealth = _enemyController.EnemyHandler.MaxHealth;
      _enemyController.CharacterUIHandler.ResetParams();
      _enemyController.EnemyHandler.SwitchModelVisibility(true, 0.1f);
    }
  }
}