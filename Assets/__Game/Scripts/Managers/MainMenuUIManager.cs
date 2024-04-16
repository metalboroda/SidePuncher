using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.Utils;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Scripts.Managers
{
  public class MainMenuUIManager : UIManagerBase
  {
    [SerializeField] private Button playBtn;

    [Header("Audio")]
    [Space]
    [SerializeField] private Button musicButton;
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;

    [Space]
    [SerializeField] private Button sfxButton;
    [SerializeField] private GameObject sfxOnIcon;
    [SerializeField] private GameObject sfxOffIcon;

    private GameBootstrapper _gameBootstrapper;
    private SceneLoader _sceneLoader;

    private GameSettings _gameSettings;

    private void Awake()
    {
      _gameBootstrapper = GameBootstrapper.Instance;
      _sceneLoader = _gameBootstrapper.SceneLoader;
    }

    private void Start()
    {
      LoadSettings();
      SubscribeButtons();
      UpdateMusicButtonVisuals();
      UpdateSFXButtonVisuals();
    }

    private void SubscribeButtons()
    {
      playBtn.onClick.AddListener(() =>
      {
        _sceneLoader.LoadSceneAsync(Hashes.GameScene, () =>
        {
          _gameBootstrapper.GameStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
        });
      });

      musicButton.onClick.AddListener(() =>
      {
        SwitchMusicVolumeButton();
      });
      sfxButton.onClick.AddListener(() =>
      {
        SwitchSFXVolumeButton();
      });
    }

    private void SwitchMusicVolumeButton()
    {
      _gameSettings.IsMusicOn = !_gameSettings.IsMusicOn;

      UpdateMusicButtonVisuals();
      EventBus<MusicSwitched>.Raise();
      SettingsManager.SaveSettings(_gameSettings);
    }

    private void SwitchSFXVolumeButton()
    {
      _gameSettings.IsSFXOn = !_gameSettings.IsSFXOn;

      UpdateSFXButtonVisuals();
      EventBus<SFXSwitched>.Raise();
      SettingsManager.SaveSettings(_gameSettings);
    }

    private void UpdateMusicButtonVisuals()
    {
      musicOnIcon.SetActive(_gameSettings.IsMusicOn);
      musicOffIcon.SetActive(!_gameSettings.IsMusicOn);
    }

    private void UpdateSFXButtonVisuals()
    {
      sfxOnIcon.SetActive(_gameSettings.IsSFXOn);
      sfxOffIcon.SetActive(!_gameSettings.IsSFXOn);
    }

    private void LoadSettings()
    {
      if (_gameSettings == null)
        _gameSettings = new GameSettings();

      _gameSettings = SettingsManager.LoadSettings<GameSettings>();
    }
  }
}