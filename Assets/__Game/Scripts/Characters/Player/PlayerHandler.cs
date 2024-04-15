using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Interfaces;
using EventBus;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerHandler : CharacterHandlerBase, IDamageable
  {
    private PlayerController _playerController;

    private EventBinding<EnemyDead> _enemyDeadEvent;

    protected override void Awake()
    {
      base.Awake();

      _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
      _enemyDeadEvent = new EventBinding<EnemyDead>(Recovery);
    }

    private void OnDisable()
    {
      _enemyDeadEvent.Remove(Recovery);
    }

    protected override void Start()
    {
      base.Start();
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;
        CapsuleCollider.enabled = false;
        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }

    public void Recovery(EnemyDead enemyDead)
    {
      int value = enemyDead.healthRecoveryValue;

      if (CurrentHealth < MaxHealth)
      {
        CurrentHealth += value;

        if (CurrentHealth > MaxHealth)
          CurrentHealth = MaxHealth;
      }
    }

    public override void Death(float delay)
    {
      EventBus<PlayerDead>.Raise(new PlayerDead());
    }
  }
}