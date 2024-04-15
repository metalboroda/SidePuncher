using Assets.__Game.Scripts.Interfaces;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public class AttackTrigger : MonoBehaviour
  {
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
      }

      if (other.TryGetComponent(out ISurfaceEffect surfaceEffect))
        surfaceEffect.SpawnEffect(other);
    }
  }
}