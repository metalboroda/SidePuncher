using Assets.__Game.Scripts.Enums;
using Assets.__Game.Scripts.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.UI
{
  public class EndCanvas : MonoBehaviour
  {
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [Space]
    [SerializeField] private TextMeshProUGUI waveLabelCounterText;

    private EventBinding<WaveCompleted> _waveCompletedEvent;

    private void Awake() {
      SubscribeButtons();
    }

    private void OnEnable() {
      _waveCompletedEvent = new EventBinding<WaveCompleted>(DisplayEndWaveCounter);
    }

    private void OnDisable() {
      _waveCompletedEvent.Remove(DisplayEndWaveCounter);
    }

    private void SubscribeButtons() {
      restartButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.Restart });
      });

      mainMenuButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.MainMenu });
      });
    }

    private void DisplayEndWaveCounter(WaveCompleted waveCompleted) {
      waveLabelCounterText.text = $"YOU SURVIVED \n{waveCompleted.WaveCount} WAVES";
    }
  }
}