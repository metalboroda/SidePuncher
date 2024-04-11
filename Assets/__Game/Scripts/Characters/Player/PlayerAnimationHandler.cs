using Assets.__Game.Scripts.Services;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAnimationHandler : CharacterAnimationHandler
  {
    private void OnEnable()
    {
      EventBus.AttackTriggered += PlayRandomLeftAttackAnimation;
    }

    private void OnDisable()
    {
      EventBus.AttackTriggered -= PlayRandomLeftAttackAnimation;
    }

    public void PlayRandomLeftAttackAnimation()
    {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur);
      OnAnimtionEnds(0.8f, PlayRandomIdleAnimation);
    }
  }
}