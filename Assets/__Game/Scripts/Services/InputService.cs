using System;
using UnityEngine.InputSystem;

namespace Assets.__Game.Scripts.Services
{
  public class InputService
  {
    public event Action PausePressed;

    public event Action LeftAttackTriggered;
    public event Action RightAttackTriggered;

    private readonly PlayerInputActions _inputActions;

    public InputService()
    {
      _inputActions = new PlayerInputActions();
      _inputActions.Enable();

      _inputActions.Navigation.Pause.performed += OnPausePressed;

      _inputActions.OnFeet.LeftAttack.performed += OnLeftAttack;
      _inputActions.OnFeet.RightAttack.performed += OnRightAttack;
    }

    public void OnPausePressed(InputAction.CallbackContext context)
    {
      PausePressed?.Invoke();
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

      _inputActions.Navigation.Pause.performed -= OnPausePressed;

      _inputActions.OnFeet.LeftAttack.performed -= OnLeftAttack;
      _inputActions.OnFeet.RightAttack.performed -= OnRightAttack;
    }
  }
}