using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAnimationHandler : CharacterAnimationHandler
  {
    private PlayerAttackHandler _playerAttackHandler;

    protected override void Awake() {
      base.Awake();

      _playerAttackHandler = GetComponent<PlayerAttackHandler>();
    }

    private void OnEnable() {
      _playerAttackHandler.AttackTriggered += PlayRandomAttackAnimation;
    }

    private void OnDisable() {
      _playerAttackHandler.AttackTriggered -= PlayRandomAttackAnimation;
    }

    public override void PlayRandomAttackAnimation() {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur / AttackCrossDivision);

      base.PlayRandomAttackAnimation();
    }
  }
}