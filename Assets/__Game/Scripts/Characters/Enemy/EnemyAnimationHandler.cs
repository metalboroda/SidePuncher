using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyAnimationHandler : CharacterAnimationHandler
  {
    private EnemyController _enemyController;

    protected override void Awake()
    {
      base.Awake();

      _enemyController = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
      _enemyController.EnemyAttackHandler.AttackTriggered += PlayRandomAttackAnimation;
    }

    private void OnDisable()
    {
      _enemyController.EnemyAttackHandler.AttackTriggered -= PlayRandomAttackAnimation;
    }

    public void PlayRandomAttackAnimation()
    {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur);
      OnAnimtionEnds(AnimationEndTime, PlayRandomIdleAnimation);
    }
  }
}