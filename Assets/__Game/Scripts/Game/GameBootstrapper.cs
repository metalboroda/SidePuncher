using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Services;
using UnityEngine;

namespace Assets.__Game.Scripts.Game
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    public StateMachine GameStateMachine;
    public SceneLoader SceneLoader;

    public GameBootstrapper()
    {
      GameStateMachine = new StateMachine();
    }

    private void Awake()
    {
      if (Instance != null && Instance != this)
        Destroy(this);
      else
        Instance = this;

      DontDestroyOnLoad(this);

      SceneLoader = new SceneLoader();
    }

    public void Start()
    {
      GameStateMachine.Init(new MainMenuState(this));
    }

    public void ToGameplayState()
    {
      GameStateMachine.ChangeState(new GameplayState(this));
    }
  }
}