using Assets.__Game.Scripts.EventBus;
using System;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;
using Random = UnityEngine.Random;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyAttackHandler : CharacterAttackHandlerBase
  {
    public event Action AttackTriggered;

    [SerializeField] private float minAttackRate = 1;
    [SerializeField] private float maxAttackRate = 1.5f;

    private float _attackTimer;

    private EventBinding<EnemyDead> _enemyDeadEvent;

    private void OnEnable() {
      _enemyDeadEvent = new EventBinding<EnemyDead>(OnEnemyDead);
    }

    private void OnDisable() {
      _enemyDeadEvent.Remove(OnEnemyDead);
    }

    public void AttackWithRate() {
      _attackTimer -= Time.deltaTime;

      if (_attackTimer <= 0f) {
        TriggerAttack();
        ResetAttackTimer();
      }
    }

    private void TriggerAttack() {
      AttackTriggered?.Invoke();
    }

    private void ResetAttackTimer() {
      _attackTimer = Random.Range(minAttackRate, maxAttackRate);
    }

    private void OnEnemyDead(EnemyDead enemyDead) {
      if (enemyDead.ID == transform.GetInstanceID()) {
        DisableAttackTriggers();
      }
    }
  }
}