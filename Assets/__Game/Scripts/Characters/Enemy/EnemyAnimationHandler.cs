using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyAnimationHandler : CharacterAnimationHandler
  {
    [Space]
    [SerializeField] private EnemyAttackHandler enemyAttackHandler;

    private void OnEnable()
    {
      enemyAttackHandler.AttackTriggered += PlayRandomAttackAnimation;
    }

    private void OnDisable()
    {
      enemyAttackHandler.AttackTriggered -= PlayRandomAttackAnimation;
    }

    public void PlayRandomAttackAnimation()
    {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur);
      OnAnimtionEnds(0.8f, PlayRandomIdleAnimation);
    }
  }
}