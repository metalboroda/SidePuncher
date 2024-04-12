namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  internal class EnemyIdleState : EnemyBaseState
  {
    public EnemyIdleState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
      EnemyAnimationHandler.PlayRandomIdleAnimation();
    }

    public override void Update()
    {
      EnemyMovementHandler.DetectEnemyAndAlly();
    }
  }
}