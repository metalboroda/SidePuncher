using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public abstract class GameBaseState : State
  {
    protected GameBootstrapper GameBootstrapper;
    protected SceneLoader SceneLoader;

    public GameBaseState(GameBootstrapper gameBootstrapper)
    {
      GameBootstrapper = gameBootstrapper;
      SceneLoader = gameBootstrapper.SceneLoader;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
  }
}
