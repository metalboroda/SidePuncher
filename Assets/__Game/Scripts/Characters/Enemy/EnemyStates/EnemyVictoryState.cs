namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  internal class EnemyVictoryState : EnemyBaseState
  {
    public EnemyVictoryState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
      EnemyAnimationHandler.StopCoroutines();
      EnemyAnimationHandler.PlayRandomVictoryAnimation();
    }
  }
}