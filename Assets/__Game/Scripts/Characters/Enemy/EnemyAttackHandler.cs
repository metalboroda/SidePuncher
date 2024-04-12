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

    private Coroutine _attackRoutine;

    public void AttackWithRate()
    {
      _attackRoutine = StartCoroutine(DoAttackWithRate());
    }

    private IEnumerator DoAttackWithRate()
    {
      while (true)
      {
        yield return new WaitForSeconds(Random.Range(minAttackRate, maxAttackRate));

        AttackTriggered?.Invoke();
      }
    }

    public void StopCoroutines()
    {
      if (_attackRoutine != null)
        StopCoroutine(_attackRoutine);
    }

    /*private IEnumerator DoPerformAttack()
    {
      yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));

      AttackTriggered?.Invoke();
    }*/
  }
}