using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Components;
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
    protected CharacterAudioHandler CharacterAudioHandler;

    protected FiniteStateMachine FiniteStateMachine;
    protected MovementComponent MovementComponent;

    public EnemyBaseState(EnemyController enemyController) {
      EnemyController = enemyController;
      EnemyHandler = EnemyController.EnemyHandler;
      EnemyAttackHandler = EnemyController.EnemyAttackHandler;
      EnemyMovementHandler = EnemyController.EnemyMovementHandler;
      EnemyAnimationHandler = EnemyController.EnemyAnimationHandler;
      CharacterPuppetHandler = EnemyController.CharacterPuppetHandler;
      CharacterAudioHandler = EnemyController.CharacterAudioHandler;

      FiniteStateMachine = EnemyController.FiniteStateMachine;
      MovementComponent = EnemyMovementHandler.MovementComponent;
    }
  }
}