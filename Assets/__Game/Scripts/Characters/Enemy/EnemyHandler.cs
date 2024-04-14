﻿using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
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
    [SerializeField] private EnemyController enemyController;

    private Renderer[] _renderers;

    private EventBinding<PlayerDeathEvent> _playerDeathEvent;

    protected override void Awake()
    {
      base.Awake();

      _renderers = transform.root.GetComponentsInChildren<Renderer>();
    }

    private void OnEnable()
    {
      _playerDeathEvent = new EventBinding<PlayerDeathEvent>(Victory);
    }

    private void OnDisable()
    {
      _playerDeathEvent.Remove(Victory);
    }

    protected override void Start()
    {
      base.Start();
    }

    public void SpawnInit(Vector3 position, Quaternion rotation)
    {
      SwitchModelVisibility(false);
      enemyController.StateMachine.ChangeStateWithDelay(new EnemyMovementState(enemyController), 0.0001f, this);
      transform.position = position;
      transform.rotation = rotation;
      enemyController.CharacterPuppetHandler.DisableRagdoll();
      CapsuleCollider.enabled = true;
      CurrentHealth = MaxHealth;
      SwitchModelVisibility(true, 0.1f);
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      enemyController.StateMachine.ChangeState(new EnemyHitState(enemyController));

      if (CurrentHealth <= 0)
      {
        CapsuleCollider.enabled = false;
        CurrentHealth = 0;
        enemyController.StateMachine.ChangeState(new EnemyDeathState(enemyController));
        EnemyDead?.Invoke();

        EventBus<EnemyDeathEvent>.Raise(new EnemyDeathEvent()
        {
          gameObject = transform.root.gameObject
        });
      }
    }

    public override void Death(float delay)
    {
      LeanPool.Despawn(transform.root.gameObject, delay);
    }

    public override void Victory()
    {
      if (enemyController.StateMachine.CurrentState is not EnemyDeathState)
        enemyController.StateMachine.ChangeState(new EnemyVictoryState(enemyController));
    }

    private void SwitchModelVisibility(bool enable, float delay = 0)
    {
      StartCoroutine(DoSwitchModelVisibility(enable, delay));
    }

    private IEnumerator DoSwitchModelVisibility(bool enable, float delay)
    {
      yield return new WaitForSeconds(delay);

      enemyController.CharacterPuppetHandler.RagdollObject.SetActive(enable);

      foreach (var renderer in _renderers)
      {
        renderer.enabled = enable;
      }
    }
  }
}