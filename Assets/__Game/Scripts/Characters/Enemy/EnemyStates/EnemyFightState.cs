namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyFightState : EnemyBaseState
  {
    public EnemyFightState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void Enter()
    {
      EnemyAnimationHandler.PlayRandomIdleAnimation();
      EnemyAttackHandler.AttackWithRate();
    }

    public override void Exit()
    {
      EnemyAttackHandler.StopCoroutines();
    }
  }
}