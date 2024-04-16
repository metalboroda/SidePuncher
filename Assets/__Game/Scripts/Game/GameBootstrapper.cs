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
      {
        Destroy(gameObject);
      }
      else
      {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        GameStateMachine = new StateMachine();
        SceneLoader = new SceneLoader();
      }
    }


    public void Start()
    {
      GameStateMachine.Init(new MainMenuState(this));
    }
  }
}