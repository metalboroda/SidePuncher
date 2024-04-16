using EventBus;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  [RequireComponent(typeof(AudioSource))]
  public class PlayerAudioHandler : CharacterAudioHandler
  {
    private PlayerController _playerController;

    private EventBinding<PlayerDead> _playerDeadEvent;

    protected override void Awake()
    {
      base.Awake();

      _playerController = GetComponent<PlayerController>();

    }

    private void OnEnable()
    {
      _playerController.PlayerAttackHandler.AttackTriggered += PlayRandomAttackSound;
      _playerDeadEvent = new EventBinding<PlayerDead>(PlayRandomDeathSound);
    }

    private void OnDisable()
    {
      _playerController.PlayerAttackHandler.AttackTriggered -= PlayRandomAttackSound;
      _playerDeadEvent.Remove(PlayRandomDeathSound);
    }

    protected override void PlayRandomAttackSound()
    {
      base.PlayRandomAttackSound();
    }

    protected override void PlayRandomDamageSound()
    {
      base.PlayRandomDamageSound();
    }

    protected override void PlayRandomDeathSound()
    {
      base.PlayRandomDeathSound();
    }
  }
}