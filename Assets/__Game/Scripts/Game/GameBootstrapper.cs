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

    private void Awake()
    {
      if (Instance != null && Instance != this)
        Destroy(this);
      else
        Instance = this;

      DontDestroyOnLoad(this);

      SceneLoader = new SceneLoader();
      GameStateMachine = new StateMachine();
    }

    public void Start()
    {
      GameStateMachine.Init(new MainMenuState(this));
    }
  }
}