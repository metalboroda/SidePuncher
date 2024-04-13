using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyBaseState : State
  {
    protected EnemyController EnemyController;
    protected EnemyHandler EnemyHandler;
    protected EnemyAttackHandler EnemyAttackHandler;
    protected EnemyMovementHandler EnemyMovementHandler;
    protected EnemyAnimationHandler EnemyAnimationHandler;
    protected CharacterPuppetHandler CharacterPuppetHandler;

    public EnemyBaseState(EnemyController enemyController)
    {
      EnemyController = enemyController;
      EnemyHandler = EnemyController.EnemyHandler;
      EnemyAttackHandler = EnemyController.EnemyAttackHandler;
      EnemyMovementHandler = EnemyController.EnemyMovementHandler;
      EnemyAnimationHandler = EnemyController.EnemyAnimationHandler;
      CharacterPuppetHandler = EnemyController.CharacterPuppetHandler;
    }
  }
}