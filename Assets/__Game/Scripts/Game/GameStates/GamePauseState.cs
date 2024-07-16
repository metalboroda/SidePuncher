using UnityEngine;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public class GamePauseState : GameBaseState
  {
    public GamePauseState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper) { }

    public override void Enter() {
      Time.timeScale = 0f;
    }

    public override void Exit() {
      Time.timeScale = 1f;
    }
  }
}