using Assets.__Game.Scripts.Enums;
using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement.UI
{
  public class PauseCanvas : MonoBehaviour
  {
    [Header("Pause")]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Button pauseContinueButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseExitButton;
    [SerializeField] private Button pauseMusicButton;
    [Space]
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;
    [SerializeField] private Button pauseSFXButton;
    [SerializeField] private GameObject sfxOnIcon;
    [SerializeField] private GameObject sfxOffIcon;

    private GameSettings _gameSettings;

    private void Awake() {
      LoadSettings();
      SubscribeButtons();
    }

    private void SubscribeButtons() {
      pauseContinueButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.StartGame });
      });

      pauseRestartButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.Restart });
      });

      pauseExitButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.MainMenu });
      });

      pauseMusicButton.onClick.AddListener(SwitchMusicVolumeButton);
      pauseSFXButton.onClick.AddListener(SwitchSFXVolumeButton);
    }

    protected void SwitchMusicVolumeButton() {
      _gameSettings.IsMusicOn = !_gameSettings.IsMusicOn;

      UpdateMusicButtonVisuals();

      SettingsManager.SaveSettings(_gameSettings);

      EventBus<MusicSwitched>.Raise();
    }

    protected void SwitchSFXVolumeButton() {
      _gameSettings.IsSFXOn = !_gameSettings.IsSFXOn;

      UpdateSFXButtonVisuals();

      SettingsManager.SaveSettings(_gameSettings);

      EventBus<SFXSwitched>.Raise();
    }

    private void UpdateMusicButtonVisuals() {
      musicOnIcon.SetActive(_gameSettings.IsMusicOn);
      musicOffIcon.SetActive(!_gameSettings.IsMusicOn);
    }

    private void UpdateSFXButtonVisuals() {
      sfxOnIcon.SetActive(_gameSettings.IsSFXOn);
      sfxOffIcon.SetActive(!_gameSettings.IsSFXOn);
    }

    private void LoadSettings() {
      if (_gameSettings == null)
        _gameSettings = new GameSettings();

      _gameSettings = SettingsManager.LoadSettings<GameSettings>();
    }
  }
}