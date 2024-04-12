using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyBaseState : State
  {
    protected EnemyController EnemyController;
    protected EnemyHandler EnemyHandler;

    public EnemyBaseState(EnemyController enemyController)
    {
      EnemyController = enemyController;
      EnemyHandler = EnemyController.EnemyHandler;
    }
  }
}