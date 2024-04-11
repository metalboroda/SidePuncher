using System;
using System.Collections.Generic;

namespace Assets.__Game.Scripts.Services
{
  public class EventBus
  {
    private readonly Dictionary<Type, List<object>> _subscribers = new Dictionary<Type, List<object>>();

    public void Publish<T>(T message)
    {
      Type messageType = typeof(T);

      if (_subscribers.TryGetValue(messageType, out var subscribers))
      {
        foreach (var subscriber in subscribers)
        {
          ((Action<T>)subscriber)(message);
        }
      }
    }

    public void Subscribe<T>(Action<T> callback)
    {
      Type messageType = typeof(T);

      if (!_subscribers.ContainsKey(messageType))
      {
        _subscribers[messageType] = new List<object>();
      }

      _subscribers[messageType].Add(callback);
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
      Type messageType = typeof(T);

      if (_subscribers.TryGetValue(messageType, out var subscribers))
      {
        subscribers.Remove(callback);
      }
    }
  }
}