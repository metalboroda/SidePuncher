using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Services;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement.UI
{
  public class GameCanvasManager : MonoBehaviour
  {
    [SerializeField] private GameObject _gameplayCanvas;
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _endCanvas;

    private List<GameObject> _canvases = new List<GameObject>();

    private InputService _inputService;

    private EventBinding<StateChanged> _stateChangedEvent;

    private void Awake() {
      _inputService = new InputService();

      AddCanvasesToList();
    }

    private void OnEnable() {
      _inputService.PausePressed += OnInputPauseButtonPressed;

      _stateChangedEvent = new EventBinding<StateChanged>(CanvasDependsOnState);
    }

    private void OnDisable() {
      _inputService.PausePressed -= OnInputPauseButtonPressed;

      _stateChangedEvent.Remove(CanvasDependsOnState);
    }

    private void OnDestroy() {
      _inputService.Dispose();
    }

    private void AddCanvasesToList() {
      _canvases.Add(_gameplayCanvas);
      _canvases.Add(_pauseCanvas);
      _canvases.Add(_endCanvas);
    }

    private void CanvasDependsOnState(StateChanged stateChanged) {
      switch (stateChanged.State) {
        case GameplayState:
          EnableCanvas(_gameplayCanvas);
          EnableCursor(false);
          break;
        case GamePauseState:
          EnableCanvas(_pauseCanvas);
          EnableCursor(true);
          break;
        case GameEndState:
          EnableCanvas(_endCanvas);
          EnableCursor(true);
          break;
      }
    }

    private void OnInputPauseButtonPressed() {
      EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = Enums.UIButtonEnums.Pause });
    }

    private void EnableCanvas(GameObject canvasToEnable) {
      foreach (GameObject canvas in _canvases) {
        canvas.SetActive(false);
      }

      canvasToEnable.SetActive(true);
    }

    private void EnableCursor(bool enable) {
      if (enable) {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
      }
      else {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      }
    }
  }
}