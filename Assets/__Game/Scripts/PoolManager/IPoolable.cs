namespace Assets.__Game.Scripts.PoolManager
{
  public interface IPoolable
  {
    void OnSpawned();
    void OnDespawned();
  }
}