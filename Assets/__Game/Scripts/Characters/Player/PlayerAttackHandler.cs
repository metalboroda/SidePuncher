using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAttackHandler : CharacterAttackHandlerBase
  {
    public event Action AttackTriggered;

    [Space]
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float allowAttackTime = 0.15f;

    private bool _canAttack = true;

    private InputService _inputService;
    private PlayerController _playerController;

    private void Awake()
    {
      _inputService = new InputService();
      _playerController = GetComponent<PlayerController>();
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

    public void LeftAttack()
    {
      if (_canAttack == false) return;

      StartCoroutine(DoSmoothRotateY(-90));
      OnAttack();
    }

    public void RightAttack()
    {
      if (_canAttack == false) return;

      StartCoroutine(DoSmoothRotateY(90));
      OnAttack();
    }

    private void OnAttack()
    {
      AttackTriggered?.Invoke();
      _canAttack = false;

      StartCoroutine(DoAllowAttack());
    }

    private IEnumerator DoSmoothRotateY(float y)
    {
      Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, y, transform.rotation.z);
      float elapsedTime = 0f;

      while (elapsedTime < rotationSpeed)
      {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime / rotationSpeed);
        elapsedTime += Time.deltaTime;

        yield return null;
      }

      transform.rotation = targetRotation;
    }

    private IEnumerator DoAllowAttack()
    {
      yield return new WaitForSeconds(allowAttackTime);

      _canAttack = true;
    }
  }
}