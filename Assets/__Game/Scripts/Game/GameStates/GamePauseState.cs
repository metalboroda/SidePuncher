using UnityEngine;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public class GamePauseState : GameBaseState
  {
    public GamePauseState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper) { }

    public override void Enter() {
      InputService.PausePressed += () => { FiniteStateMachine.ChangeState(new GameplayState(GameBootstrapper)); };

      Time.timeScale = 0f;
    }

    public override void Exit() {
      InputService.PausePressed -= () => { FiniteStateMachine.ChangeState(new GameplayState(GameBootstrapper)); };

      Time.timeScale = 1f;
    }
  }
}