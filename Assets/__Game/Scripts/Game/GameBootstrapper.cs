using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.GameManagement;
using Assets.__Game.Scripts.Services;
using UnityEngine;

namespace Assets.__Game.Scripts.Game
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    public FiniteStateMachine FiniteStateMachine { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public SceneLoader SceneLoader { get; private set; }

    private static readonly object _lock = new object();

    private void Awake() {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);

        return;
      }

      lock (_lock) {
        if (Instance == null) {
          Instance = this;

          InitializeSingleton();
        }
      }

      DontDestroyOnLoad(gameObject);
    }

    private void Start() {
      FiniteStateMachine.Init(new GameMainMenuState(this));
    }

    private void InitializeSingleton() {
      FiniteStateMachine = new FiniteStateMachine();
      GameStateManager = new GameStateManager(this);
      SceneLoader = new SceneLoader(this);
    }
  }
}