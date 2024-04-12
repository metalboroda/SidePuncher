using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable
  {
    [Space]
    [SerializeField] private EnemyController enemyController;

    protected override void Start()
    {
      base.Start();
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;

        enemyController.StateMachine.ChangeState(new EnemyDeathState(enemyController));
      }
    }

    public void Death()
    {
      Destroy(gameObject);
    }
  }
}