using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Managers;
using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.Utils;
using EventBus;
using UnityEngine;

namespace Assets.__Game.Scripts.Game
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    public AudioManager AudioManager;

    public StateMachine GameStateMachine;
    public SceneLoader SceneLoader;

    private EventBinding<PlayerDead> _playerDeadEvent;

    public GameBootstrapper()
    {
      GameStateMachine = new StateMachine();
      SceneLoader = new SceneLoader();
    }

    private void Awake()
    {
      if (Instance != null && Instance != this)
      {
        Destroy(gameObject);
      }
      else
      {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        _playerDeadEvent = new EventBinding<PlayerDead>(() =>
        {
          GameStateMachine.ChangeState(new EndState(this));
        });
      }
    }

    public void Start()
    {
      SceneLoader.LoadSceneAsync(Hashes.MainMenuScene, () =>
      {
        GameStateMachine.Init(new MainMenuState(this));
      });
    }
  }
}