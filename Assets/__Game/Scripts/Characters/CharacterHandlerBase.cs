﻿using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterHandlerBase : MonoBehaviour
  {
    [SerializeField] protected int MaxHealth = 100;
    [field: SerializeField] public int Power { get; private set; } = 25;

    [Space]
    [SerializeField] private float deathTime;

    [field: Space]
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

    protected int CurrentHealth;

    protected CapsuleCollider CapsuleCollider;

    protected virtual void Awake()
    {
      CapsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected virtual void Start()
    {
      CurrentHealth = MaxHealth;
    }

    public void Death()
    {
      Destroy(transform.root.gameObject, deathTime);
    }

    public virtual void Victory() { }
  }
}