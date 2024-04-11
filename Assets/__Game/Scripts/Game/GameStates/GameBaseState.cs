using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public abstract class GameBaseState : State
  {
    protected GameBootstrapper GameBootstrapper;
    protected StateMachine GameStateMachine;
    protected SceneLoader SceneLoader;

    public GameBaseState(GameBootstrapper gameBootstrapper)
    {
      GameBootstrapper = gameBootstrapper;
      GameStateMachine = gameBootstrapper.GameStateMachine;
      SceneLoader = gameBootstrapper.SceneLoader;
    }
  }
}