using UnityEngine;

namespace EventBus
{
  public class EventStructs { }

  public struct PlayerDeathEvent : IEvent { }

  public struct EnemyDeathEvent : IEvent
  {
    public GameObject gameObject;
  }
}