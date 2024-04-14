using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using Assets.__Game.Scripts.PoolManager;
using Assets.__Game.Scripts.Services;
using System;
using UnityEngine;
using Zenject;
using IPoolable = Assets.__Game.Scripts.PoolManager.IPoolable;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable, IPoolable
  {
    public event Action EnemyDead;

    [Space]
    [SerializeField] private EnemyController enemyController;

    [Inject] private readonly EventBus _eventBus;
    [Inject] private readonly ObjectPoolManagerDI _objectPoolManagerDI;

    protected override void Awake()
    {
      base.Awake();
    }

    private void OnEnable()
    {
      _eventBus.PlayerDead += Victory;
    }

    private void OnDisable()
    {
      _eventBus.PlayerDead -= Victory;
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
      _objectPoolManagerDI.Despawn(transform.root.gameObject, 5);
    }

    public override void Victory()
    {
      enemyController.StateMachine.ChangeState(new EnemyVictoryState(enemyController));
    }

    public void OnSpawned()
    {
      Debug.Log("Spawned");
    }

    public void OnDespawned()
    {
      Debug.Log("Despawned");
    }
  }
}