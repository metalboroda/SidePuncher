using Assets.__Game.Scripts.Interfaces;
using Lean.Pool;
using UnityEngine;

namespace Assets.__Game.Scripts.Particles
{
  public class SurfaceEffect : MonoBehaviour, ISurfaceEffect
  {
    [SerializeField] private GameObject particlePrefab;

    public void SpawnEffect(Collider collider)
    {
      if (particlePrefab == null) return;

      Vector3 contactPoint = collider.ClosestPoint(transform.position);
      LeanPool.Spawn(particlePrefab, contactPoint, Quaternion.identity);
    }
  }
}