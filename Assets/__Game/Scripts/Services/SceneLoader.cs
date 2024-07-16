using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.__Game.Scripts.Services
{
  public class SceneLoader
  {
    private GameBootstrapper _gameBootstrapper;

    private EventBinding<EventStructs.UIButtonPressed> _uiButtonPressed;

    public SceneLoader(GameBootstrapper gameBootstrapper) {
      _gameBootstrapper = gameBootstrapper;

      _uiButtonPressed = new EventBinding<EventStructs.UIButtonPressed>(OnUIButtonPressed);
    }

    public void Dispose() {
      _uiButtonPressed.Remove(OnUIButtonPressed);
    }

    public void LoadScene(string sceneName) {
      SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneAsync(string sceneName, Action callback) {
      SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += (AsyncOperation asyncOp) => {
        callback?.Invoke();
      };
    }

    public void RestartScene() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void RestartSceneAsync(Action callback) {
      SceneManager.LoadSceneAsync(
        SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single).completed += (AsyncOperation asyncOp) => {
          callback?.Invoke();
        };
    }

    private void OnUIButtonPressed(EventStructs.UIButtonPressed uiButtonPressed) {
      switch (uiButtonPressed.Button) {
        case Enums.UIButtonEnums.None:
          break;
        case Enums.UIButtonEnums.StartGame:
          LoadScene(Hashes.GameScene);
          break;
        case Enums.UIButtonEnums.MainMenu:
          LoadScene(Hashes.MainMenuScene);
          break;
        case Enums.UIButtonEnums.Restart:
          RestartScene();
          break;
      }
    }
  }
}