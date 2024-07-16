using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyDeathState : EnemyBaseState
  {
    private readonly int _maxDyingChance = 4;

    public EnemyDeathState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
      int randDyingChance = Random.Range(0, _maxDyingChance);

      EnemyAnimationHandler.StopCoroutines();
      EnemyAnimationHandler.PlayRandomDeathAnimation();
      EnemyAnimationHandler.DeathRandomRotation();

      if (randDyingChance == 0)
        EnemyAnimationHandler.OnAnimtionEnds(0.8f, EnemyAnimationHandler.PlayRandomDyingAnimation);

      CharacterPuppetHandler.EnableRagdoll();
      EnemyHandler.Death(EnemyHandler.DeathTime);
    }
  }
}