using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Interfaces;
using Assets.__Game.Scripts.Services;
using Zenject;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerHandler : CharacterHandlerBase, IDamageable
  {
    private PlayerController _playerController;
    private EventBus _eventBus;

    [Inject]
    public void Construct(EventBus eventBus)
    {
      _eventBus = eventBus;
    }

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
        _eventBus.RaisePlayerDead();
      }
    }
  }
}