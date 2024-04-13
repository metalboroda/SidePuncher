using System.Collections.Generic;
using UnityEngine;

namespace Assets.__Game.Scripts.SOs
{
  [CreateAssetMenu(fileName = "Animations", menuName = "AnimationSystem/Animations")]
  public class AnimationsSO : ScriptableObject
  {
    public List<AnimationClip> idleAnimations = new List<AnimationClip>();
    public List<AnimationClip> walkAnimations = new List<AnimationClip>();
    public List<AnimationClip> attackAnimations = new List<AnimationClip>();
    public List<AnimationClip> hitAnimations = new List<AnimationClip>();
    public List<AnimationClip> victoryAnimations = new List<AnimationClip>();
    public List<AnimationClip> deathAnimations = new List<AnimationClip>();

    public string GetRandomIdleAnimation()
    {
      return GetRandomAnimation(idleAnimations);
    }

    public string GetRandomWalkAnimation()
    {
      return GetRandomAnimation(walkAnimations);
    }

    public string GetRandomAttackAnimation()
    {
      return GetRandomAnimation(attackAnimations);
    }

    public string GetRandomHitAnimation()
    {
      return GetRandomAnimation(hitAnimations);
    }

    public string GetRandomVictoryAnimation()
    {
      return GetRandomAnimation(victoryAnimations);
    }

    public string GetRandomDeathAnimation()
    {
      return GetRandomAnimation(deathAnimations);
    }

    private string GetRandomAnimation(List<AnimationClip> animations)
    {
      if (animations.Count == 0) return null;

      int randomIndex = Random.Range(0, animations.Count);

      return animations[randomIndex].name;
    }
  }
}