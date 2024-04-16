﻿using UnityEngine;

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
    public int healthRecoveryValue;
  }
  #endregion

  #region Spawner
  public struct WaveCompleted : IEvent
  {
    public int waveCount;
  }
  #endregion

  #region Settings
  public struct MusicSwitched : IEvent { }
  public struct SFXSwitched : IEvent { }
  #endregion
}