using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;
using UnityEngine;

namespace Assets.__Game.Scripts.Game
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    private readonly GameStateMachine _stateMachine;

    public SceneLoader SceneLoader { get; private set; }

    public GameBootstrapper()
    {
      _stateMachine = new GameStateMachine();
    }

    private void Awake()
    {
      if (Instance != null && Instance != this)
      {
        Destroy(this);
      }
      else
      {
        Instance = this;
      }

      DontDestroyOnLoad(this);

      SceneLoader = new SceneLoader();
    }

    public void Start()
    {
      _stateMachine.Init(new MainMenuState(this));
    }
  }
}