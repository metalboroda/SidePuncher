using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.GameManagement.UI;
using UnityEngine;
using UnityEngine.UI;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.UI
{
  public class MainMenuCanvas : CanvasBase
  {
    [SerializeField] private Button playBtn;
    [Header("Audio")]
    [SerializeField] private Button musicButton;
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;
    [Space]
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
      playBtn.onClick.AddListener(() => {
        EventBus<UIButtonPressed>.Raise(new UIButtonPressed { Button = Enums.UIButtonEnums.StartGame });
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