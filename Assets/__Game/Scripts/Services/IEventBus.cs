using System;

namespace Assets.__Game.Scripts.Services
{
  internal interface IEventBus
  {
    void Publish<TEvent>(TEvent @event);
    void Subscribe<TEvent>(Action<TEvent> handler);
    void Unsubscribe<TEvent>(Action<TEvent> handler);
  }
}
