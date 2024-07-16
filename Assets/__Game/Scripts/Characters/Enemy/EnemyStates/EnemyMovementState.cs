using Assets.__Game.Scripts.Components;

namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyMovementState : EnemyBaseState
  {
    public EnemyMovementState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter() {
      EnemyAnimationHandler.PlayRandomWalkAnimation();
    }

    public override void Update() {
      MovementComponent.MoveForward(EnemyMovementHandler.MovementSpeed, EnemyMovementHandler.transform);
      EnemyMovementHandler.RaycastAndState(EnemyHandler.EnemyLayer,
        () => { FiniteStateMachine.ChangeState(new EnemyFightState(EnemyController)); });
      EnemyMovementHandler.RaycastAndState(EnemyHandler.AllyLayer,
        () => { FiniteStateMachine.ChangeState(new EnemyIdleState(EnemyController)); });
    }
  }
}