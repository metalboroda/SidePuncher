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

    private EventBinding<EventStructs.UIButtonPressed> _uiButtonPressed;

    public GameStateManager(GameBootstrapper gameBootstrapper) {
      _gameBootstrapper = gameBootstrapper;
      _finiteStateMachine = gameBootstrapper.FiniteStateMachine;

      _uiButtonPressed = new EventBinding<EventStructs.UIButtonPressed>(OnUIButtonPressed);
    }

    private void OnUIButtonPressed(EventStructs.UIButtonPressed uiButtonPressed) {
      if (uiButtonPressed.Button == Enums.UIButtonEnums.StartGame) {
        _finiteStateMachine.ChangeState(new GameplayState(_gameBootstrapper));
      }
    }
  }
}