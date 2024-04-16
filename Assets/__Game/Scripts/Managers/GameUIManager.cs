using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.Utils;
using DG.Tweening;
using EventBus;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PauseState = Assets.__Game.Scripts.Game.GameStates.PauseState;

namespace Assets.__Game.Scripts.Managers
{
  public class GameUIManager : MonoBehaviour
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
    [SerializeField] private Button pauseSFXButton;

    [Header("End")]
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Button endRestartButton;
    [SerializeField] private Button endExitButton;

    private int _waveCounter;
    private readonly List<GameObject> _canvases = new List<GameObject>();

    private GameBootstrapper _gameBootstrapper;

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
      }
    }

    private void SwitchCanvas(GameObject canvas)
    {
      foreach (var canvasItem in _canvases)
      {
        if (canvasItem == canvas)
          canvas.SetActive(true);
        else
          canvasItem.SetActive(false);
      }
    }
  }
}