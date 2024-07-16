using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyController : CharacterControllerBase
  {
    public EnemyHandler EnemyHandler;
    public EnemyAttackHandler EnemyAttackHandler;
    public EnemyMovementHandler EnemyMovementHandler;
    public EnemyAnimationHandler EnemyAnimationHandler;
    public CharacterPuppetHandler CharacterPuppetHandler;

    protected override void Awake() {
      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new EnemyMovementState(this));
    }
  }
}