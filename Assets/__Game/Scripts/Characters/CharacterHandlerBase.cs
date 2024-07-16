using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterHandlerBase : MonoBehaviour
  {
    public event Action<int> HealthChanged;

    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [field: SerializeField] public int Power { get; private set; } = 25;

    [Space]
    [SerializeField] private float deathTime;

    [field: Space]
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    [field: SerializeField] public LayerMask AllyLayer { get; private set; }

    private int _currentHealth;

    public int CurrentHealth {
      get => _currentHealth;
      set {
        _currentHealth = value;

        HealthChanged?.Invoke(_currentHealth);
      }
    }

    [HideInInspector]
    public CapsuleCollider CapsuleCollider;

    protected virtual void Awake() {
      CapsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected virtual void Start() {
      CurrentHealth = MaxHealth;
    }

    public virtual void Death(float delay) { }

    public virtual void Victory() { }
  }
}