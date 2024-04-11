using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.SOs
{
  [CreateAssetMenu(fileName = "Animations", menuName = "AnimationSystem/Animations")]
  public class AnimationsSO : ScriptableObject
  {
    public List<AnimationClip> idleAnimations = new List<AnimationClip>();

    [Space]
    public List<AnimationClip> attackAnimations = new List<AnimationClip>();

    public string GetRandomIdleAnimation()
    {
      return GetRandomAnimation(idleAnimations);
    }

    public string GetRandomAttackAnimation()
    {
      return GetRandomAnimation(attackAnimations);
    }

    private string GetRandomAnimation(List<AnimationClip> animations)
    {
      if (animations.Count == 0)
        return null;

      int randomIndex = Random.Range(0, animations.Count);

      return animations[randomIndex].name;
    }
  }
}