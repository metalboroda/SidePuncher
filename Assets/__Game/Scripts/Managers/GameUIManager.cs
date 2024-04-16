using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Utils;
using DG.Tweening;
using EventBus;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PauseState = Assets.__Game.Scripts.Game.GameStates.PauseState;

namespace Assets.__Game.Scripts.Managers
{
  public class GameUIManager : UIManagerBase
  {
    [Header("Wave")]
    [SerializeField] private TextMeshProUGUI waveCounterText;
    [SerializeField] private float waveFadeDuration = 0.25f;
    [SerializeField] private float waveFadeDelay = 1f;

    [Header("Gameplay")]
    [SerializeField] private GameObject gameCanvas;

    [Header("Pause")]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Button pauseContinueButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseExitButton;
    [SerializeField] private Button pauseMusicButton;
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;
    [SerializeField] private Button pauseSFXButton;
    [SerializeField] private GameObject sfxOnIcon;
    [SerializeField] private GameObject sfxOffIcon;

    [Header("End")]
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Button endRestartButton;
    [SerializeField] private Button endExitButton;

    private int _waveCounter;
    private readonly List<GameObject> _canvases = new List<GameObject>();

    private GameBootstrapper _gameBootstrapper;
    private GameSettings _gameSettings;

    private EventBinding<WaveCompleted> _waveCompletedEvent;
    private EventBinding<GameStateChanged> _gameStateChangedEvent;

    private void Awake()
    {
      _gameBootstrapper = GameBootstrapper.Instance;
    }

    private void OnEnable()
    {
      _waveCompletedEvent = new EventBinding<WaveCompleted>(DisplayWaveCounter);
      _gameStateChangedEvent = new EventBinding<GameStateChanged>(SwitchCanvasByState);
    }

    private void OnDisable()
    {
      _waveCompletedEvent.Remove(DisplayWaveCounter);
      _gameStateChangedEvent.Remove(SwitchCanvasByState);
    }

    private void Start()
    {
      AddCanvasesToList();
      SubscribeButtons();
    }

    private void AddCanvasesToList()
    {
      _canvases.Add(gameCanvas);
      _canvases.Add(pauseCanvas);
      _canvases.Add(endCanvas);
    }

    private void SubscribeButtons()
    {
      // Pause
      pauseContinueButton.onClick.AddListener(() =>
      {
        _gameBootstrapper.GameStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
      });
      pauseRestartButton.onClick.AddListener(() =>
      {
        _gameBootstrapper.SceneLoader.RestartScene();
        _gameBootstrapper.GameStateMachine.ChangeState(new MainMenuState(_gameBootstrapper));
      });
      pauseExitButton.onClick.AddListener(() =>
      {
        _gameBootstrapper.SceneLoader.LoadSceneAsync(Hashes.MainMenuScene, () =>
        {
          _gameBootstrapper.GameStateMachine.ChangeState(new MainMenuState(_gameBootstrapper));
        });
      });

      // End
      endRestartButton.onClick.AddListener(() =>
      {
        _gameBootstrapper.SceneLoader.RestartScene();
        _gameBootstrapper.GameStateMachine.ChangeState(new MainMenuState(_gameBootstrapper));
      });
      endExitButton.onClick.AddListener(() =>
      {
        _gameBootstrapper.SceneLoader.LoadSceneAsync(Hashes.MainMenuScene, () =>
        {
          _gameBootstrapper.GameStateMachine.ChangeState(new MainMenuState(_gameBootstrapper));
        });
      });
    }

    private void DisplayWaveCounter(WaveCompleted waveCompleted)
    {
      _waveCounter = waveCompleted.WaveCount;
      waveCounterText.text = $"WAVE {_waveCounter} \n COMPLETED";

      Sequence sequence = DOTween.Sequence();

      sequence.Append(waveCounterText.DOFade(1, waveFadeDuration));
      sequence.AppendInterval(waveFadeDelay);
      sequence.Append(waveCounterText.DOFade(0, waveFadeDuration));
    }

    private void SwitchCanvasByState(GameStateChanged gameState)
    {
      switch (gameState.State)
      {
        case GameplayState:
          SwitchCanvas(gameCanvas);
          break;
        case PauseState:
          SwitchCanvas(pauseCanvas);
          break;
        case EndState:
          SwitchCanvas(endCanvas, 2);
          break;
      }
    }

    private void SwitchCanvas(GameObject canvas, float delay = 0)
    {
      StartCoroutine(DoSwitchCanvas(canvas, delay));
    }

    private IEnumerator DoSwitchCanvas(GameObject canvas, float delay)
    {
      yield return new WaitForSeconds(delay);

      foreach (var canvasItem in _canvases)
      {
        if (canvasItem == canvas)
          canvas.SetActive(true);
        else
          canvasItem.SetActive(false);
      }
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