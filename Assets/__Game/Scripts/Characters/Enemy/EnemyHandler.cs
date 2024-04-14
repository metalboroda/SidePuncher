using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using EventBus;
using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable
  {
    public event Action EnemyDead;

    [Space]
    [SerializeField] private EnemyController enemyController;

    EventBinding<PlayerDeathEvent> _onPlayerDeathEvent;

    protected override void Awake()
    {
      base.Awake();
    }

    private void OnEnable()
    {
      _onPlayerDeathEvent = new EventBinding<PlayerDeathEvent>(Victory);
    }

    private void OnDisable()
    {
      _onPlayerDeathEvent.Remove(Victory);
    }

    protected override void Start()
    {
      base.Start();
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      enemyController.StateMachine.ChangeState(new EnemyHitState(enemyController));

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;
        CapsuleCollider.enabled = false;
        enemyController.StateMachine.ChangeState(new EnemyDeathState(enemyController));
        EnemyDead?.Invoke();
      }
    }

    public override void Death()
    {
      Destroy(transform.root.gameObject, 6);
    }

    public override void Victory()
    {
      enemyController.StateMachine.ChangeState(new EnemyVictoryState(enemyController));
    }
  }
}