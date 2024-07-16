using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;

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

    protected override void Awake() {
      base.Awake();

      _enemyController = GetComponent<EnemyController>();
      _renderers = transform.root.GetComponentsInChildren<Renderer>();
    }

    private void OnEnable() {
      _playerDeathEvent = new EventBinding<PlayerDead>(Victory);
    }

    private void OnDisable() {
      _playerDeathEvent.Remove(Victory);
    }

    protected override void Start() {
      base.Start();
    }

    public void Damage(int damage) {
      CurrentHealth -= damage;

      _enemyController.FiniteStateMachine.ChangeState(new EnemyHitState(_enemyController));

      EventBus<EnemyDamaged>.Raise(new EnemyDamaged());

      if (CurrentHealth <= 0) {
        CapsuleCollider.enabled = false;

        CurrentHealth = 0;

        _enemyController.FiniteStateMachine.ChangeState(new EnemyDeathState(_enemyController));

        EnemyDead?.Invoke();

        EventBus<EnemyDead>.Raise(new EnemyDead() {
          GameObject = transform.root.gameObject,
          HealthRecoveryValue = healthRecoveryValue,
        });
      }
    }

    public override void Death(float delay) {
      #region Pool
      //LeanPool.Despawn(transform.root.gameObject, delay);
      #endregion

      Destroy(transform.root.gameObject, delay);
    }

    public override void Victory() {
      if (_enemyController.FiniteStateMachine.CurrentState is not EnemyDeathState)
        _enemyController.FiniteStateMachine.ChangeState(new EnemyVictoryState(_enemyController));
    }

    public void SwitchModelVisibility(bool enable, float delay = 0) {
      StartCoroutine(DoSwitchModelVisibility(enable, delay));
    }

    private IEnumerator DoSwitchModelVisibility(bool enable, float delay) {
      yield return new WaitForSeconds(delay);

      _enemyController.CharacterPuppetHandler.RagdollObject.SetActive(enable);

      foreach (var renderer in _renderers) {
        renderer.enabled = enable;
      }
    }
  }
}