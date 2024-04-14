namespace Assets.__Game.Scripts.Characters.Player.PlayerStates
{
  internal class PlayerDeathState : PlayerBaseState
  {
    public PlayerDeathState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
      PlayerAnimationHandler.PlayRandomDeathAnimation();
      PlayerAnimationHandler.DeathRandomRotation();
      PlayerAnimationHandler.StopCoroutines();
      CharacterPuppetHandler.EnableRagdoll();
      PlayerHandler.Death();
    }
  }
}