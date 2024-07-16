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

    public void LoadSceneAsync(string sceneName, Action callback) {
      SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += (AsyncOperation asyncOp) => {
        callback?.Invoke();
      };
    }

    public void LoadScene(string sceneName) {
      SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void RestartScene() {
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnUIButtonPressed(EventStructs.UIButtonPressed uiButtonPressed) {
      if (uiButtonPressed.Button == Enums.UIButtonEnums.StartGame) {
        LoadScene(Hashes.GameScene);
      }
    }
  }
}