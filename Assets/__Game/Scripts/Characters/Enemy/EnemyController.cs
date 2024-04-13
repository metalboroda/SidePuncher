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

    protected override void Awake()
    {
      base.Awake();
    }

    private void Start()
    {
      StateMachine.Init(new EnemyMovementState(this));
    }

    public void ToPreviousState()
    {
      StateMachine.ChangeState(StateMachine.PreviousState);
    }

    public void ToFightState()
    {
      StateMachine.ChangeState(new EnemyFightState(this));
    }

    public void ToDeathState()
    {
      StateMachine.ChangeState(new EnemyDeathState(this));
    }
  }
}