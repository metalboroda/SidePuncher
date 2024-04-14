using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyAttackHandler : CharacterAttackHandlerBase
  {
    public event Action AttackTriggered;

    [Space]
    [SerializeField] private float minAttackRate = 1;
    [SerializeField] private float maxAttackRate = 1.5f;

    private float _attackTimer;

    public void AttackWithRate()
    {
      _attackTimer -= Time.deltaTime;

      if (_attackTimer <= 0f)
      {
        TriggerAttack();
        ResetAttackTimer();
      }
    }

    private void TriggerAttack()
    {
      AttackTriggered?.Invoke();
    }

    private void ResetAttackTimer()
    {
      _attackTimer = Random.Range(minAttackRate, maxAttackRate);
    }
  }
}
