using Assets.__Game.Resources.Scripts.StateMachine;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters
{
  [SelectionBase]
  public abstract class CharacterControllerBase : MonoBehaviour
  {
    public CharacterUIHandler CharacterUIHandler;
    public CharacterAudioHandler CharacterAudioHandler;

    public FiniteStateMachine FiniteStateMachine;

    protected virtual void Awake() {
      FiniteStateMachine = new FiniteStateMachine();
    }

    private void Update() {
      FiniteStateMachine.CurrentState.Update();
    }

    private void FixedUpdate() {
      FiniteStateMachine.CurrentState.FixedUpdate();
    }
  }
}