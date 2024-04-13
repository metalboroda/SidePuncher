using UnityEngine.Events;

namespace Assets.__Game.Scripts.Services
{
  public class EventBus
  {
    #region Player
    public event UnityAction PlayerDead;
    public void RaisePlayerDead() => PlayerDead?.Invoke();
    #endregion
  }
}