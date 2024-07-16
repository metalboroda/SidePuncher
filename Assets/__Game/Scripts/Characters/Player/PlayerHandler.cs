using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Interfaces;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerHandler : CharacterHandlerBase, IDamageable
  {
    private CapsuleCollider _capsuleCollider;

    private PlayerController _playerController;

    private EventBinding<EnemyDead> _enemyDeadEvent;

    protected override void Awake() {
      _capsuleCollider = GetComponent<CapsuleCollider>();

      base.Awake();

      _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable() {
      _enemyDeadEvent = new EventBinding<EnemyDead>(Recovery);
    }

    private void OnDisable() {
      _enemyDeadEvent.Remove(Recovery);
    }

    protected override void Start() {
      base.Start();
    }

    public void Damage(int damage) {
      CurrentHealth -= damage;

      EventBus<PlayerDamaged>.Raise(new PlayerDamaged());

      if (CurrentHealth <= 0) {
        CurrentHealth = 0;
        CapsuleCollider.enabled = false;
        _playerController.FiniteStateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }

    public void Recovery(EnemyDead enemyDead) {
      int value = enemyDead.HealthRecoveryValue;

      if (CurrentHealth < MaxHealth) {
        CurrentHealth += value;

        if (CurrentHealth > MaxHealth)
          CurrentHealth = MaxHealth;
      }
    }

    public override void Death(float delay) {
      _capsuleCollider.enabled = false;

      EventBus<PlayerDead>.Raise(new PlayerDead());
    }
  }
}