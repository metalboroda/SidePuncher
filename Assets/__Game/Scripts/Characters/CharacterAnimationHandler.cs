using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.SOs;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterAnimationHandler : MonoBehaviour
  {
    [SerializeField] protected AnimationsSO AnimationsSO;

    [Header("Param's")]
    [SerializeField] protected float CrossDur = 0.2f;

    [Space]
    [SerializeField] protected Animator Animator;

    protected EventBus EventBus;

    [Inject]
    public void Construct(EventBus eventBus)
    {
      EventBus = eventBus;
    }

    public void PlayRandomIdleAnimation()
    {
      PlayCrossfade(AnimationsSO.GetRandomIdleAnimation());
    }

    public void PlayCrossfade(string animationName)
    {
      if (Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) return;

      Animator.CrossFadeInFixedTime(animationName, CrossDur);
    }

    public void OnAnimtionEnds(float endPercent, Action action = null)
    {
      StartCoroutine(DoWaitForAnimationToEnd(endPercent, action));
    }

    private IEnumerator DoWaitForAnimationToEnd(float endPercent, Action callback)
    {
      yield return new WaitForSeconds(CrossDur + 0.0001f);

      while (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < endPercent)
      {
        yield return null;
      }

      callback?.Invoke();
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