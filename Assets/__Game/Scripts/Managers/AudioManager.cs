using Assets.__Game.Scripts.Infrastructure;
using Assets.__Game.Scripts.Utils;
using EventBus;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.__Game.Scripts.Managers
{
  public class AudioManager : MonoBehaviour
  {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;

    [Space]
    [SerializeField] private float musicMaxVolume;
    [SerializeField] private float sfxMaxVolume;

    private GameSettings _gameSettings;

    private EventBinding<MusicSwitched> _musicSwitchedEvent;
    private EventBinding<SFXSwitched> _sfxSwitchedEvent;

    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;

        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
    }

    private void OnEnable()
    {
      _musicSwitchedEvent = new EventBinding<MusicSwitched>(SwitchMusicVolume);
      _sfxSwitchedEvent = new EventBinding<SFXSwitched>(SwitchSFXVolume);
    }

    private void OnDisable()
    {
      _musicSwitchedEvent.Remove(SwitchMusicVolume);
      _sfxSwitchedEvent.Remove(SwitchSFXVolume);
    }

    private void Start()
    {
      LoadSettings();
      LoadVolumes();
    }

    private void LoadSettings()
    {
      if (_gameSettings == null)
        _gameSettings = new GameSettings();

      _gameSettings = SettingsManager.LoadSettings<GameSettings>();
    }

    private void LoadVolumes()
    {
      if (_gameSettings.IsMusicOn == true)
        mixer.SetFloat(Hashes.MusicVolume, musicMaxVolume);
      else
        mixer.SetFloat(Hashes.MusicVolume, -80f);

      if (_gameSettings.IsSFXOn == true)
        mixer.SetFloat(Hashes.SFXVolume, sfxMaxVolume);
      else
        mixer.SetFloat(Hashes.SFXVolume, -80f);
    }

    public void SwitchMusicVolume()
    {
      mixer.GetFloat(Hashes.MusicVolume, out float currentVolume);

      if (currentVolume == 0f)
      {
        mixer.SetFloat(Hashes.MusicVolume, -80f);
        _gameSettings.IsMusicOn = false;
      }
      else
      {
        mixer.SetFloat(Hashes.MusicVolume, musicMaxVolume);
        _gameSettings.IsMusicOn = true;
      }

      SettingsManager.SaveSettings(_gameSettings);
    }

    public void SwitchSFXVolume()
    {
      mixer.GetFloat(Hashes.SFXVolume, out float currentVolume);

      if (currentVolume == 0f)
      {
        mixer.SetFloat(Hashes.SFXVolume, -80f);
        _gameSettings.IsSFXOn = false;
      }
      else
      {
        mixer.SetFloat(Hashes.SFXVolume, sfxMaxVolume);
        _gameSettings.IsSFXOn = true;
      }

      SettingsManager.SaveSettings(_gameSettings);
    }
  }
}