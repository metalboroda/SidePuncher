using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement.UI
{
  public class CanvasBase : MonoBehaviour
  {
    protected GameSettings GameSettings;

    protected virtual void Awake() {
      LoadSettings();
    }

    protected void SwitchMusicVolumeButton() {
      GameSettings.IsMusicOn = !GameSettings.IsMusicOn;

      UpdateMusicButtonVisuals();

      SettingsManager.SaveSettings(GameSettings);

      EventBus<MusicSwitched>.Raise();
    }

    protected void SwitchSFXVolumeButton() {
      GameSettings.IsSFXOn = !GameSettings.IsSFXOn;

      UpdateSFXButtonVisuals();

      SettingsManager.SaveSettings(GameSettings);

      EventBus<SFXSwitched>.Raise();
    }

    protected virtual void UpdateMusicButtonVisuals() { }

    protected virtual void UpdateSFXButtonVisuals() { }

    private void LoadSettings() {
      if (GameSettings == null) {
        GameSettings = new GameSettings();
      }

      GameSettings = SettingsManager.LoadSettings<GameSettings>();
    }
  }
}