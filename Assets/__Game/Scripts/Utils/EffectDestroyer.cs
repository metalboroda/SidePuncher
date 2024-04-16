using Lean.Pool;
using UnityEngine;

namespace Assets.__Game.Scripts.Utils
{
  public class EffectDestroyer : MonoBehaviour
  {
    [SerializeField] private float destroyTime;

    public void DestroyEffect()
    {
      LeanPool.Despawn(this, destroyTime);
    }
  }
}