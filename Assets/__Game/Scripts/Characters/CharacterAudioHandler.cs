using Assets.__Game.Scripts.Components;
using Assets.__Game.Scripts.SOs;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterAudioHandler : MonoBehaviour
  {
    [SerializeField] protected AudioContainerSO AttackSFX;
    [SerializeField] protected AudioContainerSO DamageSFX;
    [SerializeField] protected AudioContainerSO DeathSFX;

    protected AudioSource AudioSource;
    protected AudioComponent AudioComponent;

    protected virtual void Awake()
    {
      AudioSource = GetComponent<AudioSource>();
      AudioComponent = new AudioComponent(AudioSource);
    }

    protected virtual void PlayRandomAttackSound()
    {
      PlayOneShotSound(AttackSFX.GetRandomClip());
    }

    protected virtual void PlayRandomDamageSound()
    {
      PlayOneShotSound(DamageSFX.GetRandomClip());
    }

    protected virtual void PlayRandomDeathSound()
    {
      PlayOneShotSound(DeathSFX.GetRandomClip());
    }

    protected virtual void PlayOneShotSound(AudioClip audioClip)
    {
      AudioClip clip = audioClip;

      AudioComponent.RandomPitch();
      AudioSource.PlayOneShot(clip);
    }
  }
}