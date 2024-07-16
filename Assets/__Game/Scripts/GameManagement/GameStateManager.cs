using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Game;
using Assets.__Game.Scripts.Game.GameStates;

namespace Assets.__Game.Scripts.GameManagement
{
  public class GameStateManager
  {
    private GameBootstrapper _gameBootstrapper;
    private FiniteStateMachine _finiteStateMachine;

    private EventBinding<EventStructs.UIButtonPressed> _uiButtonPressedEvent;
    private EventBinding<EventStructs.PlayerDead> _playerDeadEvent;

    public GameStateManager(GameBootstrapper gameBootstrapper) {
      _gameBootstrapper = gameBootstrapper;
      _finiteStateMachine = gameBootstrapper.FiniteStateMachine;

      _uiButtonPressedEvent = new EventBinding<EventStructs.UIButtonPressed>(OnUIButtonPressed);
      _playerDeadEvent = new EventBinding<EventStructs.PlayerDead>(OnPlayerDead);
    }

    public void Dispose() {
      _uiButtonPressedEvent.Remove(OnUIButtonPressed);
      _playerDeadEvent.Remove(OnPlayerDead);
    }

    private void OnUIButtonPressed(EventStructs.UIButtonPressed uiButtonPressed) {
      switch (uiButtonPressed.Button) {
        case Enums.UIButtonEnums.None:
          break;
        case Enums.UIButtonEnums.StartGame:
          _finiteStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
          break;
        case Enums.UIButtonEnums.Continue:
          _finiteStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
          break;
        case Enums.UIButtonEnums.Pause:
          if (_finiteStateMachine.CurrentState is GameplayState) {
            _finiteStateMachine.ChangeState(new GamePauseState(_gameBootstrapper));
          }
          else if (_finiteStateMachine.CurrentState is GamePauseState) {
            _finiteStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
          }
          break;
        case Enums.UIButtonEnums.MainMenu:
          _finiteStateMachine.ChangeState(new GameMainMenuState(_gameBootstrapper));
          break;
        case Enums.UIButtonEnums.Restart:
          _finiteStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
          break;
      }
    }

    private void OnPlayerDead() {
      _finiteStateMachine.ChangeState(new GameEndState(_gameBootstrapper));
    }
  }
}