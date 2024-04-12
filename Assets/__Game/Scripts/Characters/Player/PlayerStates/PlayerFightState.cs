using Assets.__Game.Scripts.Services;

namespace Assets.__Game.Scripts.Characters.Player.PlayerStates
{
  internal class PlayerFightState : PlayerBaseState
  {
    private InputService _inputService;

    public PlayerFightState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
      _inputService = new InputService();

      _inputService.LeftAttackTriggered += PlayerAttackHandler.LeftAttack;
      _inputService.RightAttackTriggered += PlayerAttackHandler.RightAttack;

      PlayerAnimationHandler.PlayRandomIdleAnimation();
    }

    public override void Exit()
    {
      _inputService.LeftAttackTriggered -= PlayerAttackHandler.LeftAttack;
      _inputService.RightAttackTriggered -= PlayerAttackHandler.RightAttack;
      _inputService.Dispose();
    }
  }
}
