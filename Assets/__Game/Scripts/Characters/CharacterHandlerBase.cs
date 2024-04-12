using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterHandlerBase : MonoBehaviour
  {
    [SerializeField] protected int MaxHealth = 100;
    [field: SerializeField] public int Power { get; private set; } = 25;

    [field: Space]
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

    protected int CurrentHealth;

    protected virtual void Start()
    {
      CurrentHealth = MaxHealth;
    }
  }
}