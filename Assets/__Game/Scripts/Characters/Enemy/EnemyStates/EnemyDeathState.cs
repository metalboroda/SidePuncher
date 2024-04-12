namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyDeathState : EnemyBaseState
  {
    public EnemyDeathState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void Enter()
    {
      EnemyHandler.Death();
    }
  }
}