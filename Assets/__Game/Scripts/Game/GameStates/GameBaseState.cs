using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public abstract class GameBaseState : State
  {
    protected GameBootstrapper GameBootstrapper;
    protected FiniteStateMachine FiniteStateMachine;
    protected SceneLoader SceneLoader;

    public GameBaseState(GameBootstrapper gameBootstrapper) {
      GameBootstrapper = gameBootstrapper;
      FiniteStateMachine = GameBootstrapper.FiniteStateMachine;
      SceneLoader = GameBootstrapper.SceneLoader;
    }
  }
}