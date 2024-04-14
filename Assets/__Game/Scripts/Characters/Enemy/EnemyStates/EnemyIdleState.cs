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
      CheckIfCanMoveForward();
    }

    private void CheckIfCanMoveForward()
    {
      EnemyMovementHandler.RaycastAndState(EnemyMovementHandler.AllyLayer, null, EnemyController.ToMovementState);
    }
  }
}