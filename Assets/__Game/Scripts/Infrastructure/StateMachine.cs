using System;

namespace Assets.__Game.Scripts.Infrastructure
{
  public class StateMachine
  {
    public event Action<IState> StateChanged;

    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }

    public void Init(IState initState)
    {
      CurrentState = initState;
      CurrentState.Enter();
    }

    public void ChangeState(IState newState)
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