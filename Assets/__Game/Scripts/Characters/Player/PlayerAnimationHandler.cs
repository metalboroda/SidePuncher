using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAnimationHandler : CharacterAnimationHandler
  {
    [Space]
    [SerializeField] private PlayerAttackHandler attackHandler;

    private void OnEnable()
    {
      attackHandler.AttackTriggered += PlayRandomAttackAnimation;
    }

    private void OnDisable()
    {
      attackHandler.AttackTriggered -= PlayRandomAttackAnimation;
    }

    public void PlayRandomAttackAnimation()
    {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur);
      OnAnimtionEnds(0.8f, PlayRandomIdleAnimation);
    }
  }
}