using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Scripts.Managers
{
  public class MainMenuUIManager : MonoBehaviour
  {
    [SerializeField] private Button playBtn;

    private GameBootstrapper _gameBootstrapper;
    private StateMachine _stateMachine;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
      _gameBootstrapper = GameBootstrapper.Instance;
      _stateMachine = _gameBootstrapper.GameStateMachine;
      _sceneLoader = _gameBootstrapper.SceneLoader;
    }

    private void Start()
    {
      SubscribeButtons();
    }

    private void SubscribeButtons()
    {
      playBtn.onClick.AddListener(() =>
      {
        _gameBootstrapper.SceneLoader.LoadSceneAsync(
            Hashes.GameScene, _gameBootstrapper.ToGameplayState);     
      });
    }
  }
}