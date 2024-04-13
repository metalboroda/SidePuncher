using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyDeathState : EnemyBaseState
  {
    public EnemyDeathState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
      EnemyAnimationHandler.StopCoroutines();
      EnemyAnimationHandler.PlayRandomDeathAnimation();
      EnemyAnimationHandler.DeathRandomRotation();
      CharacterPuppetHandler.EnableRagdoll();
      EnemyHandler.Death();
    }
  }
}