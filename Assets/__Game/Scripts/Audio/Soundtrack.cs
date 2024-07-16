using Assets.__Game.Scripts.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.__Game.Scripts.Audio
{
  [RequireComponent(typeof(AudioSource))]
  public class Soundtrack : MonoBehaviour
  {
    public static Soundtrack Instance { get; private set; }

    [SerializeField] private AudioMixer musicMixer;
    [Space]
    [SerializeField] private List<AudioClip> soundtracks = new List<AudioClip>();

    private AudioSource _audioSource;
    private GameSettings _gameSettings;
    private List<AudioClip> _shuffledTracks;
    private int _currentTrackIndex = 0;

    private void Awake() {
      _audioSource = GetComponent<AudioSource>();

      if (Instance != null && Instance != this) {
        Destroy(gameObject);
      }
      else {
        lock (typeof(Soundtrack)) {
          if (Instance == null) {
            Instance = this;

            DontDestroyOnLoad(gameObject);
          }
        }
      }
    }

    private void Start() {
      LoadSettings();
      ShuffleTracks();
      StartCoroutine(DoPlaySoundtracks());
    }

    private void LoadSettings() {
      if (_gameSettings == null)
        _gameSettings = new GameSettings();

      _gameSettings = SettingsManager.LoadSettings<GameSettings>();
    }

    private void ShuffleTracks() {
      _shuffledTracks = new List<AudioClip>(soundtracks);

      for (int i = 0; i < _shuffledTracks.Count; i++) {
        AudioClip temp = _shuffledTracks[i];
        int randomIndex = Random.Range(0, _shuffledTracks.Count);

        _shuffledTracks[i] = _shuffledTracks[randomIndex];
        _shuffledTracks[randomIndex] = temp;
      }
    }

    private IEnumerator DoPlaySoundtracks() {
      while (true) {
        if (_currentTrackIndex >= _shuffledTracks.Count) {
          ShuffleTracks();

          _currentTrackIndex = 0;
        }

        _audioSource.clip = _shuffledTracks[_currentTrackIndex];
        _audioSource.Play();

        while (_audioSource.isPlaying) {
          yield return null;
        }

        _currentTrackIndex++;
      }
    }
  }
}