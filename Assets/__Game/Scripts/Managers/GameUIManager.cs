using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;
using DG.Tweening;
using EventBus;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("Pause")]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Button pauseContinueButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseExitButton;

    private readonly List<GameObject> _canvases = new List<GameObject>();

    private GameBootstrapper _bootstrapper;

    private EventBinding<WaveCompleted> _waveCompletedEvent;

    private void Awake()
    {
      _bootstrapper = GameBootstrapper.Instance;
    }

    private void OnEnable()
    {
      _waveCompletedEvent = new EventBinding<WaveCompleted>(DisplayWaveCounter);
    }

    private void OnDisable()
    {
      _waveCompletedEvent.Remove(DisplayWaveCounter);
    }

    private void Start()
    {
      AddCanvasesToList();
    }

    private void AddCanvasesToList()
    {
      _canvases.Add(pauseCanvas);
    }

    private void DisplayWaveCounter(WaveCompleted waveCompleted)
    {
      int counter = waveCompleted.WaveCount;

      waveCounterText.text = $"WAVE {counter} \n COMPLETED";

      Sequence sequence = DOTween.Sequence();

      sequence.Append(waveCounterText.DOFade(1, waveFadeDuration));
      sequence.AppendInterval(waveFadeDelay);
      sequence.Append(waveCounterText.DOFade(0, waveFadeDuration));
    }

    private void SwitchCanvas(State gameState)
    {
      foreach (var canvas in _canvases)
      {
        canvas.SetActive(false);
      }

      switch (gameState)
      {
        case GameplayState:
          break;
        case PauseState:
          break;

      }
    }
  }
}