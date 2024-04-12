using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  public abstract class CharacterControllerBase : MonoBehaviour
  {
    public StateMachine StateMachine;

    protected virtual void Awake()
    {
      StateMachine = new StateMachine();
    }
  }
}