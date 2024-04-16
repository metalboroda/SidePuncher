using Assets.__Game.Scripts.SOs;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterAudioHandler : MonoBehaviour
  {
    [SerializeField] protected AudioContainerSO attackSFX;

    protected virtual void PlayRandomAttackSound() { }
  }
}