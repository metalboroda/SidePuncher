using Assets.__Game.Scripts.Components;
using Assets.__Game.Scripts.SOs;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  [RequireComponent(typeof(AudioSource))]
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

    public virtual void PlayRandomAttackSound()
    {
      PlayOneShotSound(AttackSFX.GetRandomClip());
    }

    public virtual void PlayRandomDamageSound()
    {
      PlayOneShotSound(DamageSFX.GetRandomClip());
    }

    public virtual void PlayRandomDeathSound()
    {
      PlayOneShotSound(DeathSFX.GetRandomClip());
    }

    public virtual void PlayOneShotSound(AudioClip audioClip)
    {
      AudioClip clip = audioClip;

      AudioComponent.RandomPitch();
      AudioSource.PlayOneShot(clip);
    }
  }
}