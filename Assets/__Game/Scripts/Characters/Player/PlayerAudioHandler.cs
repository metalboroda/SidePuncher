using EventBus;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
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

    public override void PlayRandomAttackSound()
    {
      base.PlayRandomAttackSound();
    }

    public override void PlayRandomDamageSound()
    {
      base.PlayRandomDamageSound();
    }

    public override void PlayRandomDeathSound()
    {
      base.PlayRandomDeathSound();
    }
  }
}