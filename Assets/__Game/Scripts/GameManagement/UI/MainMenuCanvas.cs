using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement.UI
{
  public class MainMenuCanvas : UIManagerBase
  {
    [SerializeField] private Button playBtn;
    [Header("Audio")]
    [SerializeField] private Button musicButton;
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;
    [Space]
    [SerializeField] private Button sfxButton;
    [SerializeField] private GameObject sfxOnIcon;
    [SerializeField] private GameObject sfxOffIcon;

    private GameBootstrapper _gameBootstrapper;
    private SceneLoader _sceneLoader;

    protected override void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;
      _sceneLoader = _gameBootstrapper.SceneLoader;

      base.Awake();
    }

    private void Start() {
      SubscribeButtons();
      UpdateMusicButtonVisuals();
      UpdateSFXButtonVisuals();
    }

    private void SubscribeButtons() {
      playBtn.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = Enums.UIButtonEnums.StartGame });
      });

      musicButton.onClick.AddListener(SwitchMusicVolumeButton);
      sfxButton.onClick.AddListener(SwitchSFXVolumeButton);
    }

    protected override void UpdateMusicButtonVisuals() {
      if (musicOnIcon == null || musicOffIcon == null) {
        Debug.LogError("Music icons are not assigned in the inspector");
        return;
      }

      musicOnIcon.SetActive(_gameSettings.IsMusicOn);
      musicOffIcon.SetActive(!_gameSettings.IsMusicOn);
    }

    protected override void UpdateSFXButtonVisuals() {
      if (sfxOnIcon == null || sfxOffIcon == null) {
        Debug.LogError("SFX icons are not assigned in the inspector");
        return;
      }

      sfxOnIcon.SetActive(_gameSettings.IsSFXOn);
      sfxOffIcon.SetActive(!_gameSettings.IsSFXOn);
    }
  }
}