using Assets.__Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.__Game.Scripts
{
  public class TestScript : MonoBehaviour
  {
    private EventBus _eventBus;

    [Inject]
    public void Construct(EventBus eventBus)
    {
      _eventBus = eventBus;
    }
  }
}