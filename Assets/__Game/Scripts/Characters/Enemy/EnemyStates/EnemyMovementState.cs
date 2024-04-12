using Assets.__Game.Scripts.Components;

namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyMovementState : EnemyBaseState
  {
    private MovementComponent _movementComponent;

    public EnemyMovementState(EnemyController enemyController) : base(enemyController)
    {
      _movementComponent = new MovementComponent();
    }

    public override void Enter()
    {
      EnemyAnimationHandler.PlayRandomWalkAnimation();
    }

    public override void Update()
    {
      _movementComponent.MoveForward(EnemyMovementHandler.MovementSpeed, EnemyMovementHandler.transform);
    }
  }
}