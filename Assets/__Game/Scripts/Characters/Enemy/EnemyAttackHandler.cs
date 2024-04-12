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
    [SerializeField] private float minAttackDelay = 0.2f;
    [SerializeField] protected float maxAttackDelay = 0.6f;

    [Space]
    [SerializeField] private float minAttackRate = 1;
    [SerializeField] private float maxAttackRate = 1.5f;

    public void AttackWithRate()
    {
      StartCoroutine(DoAttackWithRate());
    }

    private IEnumerator DoAttackWithRate()
    {
      yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));

      while (true)
      {
        AttackTriggered?.Invoke();

        yield return new WaitForSeconds(Random.Range(minAttackRate, maxAttackRate));
      }
    }

    /*private IEnumerator DoPerformAttack()
    {
      yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));

      AttackTriggered?.Invoke();
    }*/
  }
}