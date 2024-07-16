using Assets.__Game.Resources.Scripts.StateMachine;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  [SelectionBase]
  public abstract class CharacterControllerBase : MonoBehaviour
  {
    public CharacterUIHandler CharacterUIHandler;
    public CharacterAudioHandler CharacterAudioHandler;

    public FiniteStateMachine StateMachine;

    protected virtual void Awake() {
      StateMachine = new FiniteStateMachine();
    }

    private void Update() {
      StateMachine.CurrentState.Update();
    }

    private void FixedUpdate() {
      StateMachine.CurrentState.FixedUpdate();
    }
  }
}