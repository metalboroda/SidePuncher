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

    private List<int> _previousTracks = new List<int>();

    private AudioSource _audioSource;

    private GameSettings _gameSettings;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();

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

    private void Start()
    {
      StartCoroutine(DoPlaySoundtracks());
    }

    private IEnumerator DoPlaySoundtracks()
    {
      while (true)
      {
        int randomIndex = GetRandomTrackIndex();

        _audioSource.clip = soundtracks[randomIndex];
        _audioSource.Play();

        while (_audioSource.isPlaying)
        {
          yield return null;
        }

        _previousTracks.Add(randomIndex);

        if (_previousTracks.Count > 2)
        {
          _previousTracks.RemoveAt(0);
        }
      }
    }

    private int GetRandomTrackIndex()
    {
      int randomIndex;

      do
      {
        randomIndex = Random.Range(0, soundtracks.Count);
      } while (_previousTracks.Contains(randomIndex));

      return randomIndex;
    }
  }
}