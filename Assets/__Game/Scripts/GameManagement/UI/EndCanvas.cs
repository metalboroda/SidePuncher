using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;
using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Enums;

namespace Assets.__Game.Scripts.GameManagement.UI
{
  public class EndCanvas : MonoBehaviour
  {
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Button endRestartButton;
    [SerializeField] private Button endExitButton;
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
      endRestartButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.Restart });
      });

      endExitButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.MainMenu });
      });
    }

    private void DisplayEndWaveCounter(WaveCompleted waveCompleted) {
      waveLabelCounterText.text = $"YOU SURVIVED \n{waveCompleted.WaveCount} WAVES";
    }
  }
}