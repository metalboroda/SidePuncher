using Assets.__Game.Scripts.EventBus;
using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.Audio;
using static Assets.__Game.Scripts.EventBus.EventStructs;

namespace Assets.__Game.Scripts.GameManagement
{
  public class AudioManager : MonoBehaviour
  {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [Space]
    [SerializeField] private float musicMaxVolume = 0f;
    [SerializeField] private float sfxMaxVolume = 0f;

    private GameSettings _gameSettings;

    private EventBinding<MusicSwitched> _musicSwitchedEvent;
    private EventBinding<SFXSwitched> _sfxSwitchedEvent;

    private void Awake() {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);
      }
      else {
        lock (typeof(AudioManager)) {
          if (Instance == null) {
            Instance = this;

            DontDestroyOnLoad(gameObject);
          }
        }
      }
    }

    private void OnEnable() {
      _musicSwitchedEvent = new EventBinding<MusicSwitched>(SwitchMusicVolume);
      _sfxSwitchedEvent = new EventBinding<SFXSwitched>(SwitchSFXVolume);
    }

    private void OnDisable() {
      _musicSwitchedEvent?.Remove(SwitchMusicVolume);
      _sfxSwitchedEvent?.Remove(SwitchSFXVolume);
    }

    private void Start() {
      LoadSettings();
      LoadVolumes();
    }

    private void LoadSettings() {
      if (_gameSettings == null)
        _gameSettings = new GameSettings();

      _gameSettings = SettingsManager.LoadSettings<GameSettings>();
    }

    private void LoadVolumes() {
      SetVolume(Hashes.MusicVolume, _gameSettings.IsMusicOn ? musicMaxVolume : -80f);
      SetVolume(Hashes.SFXVolume, _gameSettings.IsSFXOn ? sfxMaxVolume : -80f);
    }

    private void SetVolume(string parameter, float volume) {
      mixer.SetFloat(parameter, volume);
    }

    public void SwitchMusicVolume() {
      SwitchVolume(Hashes.MusicVolume, ref _gameSettings.IsMusicOn, musicMaxVolume);
    }

    public void SwitchSFXVolume() {
      SwitchVolume(Hashes.SFXVolume, ref _gameSettings.IsSFXOn, sfxMaxVolume);
    }

    private void SwitchVolume(string parameter, ref bool setting, float maxVolume) {
      mixer.GetFloat(parameter, out float currentVolume);

      if (Mathf.Approximately(currentVolume, maxVolume)) {
        SetVolume(parameter, -80f);

        setting = false;
      }
      else {
        SetVolume(parameter, maxVolume);

        setting = true;
      }

      SettingsManager.SaveSettings(_gameSettings);
    }
  }
}