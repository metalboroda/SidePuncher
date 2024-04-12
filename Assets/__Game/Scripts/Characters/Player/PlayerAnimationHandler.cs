using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAnimationHandler : CharacterAnimationHandler
  {
    [Space]
    [SerializeField] private PlayerAttackHandler playerAttackHandler;

    private void OnEnable()
    {
      playerAttackHandler.AttackTriggered += PlayRandomAttackAnimation;
    }

    private void OnDisable()
    {
      playerAttackHandler.AttackTriggered -= PlayRandomAttackAnimation;
    }

    public void PlayRandomAttackAnimation()
    {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur);
      OnAnimtionEnds(0.8f, PlayRandomIdleAnimation);
    }
  }
}