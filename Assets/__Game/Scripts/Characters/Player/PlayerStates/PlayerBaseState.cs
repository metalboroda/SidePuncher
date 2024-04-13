using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Characters.Player.PlayerStates
{
  public class PlayerBaseState : State
  {
    protected PlayerController PlayerController;
    protected PlayerHandler PlayerHandler;
    protected PlayerAttackHandler PlayerAttackHandler;
    protected PlayerAnimationHandler PlayerAnimationHandler;
    protected CharacterPuppetHandler CharacterPuppetHandler;

    public PlayerBaseState(PlayerController playerController)
    {
      PlayerController = playerController;
      PlayerHandler = PlayerController.PlayerHandler;
      PlayerAttackHandler = PlayerController.PlayerAttackHandler;
      PlayerAnimationHandler = PlayerController.PlayerAnimationHandler;
      CharacterPuppetHandler = PlayerController.CharacterPuppetHandler;
    }
  }
}
