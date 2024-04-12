using Assets.__Game.Scripts.SOs;
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

    private Coroutine _animationEndRoutine;

    protected Animator Animator;

    protected virtual void Awake()
    {
      Animator = GetComponent<Animator>();
    }

    public void PlayRandomIdleAnimation()
    {
      PlayCrossfade(AnimationsSO.GetRandomIdleAnimation());
    }

    public void PlayRandomWalkAnimation()
    {
      PlayCrossfade(AnimationsSO.GetRandomWalkAnimation());
    }

    public void PlayRandomHitAnimation()
    {
      PlayCrossfade(AnimationsSO.GetRandomHitAnimation());
    }

    public void PlayRandomDeathAnimation()
    {
      PlayCrossfade(AnimationsSO.GetRandomDeathAnimation());
    }

    public void DeathRandomRotation()
    {
      int rand = Random.Range(50, 70);
      Vector3 eulerAngle = transform.rotation.eulerAngles;
      eulerAngle.y += Random.Range(-rand, rand);

      transform.rotation = Quaternion.Euler(eulerAngle);
    }

    public void PlayCrossfade(string animationName)
    {
      if (Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) return;

      Animator.CrossFadeInFixedTime(animationName, CrossDur);
    }

    public void OnAnimtionEnds(float endPercent, Action action = null)
    {
      if (_animationEndRoutine != null)
        StopCoroutine(_animationEndRoutine);

      _animationEndRoutine = StartCoroutine(DoWaitForAnimationToEnd(endPercent, action));
    }

    private IEnumerator DoWaitForAnimationToEnd(float endPercent, Action callback)
    {
      yield return new WaitForSeconds(CrossDur);

      while (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < endPercent)
      {
        yield return null;
      }

      callback?.Invoke();
    }

    public void StopCoroutines()
    {
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
  }
}