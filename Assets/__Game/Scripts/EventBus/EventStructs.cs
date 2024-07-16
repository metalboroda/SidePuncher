using Assets.__Game.Scripts.Enums;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.__Game.Scripts.EventBus
{
  public class EventStructs
  {
    #region Infrastructure
    public struct StateChanged : IEvent
    {
      public State State;
    }
    #endregion

    #region Settings
    public struct MusicSwitched : IEvent { }
    public struct SFXSwitched : IEvent { }
    #endregion

    #region UI
    public struct UIButtonPressed : IEvent
    {
      public UIButtonEnums Button;
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
  }
}