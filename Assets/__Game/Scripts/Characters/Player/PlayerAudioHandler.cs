using Assets.__Game.Scripts.Components;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  [RequireComponent(typeof(AudioSource))]
  public class PlayerAudioHandler : CharacterAudioHandler
  {
    private AudioSource _audioSource;

    private PlayerController _playerController;
    private AudioComponent _audioComponent;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
      _playerController = GetComponent<PlayerController>();
      _audioComponent = new AudioComponent(_audioSource);
    }

    private void OnEnable()
    {
      _playerController.PlayerAttackHandler.AttackTriggered += PlayRandomAttackSound;
    }

    private void OnDisable()
    {
      _playerController.PlayerAttackHandler.AttackTriggered -= PlayRandomAttackSound;
    }

    protected override void PlayRandomAttackSound()
    {
      AudioClip attackClip = attackSFX.GetRandomClip();

      _audioComponent.RandomPitch();
      _audioSource.PlayOneShot(attackClip);
    }
  }
}