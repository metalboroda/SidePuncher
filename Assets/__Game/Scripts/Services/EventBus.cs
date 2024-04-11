using UnityEngine.Events;

namespace Assets.__Game.Scripts.Services
{
  public class EventBus
  {
    #region Player
    public event UnityAction AttackTriggered;
    public void RaiseAttackTriggered() => AttackTriggered?.Invoke();
    #endregion
  }
}