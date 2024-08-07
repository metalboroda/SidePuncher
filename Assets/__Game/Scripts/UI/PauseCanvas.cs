using Assets.__Game.Scripts.Enums;
using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.GameManagement.UI;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.UI
{
  public class PauseCanvas : CanvasBase
  {
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [Space]
    [SerializeField] private Button musicButton;
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;
    [SerializeField] private Button sfxButton;
    [SerializeField] private GameObject sfxOnIcon;
    [SerializeField] private GameObject sfxOffIcon;

    protected override void Awake() {
      base.Awake();
    }

    private void Start() {
      SubscribeButtons();
      UpdateMusicButtonVisuals();
      UpdateSFXButtonVisuals();
    }

    private void SubscribeButtons() {
      continueButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.Continue });
      });

      restartButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.Restart });
      });

      mainMenuButton.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = UIButtonEnums.MainMenu });
      });

      musicButton.onClick.AddListener(() => {
        SwitchMusicVolumeButton();

        EventBus<UIButtonPressed>.Raise(new UIButtonPressed());
      });

      sfxButton.onClick.AddListener(() => {
        SwitchSFXVolumeButton();

        EventBus<UIButtonPressed>.Raise(new UIButtonPressed());
      });
    }

    protected override void UpdateMusicButtonVisuals() {
      musicOnIcon.SetActive(GameSettings.IsMusicOn);
      musicOffIcon.SetActive(!GameSettings.IsMusicOn);
    }

    protected override void UpdateSFXButtonVisuals() {
      sfxOnIcon.SetActive(GameSettings.IsSFXOn);
      sfxOffIcon.SetActive(!GameSettings.IsSFXOn);
    }
  }
}