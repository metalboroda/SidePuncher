using Assets.__Game.Scripts.Services;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAttackHandler : MonoBehaviour
  {
    private InputService _inputService;

    private void Awake()
    {
      _inputService = new InputService();
    }

    private void OnEnable()
    {
      _inputService.LeftAttackTriggered += LeftAttack;
      _inputService.RightAttackTriggered += RightAttack;
    }

    private void OnDisable()
    {
      _inputService.LeftAttackTriggered -= LeftAttack;
      _inputService.RightAttackTriggered -= RightAttack;
    }

    private void OnDestroy()
    {
      _inputService.Dispose();
    }

    private void LeftAttack()
    {
      Debug.Log("LeftAttack");
    }

    private void RightAttack()
    {
      Debug.Log("RightAttack");
    }
  }
}