namespace Assets.__Game.Scripts.Characters.Player.PlayerStates
{
  internal class PlayerFightState : PlayerBaseState
  {
    public PlayerFightState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
      PlayerAnimationHandler.PlayRandomIdleAnimation();
    }
  }
}
