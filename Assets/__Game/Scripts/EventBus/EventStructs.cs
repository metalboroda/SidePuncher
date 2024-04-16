using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace EventBus
{
  public class EventStructs { }

  #region Game
  public struct GameStateChanged : IEvent
  {
    public State State;
  }
  #endregion

  #region Player
  public struct PlayerDamaged : IEvent { }
  public struct PlayerDead : IEvent { }
  #endregion

  #region Enemy
  public struct EnemyDead : IEvent
  {
    public GameObject GameObject;
    public int HealthRecoveryValue;
  }
  #endregion

  #region Spawner
  public struct WaveCompleted : IEvent
  {
    public int WaveCount;
  }
  #endregion

  #region Settings
  public struct MusicSwitched : IEvent { }
  public struct SFXSwitched : IEvent { }
  #endregion
}