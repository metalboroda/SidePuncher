using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using Assets.__Game.Scripts.Services;
using System;
using Zenject;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable
  {
    public event Action EnemyDead;

    private EnemyController _enemyController;
    private EventBus _eventBus;

    [Inject]
    public void Construct(EventBus eventBus)
    {
      _eventBus = eventBus;
    }

    protected override void Awake()
    {
      base.Awake();

      _enemyController = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
      _eventBus.PlayerDead += Victory;
    }

    private void OnDisable()
    {
      _eventBus.PlayerDead -= Victory;
    }

    protected override void Start()
    {
      base.Start();
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      _enemyController.StateMachine.ChangeState(new EnemyHitState(_enemyController));

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;
        CapsuleCollider.enabled = false;
        _enemyController.StateMachine.ChangeState(new EnemyDeathState(_enemyController));
        EnemyDead?.Invoke();
      }
    }

    public override void Victory()
    {
      _enemyController.StateMachine.ChangeState(new EnemyVictoryState(_enemyController));
    }
  }
}