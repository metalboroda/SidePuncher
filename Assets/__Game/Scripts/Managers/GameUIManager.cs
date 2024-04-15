using DG.Tweening;
using EventBus;
using TMPro;
using UnityEngine;

namespace Assets.__Game.Scripts.Managers
{
  public class GameUIManager : MonoBehaviour
  {
    [Header("Wave")]
    [SerializeField] private TextMeshProUGUI waveCounterText;
    [SerializeField] private float waveFadeDuration = 0.25f;
    [SerializeField] private float waveFadeDelay = 1f;

    private EventBinding<WaveCompleted> _waveCompletedEvent;

    private void OnEnable()
    {
      _waveCompletedEvent = new EventBinding<WaveCompleted>(DisplayWaveCounter);
    }

    private void OnDisable()
    {
      _waveCompletedEvent.Remove(DisplayWaveCounter);
    }

    private void DisplayWaveCounter(WaveCompleted waveCompleted)
    {
      int counter = waveCompleted.waveCount;

      waveCounterText.text = $"WAVE {counter} \n COMPLETED";

      Sequence sequence = DOTween.Sequence();

      sequence.Append(waveCounterText.DOFade(1, waveFadeDuration));
      sequence.AppendInterval(waveFadeDelay);
      sequence.Append(waveCounterText.DOFade(0, waveFadeDuration));
    }
  }
}