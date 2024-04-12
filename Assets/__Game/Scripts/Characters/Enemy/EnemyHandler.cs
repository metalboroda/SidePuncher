using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using System;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable
  {
    public event Action EnemyDead;

    private EnemyController _enemyController;

    protected override void Awake()
    {
      base.Awake();

      _enemyController = GetComponent<EnemyController>();
    }

    protected override void Start()
    {
      base.Start();
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;
        CapsuleCollider.enabled = false;
        _enemyController.StateMachine.ChangeState(new EnemyDeathState(_enemyController));
        EnemyDead?.Invoke();
      }
    }
  }
}