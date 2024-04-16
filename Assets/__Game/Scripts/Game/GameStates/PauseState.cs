using UnityEngine;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public class PauseState : GameBaseState
  {
    public PauseState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper)
    {
    }

    public override void Enter()
    {
      InputService.PausePressed += () => { GameStateMachine.ChangeState(new GameplayState(GameBootstrapper)); };

      Time.timeScale = 0f;
    }

    public override void Exit()
    {
      InputService.PausePressed -= () => { GameStateMachine.ChangeState(new GameplayState(GameBootstrapper)); };

      Time.timeScale = 1f;
    }
  }
}