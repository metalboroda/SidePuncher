﻿using Assets.__Game.Scripts.Services;

namespace Assets.__Game.Scripts.Game.GameStates
{
  internal class GameplayState : GameBaseState
  {
    public GameplayState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper)
    {
    }

    public override void Enter()
    {
      InputService.PausePressed += () => { GameStateMachine.ChangeState(new PauseState(GameBootstrapper)); };
    }

    public override void Exit()
    {
      InputService.PausePressed -= () => { GameStateMachine.ChangeState(new PauseState(GameBootstrapper)); };
    }
  }
}
