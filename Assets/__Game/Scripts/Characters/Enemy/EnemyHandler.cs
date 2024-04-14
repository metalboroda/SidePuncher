using Assets.__Game.Scripts.Characters.Enemy.EnemyStates;
using Assets.__Game.Scripts.Interfaces;
using EventBus;
using Lean.Pool;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Enemy
{
  public class EnemyHandler : CharacterHandlerBase, IDamageable
  {
    public event Action EnemyDead;

    [Space]
    [SerializeField] private GameObject puppetObject;

    [Space]
    [SerializeField] private EnemyController enemyController;

    private Renderer[] _renderers;

    private EventBinding<PlayerDeathEvent> _onPlayerDeathEvent;

    protected override void Awake()
    {
      base.Awake();

      _renderers = transform.root.GetComponentsInChildren<Renderer>();
    }

    private void OnEnable()
    {
      _onPlayerDeathEvent = new EventBinding<PlayerDeathEvent>(Victory);
    }

    private void OnDisable()
    {
      _onPlayerDeathEvent.Remove(Victory);
    }

    protected override void Start()
    {
      base.Start();
    }

    public void SpawnInit(Vector3 position, Quaternion rotation)
    {
      SwitchModelVisibility(false);
      enemyController.StateMachine.ChangeStateWithDelay(new EnemyMovementState(enemyController), 0.0001f, this);
      transform.position = position;
      transform.rotation = rotation;
      enemyController.CharacterPuppetHandler.DisableRagdoll();
      CapsuleCollider.enabled = true;
      CurrentHealth = MaxHealth;
      SwitchModelVisibility(true, 0.1f);
    }

    public void Damage(int damage)
    {
      CurrentHealth -= damage;

      enemyController.StateMachine.ChangeState(new EnemyHitState(enemyController));

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;
        CapsuleCollider.enabled = false;
        enemyController.StateMachine.ChangeState(new EnemyDeathState(enemyController));
        EnemyDead?.Invoke();
      }
    }

    public override void Death(float delay)
    {
      LeanPool.Despawn(transform.root.gameObject, delay);
    }

    public override void Victory()
    {
      enemyController.StateMachine.ChangeState(new EnemyVictoryState(enemyController));
    }

    private void SwitchModelVisibility(bool enable, float delay = 0)
    {
      StartCoroutine(DoSwitchModelVisibility(enable, delay));
    }

    private IEnumerator DoSwitchModelVisibility(bool enable, float delay)
    {
      yield return new WaitForSeconds(delay);

      puppetObject.SetActive(enable);

      foreach (var renderer in _renderers)
      {
        renderer.enabled = enable;
      }
    }
  }
}