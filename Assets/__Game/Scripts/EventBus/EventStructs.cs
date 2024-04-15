using UnityEngine;

namespace EventBus
{
  public class EventStructs { }

  #region Player
  public struct PlayerDead : IEvent { }
  #endregion

  #region Enemy
  public struct EnemyDead : IEvent
  {
    public GameObject gameObject;
  }
  #endregion

  #region Spawner
  public struct WaveCompleted : IEvent
  {
    public int waveCount;
  }
  #endregion
}