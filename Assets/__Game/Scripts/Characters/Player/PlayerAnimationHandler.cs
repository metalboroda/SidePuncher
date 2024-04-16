﻿using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAnimationHandler : CharacterAnimationHandler
  {
    private PlayerAttackHandler _playerAttackHandler;

    protected override void Awake()
    {
      base.Awake();

      _playerAttackHandler = GetComponent<PlayerAttackHandler>();
    }

    private void OnEnable()
    {
      _playerAttackHandler.AttackTriggered += PlayRandomAttackAnimation;
    }

    private void OnDisable()
    {
      _playerAttackHandler.AttackTriggered -= PlayRandomAttackAnimation;
    }

    public void PlayRandomAttackAnimation()
    {
      Animator.CrossFadeInFixedTime(AnimationsSO.GetRandomAttackAnimation(), CrossDur);
      OnAnimtionEnds(0.99f, PlayRandomIdleAnimation);
    }
  }
}