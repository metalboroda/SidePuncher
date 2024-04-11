using System;

namespace Assets.__Game.Scripts.Infrastructure
{
  public class GameStateMachine
  {
    public event Action<State> StateChanged;

    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    public void Init(State initState)
    {
      CurrentState = initState;
      CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
      if (newState == CurrentState) return;

      PreviousState = CurrentState;
      CurrentState.Exit();
      CurrentState = newState;
      CurrentState.Enter();
      StateChanged?.Invoke(CurrentState);
    }
  }
}