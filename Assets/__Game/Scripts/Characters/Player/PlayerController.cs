using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerController : CharacterControllerBase
  {
    public PlayerHandler PlayerHandler;
    public PlayerAttackHandler PlayerAttackHandler;
    public PlayerAnimationHandler PlayerAnimationHandler;

    protected override void Awake()
    {
      base.Awake();
    }

    private void Start()
    {
      StateMachine.Init(new PlayerFightState(this));
    }
  }
}