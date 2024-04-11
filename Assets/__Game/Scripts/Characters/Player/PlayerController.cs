using Assets.__Game.Scripts.Characters.Player.PlayerStates;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.__Game.Scripts.Characters.Player
{
  public class PlayerController : MonoBehaviour
  {
    public PlayerHandler PlayerHandler;
    public PlayerAnimationHandler PlayerAnimationHandler;

    private StateMachine _stateMachine;

    private void Awake()
    {
      _stateMachine = new StateMachine();
    }

    private void Start()
    {
      _stateMachine.Init(new PlayerFightState(this));
    }
  }
}