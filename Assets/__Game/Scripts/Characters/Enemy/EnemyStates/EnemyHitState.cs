namespace Assets.__Game.Scripts.Characters.Enemy.EnemyStates
{
  public class EnemyHitState : EnemyBaseState
  {
    public EnemyHitState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter() {
      EnemyAnimationHandler.PlayRandomHitAnimation();
      CharacterAudioHandler.PlayRandomDamageSound();
      EnemyAnimationHandler.StopCoroutines();
      EnemyAnimationHandler.OnAnimtionEnds(EnemyAnimationHandler.AnimationEndTime,
        () => { FiniteStateMachine.ChangeState(new EnemyFightState(EnemyController)); });
    }
  }
}
