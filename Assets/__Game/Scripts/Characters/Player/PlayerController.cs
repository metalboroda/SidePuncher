using Assets.__Game.Scripts.Characters.Player.PlayerStates;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerController : CharacterControllerBase
  {
    public PlayerHandler PlayerHandler;
    public PlayerAttackHandler PlayerAttackHandler;
    public PlayerAnimationHandler PlayerAnimationHandler;
    public CharacterPuppetHandler CharacterPuppetHandler;

    protected override void Awake() {
      base.Awake();
    }

    private void Start() {
      FiniteStateMachine.Init(new PlayerFightState(this));
    }
  }
}