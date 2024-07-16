using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Services;
using Assets.__Game.Scripts.Utils;
using UnityEngine;

namespace Assets.__Game.Scripts.Game
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    public FiniteStateMachine FiniteStateMachine { get; private set; }
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

    private void InitializeSingleton() {
      FiniteStateMachine = new FiniteStateMachine();
      SceneLoader = new SceneLoader();
    }

    private void Start() {
      SceneLoader.LoadScene(Hashes.MainMenuScene);
    }
  }
}