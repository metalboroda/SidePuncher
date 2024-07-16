using Assets.__Game.Scripts.EventBus;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.UI
{
  public class GameplayCanvas : MonoBehaviour
  {
    [SerializeField] private Image damageVignette;
    [SerializeField] private float damageMaxFade = 0.15f;
    [SerializeField] private float damageDuration = 0.1f;
    [Header("Wave")]
    [SerializeField] private TextMeshProUGUI waveCounterText;
    [SerializeField] private float waveFadeDuration = 0.25f;
    [SerializeField] private float waveFadeInDelay = 0.5f;
    [SerializeField] private float waveFadeOutDelay = 2.5f;

    private EventBinding<PlayerDamaged> _playerDamagedEvent;
    private EventBinding<WaveCompleted> _waveCompletedEvent;

    private void OnEnable() {
      _playerDamagedEvent = new EventBinding<PlayerDamaged>(DisplayDamageVignette);
      _waveCompletedEvent = new EventBinding<WaveCompleted>(DisplayWaveCounter);
    }

    private void OnDisable() {
      _playerDamagedEvent.Remove(DisplayDamageVignette);
      _waveCompletedEvent.Remove(DisplayWaveCounter);
    }

    private void DisplayDamageVignette() {
      Sequence sequence = DOTween.Sequence();

      sequence.Append(damageVignette.DOFade(0, damageDuration));
      sequence.Append(damageVignette.DOFade(damageMaxFade, damageDuration));
      sequence.AppendInterval(damageDuration / 5);
      sequence.Append(damageVignette.DOFade(0, damageDuration));
    }

    private void DisplayWaveCounter(WaveCompleted waveCompleted) {
      waveCounterText.text = $"WAVE {waveCompleted.WaveCount}\n COMPLETED";

      Sequence sequence = DOTween.Sequence();

      sequence.SetDelay(waveFadeInDelay);
      sequence.Append(waveCounterText.DOFade(1, waveFadeDuration));
      sequence.AppendInterval(waveFadeOutDelay);
      sequence.Append(waveCounterText.DOFade(0, waveFadeDuration));
    }
  }
}