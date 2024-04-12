using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Interfaces;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerHandler : CharacterHandlerBase, IDamageable
  {
    private PlayerController _playerController;

    private void Awake()
    {
      _playerController = GetComponent<PlayerController>();
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

        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }
  }
}