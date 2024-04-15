using Assets.__Game.Scripts.Interfaces;
using Lean.Pool;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public class AttackTrigger : MonoBehaviour, IAttackEffect
  {
    [SerializeField] private GameObject effectPrefab;

    private CharacterHandlerBase _characterHandler;

    private void Awake()
    {
      _characterHandler = GetComponentInParent<CharacterHandlerBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if ((_characterHandler.EnemyLayer & 1 << other.gameObject.layer) != 0
        && other.TryGetComponent(out IDamageable damageable))
      {
        damageable.Damage(_characterHandler.Power);

        SpawnEffect();
      }
    }

    public void SpawnEffect()
    {
      Instantiate(effectPrefab, transform.position, transform.rotation);
    }
  }
}