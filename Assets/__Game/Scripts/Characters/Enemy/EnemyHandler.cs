using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using EventBus;
using Lean.Pool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable
  {
    public event Action EnemyDead;

    [Space]
    [SerializeField] private int healthRecoveryValue = 10;

    private Renderer[] _renderers;

    private EnemyController _enemyController;

    private EventBinding<PlayerDead> _playerDeathEvent;

    protected override void Awake()
    {
      base.Awake();

      _enemyController = GetComponent<EnemyController>();
      _renderers = transform.root.GetComponentsInChildren<Renderer>();
    }

    private void OnEnable()
    {
      _playerDeathEvent = new EventBinding<PlayerDead>(Victory);
    }

    private void OnDisable()
    {
      _playerDeathEvent.Remove(Victory);
    }

    protected override void Start()
    {
      base.Start();
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      _enemyController.StateMachine.ChangeState(new EnemyHitState(_enemyController));

      if (CurrentHealth <= 0)
      {
        CapsuleCollider.enabled = false;
        CurrentHealth = 0;
        _enemyController.StateMachine.ChangeState(new EnemyDeathState(_enemyController));
        EnemyDead?.Invoke();

        EventBus<EnemyDead>.Raise(new EnemyDead()
        {
          gameObject = transform.root.gameObject,
          healthRecoveryValue = this.healthRecoveryValue,
        });
      }
    }

    public override void Death(float delay)
    {
      LeanPool.Despawn(transform.root.gameObject, delay);
    }

    public override void Victory()
    {
      if (_enemyController.StateMachine.CurrentState is not EnemyDeathState)
        _enemyController.StateMachine.ChangeState(new EnemyVictoryState(_enemyController));
    }

    public void SwitchModelVisibility(bool enable, float delay = 0)
    {
      StartCoroutine(DoSwitchModelVisibility(enable, delay));
    }

    private IEnumerator DoSwitchModelVisibility(bool enable, float delay)
    {
      yield return new WaitForSeconds(delay);

      _enemyController.CharacterPuppetHandler.RagdollObject.SetActive(enable);

      foreach (var renderer in _renderers)
      {
        renderer.enabled = enable;
      }
    }
  }
}