using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerAttackHandler : CharacterAttackHandlerBase
  {
    public event Action AttackTriggered;

    [Space]
    [SerializeField] private float yRotationLeft = -90f;
    [SerializeField] private float yRotationRight = 90f;
    [SerializeField] private float rotationDuration = 0.1f;
    [SerializeField] private float allowAttackTime = 0.15f;

    private bool _canAttack = true;
    private float _lastAttackTime;
    private Tweener _rotationTween;

    private void Update()
    {
      AllowAttackTimer();
    }

    public void LeftAttack()
    {
      if (_canAttack == false) return;

      SmoothRotateY(yRotationLeft);
      OnAttack();
    }

    public void RightAttack()
    {
      if (_canAttack == false) return;

      SmoothRotateY(yRotationRight);
      OnAttack();
    }

    private void OnAttack()
    {
      AttackTriggered?.Invoke();
      _canAttack = false;
      _lastAttackTime = Time.time;
    }

    private void SmoothRotateY(float y)
    {
      // DOTween goes crazy after restart
      try
      {
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, y, transform.rotation.z);

        DOTween.Kill(_rotationTween, true);

        _rotationTween = transform.DORotateQuaternion(targetRotation, rotationDuration).SetAutoKill(true);
      }
      catch { }
    }

    private void AllowAttackTimer()
    {
      if (_canAttack == false && Time.time - _lastAttackTime >= allowAttackTime)
        _canAttack = true;
    }
  }
}