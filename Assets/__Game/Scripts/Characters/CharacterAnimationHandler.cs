using Assets.__Game.Scripts.SOs;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterAnimationHandler : MonoBehaviour
  {
    [SerializeField] protected AnimationsSO AnimationsSO;

    [Header("Param's")]
    [SerializeField] protected float CrossDur = 0.2f;
    [field: Space]
    [field: SerializeField] public float AnimationEndTime = 0.8f;
    [field: SerializeField] public float AttackCrossDivision = 1.5f;
    [Header("Underground Animation")]
    [SerializeField] private float undergroundY = -5f;
    [SerializeField] private float undergroundDelay = 7.25f;
    [SerializeField] private float undergroundSpeed = 5f;

    private Coroutine _animationEndRoutine;

    protected Animator Animator;

    protected virtual void Awake() {
      Animator = GetComponent<Animator>();
    }

    private void OnDestroy() {
      DOTween.Kill(transform);
    }

    public void PlayRandomIdleAnimation() {
      PlayCrossfade(AnimationsSO.GetRandomIdleAnimation());
    }

    public void PlayRandomWalkAnimation() {
      PlayCrossfade(AnimationsSO.GetRandomWalkAnimation());
    }

    public virtual void PlayRandomAttackAnimation() {
      OnAnimtionEnds(AnimationEndTime, PlayRandomIdleAnimation);
    }

    public void PlayRandomHitAnimation() {
      PlayCrossfade(AnimationsSO.GetRandomHitAnimation());
    }

    public void PlayRandomVictoryAnimation() {
      PlayCrossfade(AnimationsSO.GetRandomVictoryAnimation());
    }

    public void PlayRandomDeathAnimation() {
      PlayCrossfade(AnimationsSO.GetRandomDeathAnimation());
    }

    public void PlayRandomDyingAnimation() {
      PlayCrossfade(AnimationsSO.GetRandomDyingAnimation());
    }

    public void DeathRandomRotation() {
      int minRotation = 50;
      int maxRotation = 70;

      int randRotation = Random.Range(minRotation, maxRotation);
      Vector3 eulerAngle = transform.rotation.eulerAngles;
      float rotationDuration = 0.15f;

      eulerAngle.y += Random.Range(-randRotation, randRotation);

      transform.DORotate(eulerAngle, rotationDuration);
    }

    public void UndergroundAnimation() {
      transform.DOMoveY(undergroundY, undergroundSpeed)
        .SetDelay(undergroundDelay)
        .SetSpeedBased(true);
    }

    #region Utils
    public void PlayCrossfade(string animationName) {
      Animator.CrossFadeInFixedTime(animationName, CrossDur);
    }

    public void OnAnimtionEnds(float endPercent, Action action = null) {
      if (_animationEndRoutine != null)
        StopCoroutine(_animationEndRoutine);

      _animationEndRoutine = StartCoroutine(DoWaitForAnimationToEnd(endPercent, action));
    }

    private IEnumerator DoWaitForAnimationToEnd(float endPercent, Action callback) {
      yield return new WaitForSeconds(CrossDur);

      while (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < endPercent) {
        yield return null;
      }

      callback?.Invoke();
    }

    public void StopCoroutines() {
      if (_animationEndRoutine != null)
        StopCoroutine(_animationEndRoutine);
    }

    #region Adding animations in runtime
    /*protected void AddAnimationsToController()
        {
          if (AnimationsSO == null || Animator == null)
          {
            Debug.LogWarning("AnimationsSO or Animator not set in CharacterAnimationHandler.");
            return;
          }

          AddAnimations(AnimationsSO.idleAnimations);
          AddAnimations(AnimationsSO.leftAttackAnimations);
          AddAnimations(AnimationsSO.rightAttackAnimations);
        }*/

    /*private void AddAnimations(List<AnimationClip> animations)
    {
      if (animations == null || animations.Count == 0)
        return;

      AnimatorController animatorController = Animator.runtimeAnimatorController as AnimatorController;

      if (animatorController == null)
      {
        Debug.LogWarning("Animator Controller not found.");
        return;
      }

      foreach (AnimationClip clip in animations)
      {
        if (clip != null && !HasAnimation(animatorController, clip))
        {
          animatorController.AddMotion(clip);
        }
      }
    }*/

    /*private bool HasAnimation(AnimatorController controller, AnimationClip clip)
    {
      foreach (AnimatorControllerLayer layer in controller.layers)
      {
        foreach (ChildAnimatorState state in layer.stateMachine.states)
        {
          if (state.state.motion == clip)
          {
            return true;
          }
        }
      }
      return false;
    }*/
    #endregion
    #endregion
  }
}