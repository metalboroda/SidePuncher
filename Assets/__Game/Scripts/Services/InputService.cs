using System;
using UnityEngine.InputSystem;

namespace Assets.__Game.Scripts.Services
{
  public class InputService
  {
    public event Action LeftAttackTriggered;
    public event Action RightAttackTriggered;

    private readonly PlayerInputActions _inputActions;

    public InputService()
    {
      _inputActions = new PlayerInputActions();
      _inputActions.Enable();

      _inputActions.OnFeet.LeftAttack.performed += OnLeftAttack;
      _inputActions.OnFeet.RightAttack.performed += OnRightAttack;
    }

    public void OnLeftAttack(InputAction.CallbackContext context)
    {
      LeftAttackTriggered?.Invoke();
    }

    public void OnRightAttack(InputAction.CallbackContext context)
    {
      RightAttackTriggered?.Invoke();
    }

    public void Dispose()
    {
      _inputActions.Disable();

      _inputActions.OnFeet.LeftAttack.performed -= OnLeftAttack;
      _inputActions.OnFeet.RightAttack.performed -= OnRightAttack;
    }
  }
}