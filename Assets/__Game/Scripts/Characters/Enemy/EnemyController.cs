using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyController : CharacterControllerBase
  {
    public EnemyHandler EnemyHandler;
    public EnemyAttackHandler EnemyAttackHandler;

    protected override void Awake()
    {
      base.Awake();
    }

    private void Start()
    {
      StateMachine.Init(new EnemyFightState(this));
    }
  }
}