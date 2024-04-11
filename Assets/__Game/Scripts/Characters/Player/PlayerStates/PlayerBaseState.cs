using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Characters.Player.PlayerStates
{
  public class PlayerBaseState : State
  {
    protected PlayerController PlayerController;
    protected PlayerHandler PlayerHandler;
    protected PlayerAnimationHandler PlayerAnimationHandler;

    public PlayerBaseState(PlayerController playerController)
    {
      PlayerController = playerController;
      PlayerHandler = PlayerController.PlayerHandler;
      PlayerAnimationHandler = PlayerController.PlayerAnimationHandler;
    }
  }
}
