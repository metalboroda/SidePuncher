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
    private float _lastAttackTime;

    private void Update()
    {
      AllowAttackTimer();
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
      _lastAttackTime = Time.time;
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

    private void AllowAttackTimer()
    {
      if (_canAttack == false && Time.time - _lastAttackTime >= allowAttackTime)
        _canAttack = true;
    }
  }
}