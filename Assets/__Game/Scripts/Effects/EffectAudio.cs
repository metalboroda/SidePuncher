using Assets.__Game.Scripts.Components;
using Assets.__Game.Scripts.SOs;
using UnityEngine;

namespace Assets.__Game.Scripts.Effects
{
  [RequireComponent(typeof(AudioSource))]
  public class EffectAudio : MonoBehaviour
  {
    [SerializeField] private AudioContainerSO hitSFX;

    private AudioSource _audioSource;

    private AudioComponent _audioComponent;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
      _audioComponent = new AudioComponent(_audioSource);
    }

    private void OnEnable()
    {
      PlayRandomHitSound();
    }

    private void PlayRandomHitSound()
    {
      AudioClip audioClip = hitSFX.GetRandomClip();

      _audioComponent.RandomPitch();
      _audioSource.PlayOneShot(audioClip);
    }
  }
}