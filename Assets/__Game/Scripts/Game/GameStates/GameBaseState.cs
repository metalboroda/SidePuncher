using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Game.GameStates
{
  public abstract class GameBaseState : IState
  {
    protected GameBootstrapper _gameBootstrapper;
    protected SceneLoader _sceneLoader;

    public GameBaseState(GameBootstrapper gameBootstrapper)
    {
      _gameBootstrapper = gameBootstrapper;
      _sceneLoader = gameBootstrapper.SceneLoader;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
  }
}
