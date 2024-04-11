namespace Assets.__Game.Scripts.Infrastructure
{
  public abstract class State
  {
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
  }
}