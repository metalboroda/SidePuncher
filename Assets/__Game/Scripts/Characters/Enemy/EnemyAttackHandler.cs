using System;
using System.Collections;
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

    public void AttackWithRate()
    {
      StartCoroutine(DoAttackWithRate());
    }

    private IEnumerator DoAttackWithRate()
    {
      while (true)
      {
        yield return new WaitForSeconds(Random.Range(minAttackRate, maxAttackRate));

        PerformAttack();
      }
    }

    private void PerformAttack()
    {
      AttackTriggered?.Invoke();
    }
  }
}