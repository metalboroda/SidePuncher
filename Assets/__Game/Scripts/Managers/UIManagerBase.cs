using Assets.__Game.Scripts.Infrastructure;
using EventBus;
using UnityEngine;

namespace Assets.__Game.Scripts.Managers
{
  public class UIManagerBase : MonoBehaviour
  {
    protected GameSettings _gameSettings;

    protected virtual void Awake()
    {
      LoadSettings();
    }

    protected void SwitchMusicVolumeButton()
    {
      _gameSettings.IsMusicOn = !_gameSettings.IsMusicOn;

      UpdateMusicButtonVisuals();
      EventBus<MusicSwitched>.Raise();
      SettingsManager.SaveSettings(_gameSettings);
    }

    protected void SwitchSFXVolumeButton()
    {
      _gameSettings.IsSFXOn = !_gameSettings.IsSFXOn;

      UpdateSFXButtonVisuals();
      EventBus<SFXSwitched>.Raise();
      SettingsManager.SaveSettings(_gameSettings);
    }

    protected virtual void UpdateMusicButtonVisuals() { }

    protected virtual void UpdateSFXButtonVisuals() { }

    private void LoadSettings()
    {
      if (_gameSettings == null)
        _gameSettings = new GameSettings();

      _gameSettings = SettingsManager.LoadSettings<GameSettings>();
    }
  }
}