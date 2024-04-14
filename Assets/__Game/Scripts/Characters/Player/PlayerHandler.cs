using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Interfaces;
using EventBus;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerHandler : CharacterHandlerBase, IDamageable
  {
    private PlayerController _playerController;

    protected override void Awake()
    {
      base.Awake();

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
        CapsuleCollider.enabled = false;
        _playerController.StateMachine.ChangeState(new PlayerDeathState(_playerController));
      }
    }

    public override void Death()
    {
      EventBus<PlayerDeathEvent>.Raise(new PlayerDeathEvent());
    }
  }
}