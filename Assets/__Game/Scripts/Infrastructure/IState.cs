namespace Assets.__Game.Scripts.Infrastructure
{
  public interface IState
  {
    void Enter();
    void Exit();
  }

  public interface IUpdatableState : IState
  {
    void Update();
  }

  public interface IFixedUpdatableState : IState
  {
    void FixedUpdate();
  }
}