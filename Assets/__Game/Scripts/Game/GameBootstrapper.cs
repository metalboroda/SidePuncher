using Assets.__Game.Scripts.Game.GameStates;
using Assets.__Game.Scripts.Infrastructure;
using System;
using UnityEngine;
using Zenject;

namespace Assets.__Game.Scripts.Game
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    private readonly StateMachine _stateMachine;

    public SceneLoader SceneLoader { get; private set; }

    [Inject] private readonly EventBusService _eventBus;

    [Inject]
    public GameBootstrapper(EventBusService eventBus)
    {
      _stateMachine = new StateMachine();
      _eventBus = eventBus;
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

    public void Publish<T>(T message)
    {
      _eventBus.Publish(message);
    }

    public void Subscribe<T>(Action<T> callback)
    {
      _eventBus.Subscribe(callback);
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
      _eventBus.Unsubscribe(callback);
    }
  }
}