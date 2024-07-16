using Assets.__Game.Scripts.EventBus;
using DG.Tweening;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement
{
  public class CameraManager : MonoBehaviour
  {
    [Header("Punch Reaction")]
    [SerializeField] private float punchReactionDuration = 0.1f;
    [SerializeField] private Vector3 punchReactionDirection = new Vector3(0.1f, 0.1f, 0f);
    [Header("Hit Reaction")]
    [SerializeField] private float hitReactionDuration = 0.25f;
    [SerializeField] private Vector3 hitReactionDirection = new Vector3(0.2f, 0.2f, 0f);
    [Header("")]
    [SerializeField] private int vibrato = 50;

    private EventBinding<EnemyDamaged> _enemyDamagedEvent;
    private EventBinding<PlayerDamaged> _playerDamagedEvent;

    private Camera _mainCamera;

    private void Awake() {
      _mainCamera = Camera.main;
    }

    private void OnEnable() {
      _enemyDamagedEvent = new EventBinding<EnemyDamaged>(PunchCamera);
      _playerDamagedEvent = new EventBinding<PlayerDamaged>(HitCamera);
    }

    private void OnDisable() {
      _enemyDamagedEvent.Remove(PunchCamera);
      _playerDamagedEvent.Remove(HitCamera);
    }

    private void PunchCamera() {
      _mainCamera.transform.DOPunchPosition(
        punchReactionDirection, punchReactionDuration, vibrato).SetEase(Ease.OutQuad).SetAutoKill(true);
    }

    private void HitCamera() {
      _mainCamera.transform.DOPunchPosition(
        hitReactionDirection, hitReactionDuration, vibrato).SetEase(Ease.OutQuad).SetAutoKill(true);
    }
  }
}