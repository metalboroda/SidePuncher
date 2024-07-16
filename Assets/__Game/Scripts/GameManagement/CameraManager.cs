using Assets.__Game.Scripts.EventBus;
using DG.Tweening;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement
{
  public class CameraManager : MonoBehaviour
  {
    [Header("Punch param's")]
    [SerializeField] private float punchDuration = 0.25f;
    [SerializeField] private Vector3 punchDirection = new Vector3(0.2f, 0.2f, 0f);

    private EventBinding<PlayerDamaged> _playerDamagedEvent;

    private Camera _mainCamera;

    private void Awake() {
      _mainCamera = Camera.main;
    }

    private void OnEnable() {
      _playerDamagedEvent = new EventBinding<PlayerDamaged>(PunchCamera);
    }

    private void OnDisable() {
      _playerDamagedEvent.Remove(PunchCamera);
    }

    private void PunchCamera() {
      _mainCamera.transform.DOPunchPosition(
        punchDirection, punchDuration, 50).SetEase(Ease.OutQuad).SetAutoKill(true);
    }
  }
}