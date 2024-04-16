using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.Utils;
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

    protected override void Awake()
    {
      base.Awake();

      _gameBootstrapper = GameBootstrapper.Instance;
      _sceneLoader = _gameBootstrapper.SceneLoader;
    }

    private void Start()
    {
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

      musicButton.onClick.AddListener(SwitchMusicVolumeButton);
      sfxButton.onClick.AddListener(SwitchSFXVolumeButton);
    }

    protected override void UpdateMusicButtonVisuals()
    {
      musicOnIcon.SetActive(_gameSettings.IsMusicOn);
      musicOffIcon.SetActive(!_gameSettings.IsMusicOn);
    }

    protected override void UpdateSFXButtonVisuals()
    {
      sfxOnIcon.SetActive(_gameSettings.IsSFXOn);
      sfxOffIcon.SetActive(!_gameSettings.IsSFXOn);
    }
  }
}